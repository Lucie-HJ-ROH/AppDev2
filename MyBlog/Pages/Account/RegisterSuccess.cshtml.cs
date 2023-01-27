using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyBlog.Pages;

public class RegisterSuccess : PageModel
{
    public string Email { get; set; }
    public void OnGet()
    {
        
    }
}