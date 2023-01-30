using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MidtermTodos.Pages.Account;

public class Login : PageModel
{
    [BindProperty]
    public InputModel Input { get; set; }
    public string ReturnUrl { get; set; }
  
    private SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<Register> _logger;

    public Login(SignInManager<IdentityUser> signInManager,ILogger<Register> logger)
    {
        this._signInManager = signInManager;
        this._logger = logger;

    }
    public class InputModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "the {0} must be at least {2} and max {1} characters long.", 
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            
            var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, false, true);
            if (result.Succeeded)
            {
                //  await _signInManager.SignInAsync(user, isPersistent: true);
                _logger.LogInformation($"User {Input.UserName} Logged in");
                return RedirectToPage("../Index");
            }

            //user does not exist , password invalid , account locked out;
            ModelState.AddModelError(string.Empty,"Login Failed  : user does not exist , password invalid , account locked out");

           
        }

        return Page();
    }
    public void OnGet()
    {
        
    }
}