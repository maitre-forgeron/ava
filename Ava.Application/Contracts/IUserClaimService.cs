namespace Ava.Application.Contracts
{
    public interface IUserClaimService
    {
        bool HasRoleClaim(string roleClaim);
    }
}
