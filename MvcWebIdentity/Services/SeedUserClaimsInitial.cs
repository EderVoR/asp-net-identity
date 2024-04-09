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
						await _userManager.AddClaimAsync(user1, new Claim("CadastradoEm", "09/12/2014"));
					if (!claimList.Contains("IsAdmin"))
						await _userManager.AddClaimAsync(user1, new Claim("IsAdmin", "true"));
				}

				IdentityUser user2 = await _userManager.FindByEmailAsync("usuario@localhost");

				if (user2 is not null)
				{
					var claimList = (await _userManager.GetClaimsAsync(user2))
									.Select(p => p.Type);

					if (!claimList.Contains("IsAdmin"))
						await _userManager.AddClaimAsync(user2, new Claim("IsAdmin", "false"));
					if (!claimList.Contains("IsFuncionario"))
						await _userManager.AddClaimAsync(user2, new Claim("IsFuncionario", "true"));
					if (!claimList.Contains("CadastradoEm"))
						await _userManager.AddClaimAsync(user1, new Claim("CadastradoEm", "01/01/2020"));
				}

				IdentityUser user3 = await _userManager.FindByEmailAsync("gerente@localhost");

				if (user3 is not null)
				{
					var claimList = (await _userManager.GetClaimsAsync(user3))
									.Select(p => p.Type);
				
					if (!claimList.Contains("CadastradoEm"))
						await _userManager.AddClaimAsync(user3, new Claim("CadastradoEm", "03/03/2021"));
				}
			}	
			catch (Exception ex)
			{
				throw;
			}
		}
	}
}
