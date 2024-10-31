using Ava.Domain.Models.User;
using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Ava.Infrastructure.Db.DataProviders;

internal static class IdentityDataProvider
{
    public static async Task InitializeUsersAndRolesTransactionallyAsync(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, AvaDbContext context)
    {
        await InitializeRoles(roleManager);

        var strategy = context.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var adminUser = new User { UserName = "admin", Email = "admin@ava.ge" };
                var adminResult = await userManager.CreateAsync(adminUser, "admin1234");

                if (adminResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "admin");
                }

                var customerUser = new User { UserName = "customer", Email = "customer@ava.ge" };
                var customerResult = await userManager.CreateAsync(customerUser, "customer1234");

                if (customerResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(customerUser, "customer");

                    var customer = new Customer(customerUser.Id, "John", "Doe", "12345667");
                    context.Customers.Add(customer);
                }
                else
                {
                    throw new Exception("Failed to create customer user.");
                }

                var therapistUser = new User { UserName = "therapist", Email = "therapist@ava.ge" };
                var therapistResult = await userManager.CreateAsync(therapistUser, "therapist1234");

                if (therapistResult.Succeeded)
                {
                    await userManager.AddToRoleAsync(therapistUser, "therapist");

                    var therapist = new Therapist(
                        therapistUser.Id,
                        "Jane",
                        "Smith",
                        "0987654321",
                        4.5d,
                        "some kind of summary",
                        Guid.NewGuid()
                    );
                    context.Therapists.Add(therapist);
                }
                else
                {
                    throw new Exception("Failed to create therapist user.");
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception("Transaction failed: " + ex.Message, ex);
            }
        });
    }

    private static async Task InitializeRoles(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roleNames = { CustomIdentityRoles.Admin, CustomIdentityRoles.Customer, CustomIdentityRoles.Therapist };

        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole<Guid>(roleName);

                await roleManager.CreateAsync(role);
            }
        }
    }
}
