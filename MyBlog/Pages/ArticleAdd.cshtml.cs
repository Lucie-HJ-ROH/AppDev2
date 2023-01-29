using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyBlog.Data;
using MyBlog.Model;

namespace MyBlog.Pages;

[Authorize]
public class ArticleAdd : PageModel
{
    private BlogDbContext db;
    private readonly ILogger<Register> _logger;
    private IWebHostEnvironment _environment;

    public ArticleAdd(BlogDbContext db, ILogger<Register> logger, IWebHostEnvironment environment)
    {
        this.db = db;
        this._logger = logger;
        this._environment = environment;
    }

    [BindProperty, Required, MinLength(1), Display(Name = "Title")]
    public string Title { get; set; }

    [BindProperty, Required, MinLength(1), MaxLength(2000), Display(Name = "Content")]
    public string Body { get; set; }

    [BindProperty] public IFormFile Upload { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            string imagePath = null;
            if (Upload != null)
            {
                string fileExtension = Path.GetExtension(Upload.FileName).ToLower();
                string[] allowExtensions = { ".jpg", ".jpeg", ".gif", ".png" };

                if (!allowExtensions.Contains(fileExtension))
                {
                    //user does not exist , password invalid , account locked out;
                    ModelState.AddModelError(string.Empty, "Only Image Files  : (jpg, jpeg, gif, png) are allowed");
                    return Page();
                }

                var invalids = System.IO.Path.GetInvalidFileNameChars();
                var newFileName = String
                    .Join("_", Upload.FileName.Split(invalids, StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
                
                var file = Path.Combine(_environment.ContentRootPath, "wwwroot", "Uploads", newFileName);
                try
                {
                    using (var filestream = new FileStream(file, FileMode.Create))
                    {
                        await Upload.CopyToAsync(filestream);
                    }
                }
                catch (Exception e) when (e is IOException || e is SystemException)
                {
                    ModelState.AddModelError(string.Empty, "Internal error " + "saving the uploaded file");
                    return Page();
                }


                imagePath = Path.Combine("Uploads", newFileName);
            }

            var userName = User.Identity.Name;
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var newArticle = new Article
                { ImagePath = imagePath, Title = Title, Body = Body, Author = user, Created = DateTime.Now };
            db.Add(newArticle);
            await db.SaveChangesAsync();
            return RedirectToPage("ArticleAddSuccess");
        }

        return Page();
    }

    public void OnGet()
    {
    }
}