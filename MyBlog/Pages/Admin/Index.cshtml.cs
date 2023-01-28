using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MyBlog.Pages;

[Authorize(Roles="Admin")]
public class AdminIndex : PageModel
{
    public void OnGet()
    {
        
    }
}