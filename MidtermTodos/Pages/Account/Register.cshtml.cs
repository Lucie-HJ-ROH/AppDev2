using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MidtermTodos.Pages.Account;

public class Register : PageModel
{
    [BindProperty] public InputModel Input { get; set; }

    public string ReturnUrl { get; set; }

    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;
    private readonly ILogger<Register> _logger;

    public Register(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager,
        ILogger<Register> logger)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._logger = logger;
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "the {0} must be at least {2} and max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = Input.UserName, Email = Input.Email };
            var result = await _userManager.CreateAsync(user, Input.Password);
            if (result.Succeeded)
            {
                //  await _signInManager.SignInAsync(user, isPersistent: true);
    
                _logger.LogInformation($"User {Input.UserName} create a new account with password.");
                return RedirectToPage("RegisterSuccess", new { username = Input.UserName });
            }
    
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    
        return Page();
    }

    // public async Task<IActionResult> OnPostAsync()
    // {
    //     if (ModelState.IsValid)
    //     {
    //         var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
    //         var result = await _userManager.CreateAsync(user, Input.Password);
    //         if (result.Succeeded)
    //         {
    //             //  await _signInManager.SignInAsync(user, isPersistent: true);
    //
    //             _logger.LogInformation($"User {Input.Email} create a new account with password.");
    //             return RedirectToPage("RegisterSuccess", new { email = Input.Email });
    //         }
    //
    //         foreach (var error in result.Errors)
    //         {
    //             ModelState.AddModelError(string.Empty, error.Description);
    //         }
    //     }
    //
    //     return Page();
    // }

    public void OnGet()
    {
    }
}