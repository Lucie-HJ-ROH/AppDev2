using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MidtermTodos.Pages.Account;

public class Logout : PageModel
{

  
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<Register> _logger;

    public Logout(SignInManager<IdentityUser> signInManager,ILogger<Register> logger)
    {
        this._signInManager = signInManager;
        this._logger = logger;

    }
    public async Task<IActionResult> OnGetAsync()
    {
        if (_signInManager.IsSignedIn(User))
        {
            _logger.LogInformation($"User {User.Identity.Name} Logg out");
        }

        await _signInManager.SignOutAsync();
        return Page();
    }
}