using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MidtermTodos.Data;
using MidtermTodos.Pages.Account;

namespace MidtermTodos.Pages.Todo;
[Authorize]
public class TodoAdd : PageModel
{
    
    private TodoDbContext db;
    private readonly ILogger<Register> _logger;

    public TodoAdd(TodoDbContext db, ILogger<Register> logger)
    {
        this.db = db;
        this._logger = logger;
    }
    [BindProperty, Required, MinLength(1),MaxLength(200), Display(Name = "Task")]
    public string Task { get; set; }
    
    [BindProperty]
    public DateTime DueDate { get; set; }
    
    [BindProperty]
    public bool IsDone { get; set; }

    
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
           
            var userName = User.Identity.Name;
            var user = db.Users.Where(u => u.UserName == userName).FirstOrDefault();
            var newTodo = new Model.Todo
                {Task = Task, DueDate = DueDate, Owner = user, IsDone = IsDone};
            db.Add(newTodo);
            await db.SaveChangesAsync();
            return RedirectToPage("TodoAddSuccess");
        }

        return Page();
    }
    
    
    public void OnGet()
    {
        
    }
}