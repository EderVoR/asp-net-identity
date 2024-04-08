using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MvcWebIdentity.Services
{
	public class SeedUserClaimsInitial : ISeedUserClaimsInitial
	{
		private readonly UserManager<IdentityUser> _userManager;

        public SeedUserClaimsInitial(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

		public async Task SeedUserClaims()
		{
			try
			{
				IdentityUser user1 = await _userManager.FindByEmailAsync("admin@localhost");

				if(user1 is not null)
				{
					var claimList = (await _userManager.GetClaimsAsync(user1))
									.Select(p => p.Type);

					if (!claimList.Contains("CadastradoEm"))
						await _userManager.AddClaimAsync(user1, new Claim("CadastradoEm", DateTime.Now.ToString()));
					if (!claimList.Contains("IsAdmin"))
						await _userManager.AddClaimAsync(user1, new Claim("IsAdmin", "true"));
				}

				IdentityUser user2 = await _userManager.FindByEmailAsync("usuario@localhost");

				if (user2 is not null)
				{
					var claimList = (await _userManager.GetClaimsAsync(user2))
									.Select(p => p.Type);

					if (!claimList.Contains("CadastradoEm"))
						await _userManager.AddClaimAsync(user2, new Claim("IsAdmin", "false"));
					if (!claimList.Contains("IsAdmin"))
						await _userManager.AddClaimAsync(user2, new Claim("IsFuncionario", "true"));
				}
			}	
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
