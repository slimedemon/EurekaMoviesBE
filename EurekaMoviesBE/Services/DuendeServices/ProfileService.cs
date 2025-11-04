using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using EurekaMoviesBE.Constants;
using System.Security.Claims;

namespace EurekaMoviesBE.Services.DuendeServices
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ProfileService> _logger;

        public ProfileService(UserManager<User> userManager, ILogger<ProfileService> logger) 
        {
            _userManager = userManager;
            _logger = logger;
        }
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var clientId = context.Client.ClientId;
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub);
                
                if (user == null) 
                {
                    throw new InvalidOperationException("User not found");
                }

                var roles = await _userManager.GetRolesAsync(user);
                var claims = new List<Claim>
                {
                    new Claim(CustomClaimTypes.UserId, sub),
                    new Claim(CustomClaimTypes.Email, user.UserName!),
                    new Claim(CustomClaimTypes.ClientId, clientId)
                };

                foreach(var role in roles)
                {
                    claims.Add(new Claim(CustomClaimTypes.Role, role));
                }

                context.IssuedClaims.AddRange(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{nameof(ProfileService)} Has error: {ex.Message}");
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var account = await _userManager.FindByIdAsync(sub);
            context.IsActive = account != null;
        }
    }
}
