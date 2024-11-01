using Ava.Infrastructure.Db.DataProviders;
using Ava.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace Ava.Infrastructure.Db;

public class AvaDbContextInitialiser
{
    private readonly ILogger<AvaDbContext> _logger;
    private readonly AvaDbContext _context;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public AvaDbContextInitialiser(ILogger<AvaDbContext> logger, AvaDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GeneralErrorMessages.DatabaseInitializationErrorMessage);
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, GeneralErrorMessages.DatabaseSeedingErrorMessage);
            throw;
        }
    }

    private async Task TrySeedAsync()
    {
        if (_context.Database.CanConnect())
        {
            await SeedUsersAndRoles();
            await CreateCategories();
        }
    }

    private async Task SeedUsersAndRoles()
    {
        if (!_context.Users.Any())
        {
            await IdentityDataProvider.InitializeUsersAndRolesTransactionallyAsync(_userManager, _roleManager, _context);
        }
    }

    private async Task CreateCategories()
    {
        if (!_context.Categories.Any())
        {
            _context.AddRange(CategoriesProvider.GetCategories());
        }

        await _context.SaveChangesAsync();
    }
}
