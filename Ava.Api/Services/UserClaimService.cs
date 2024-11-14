using Ava.Application.Contracts;

namespace Ava.Api.Services
{
    public class UserClaimService : IUserClaimService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserClaimService(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool HasRoleClaim(string roleClaim)
        {
            var therapistClaim = _contextAccessor.HttpContext?.User.HasClaim(roleClaim, "true");

            if (therapistClaim == null || therapistClaim == false)
            {
                return false;
            }

            return true;
        }
    }
}
