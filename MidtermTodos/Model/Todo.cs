using Microsoft.AspNetCore.Identity;

namespace MidtermTodos.Model;

public class Todo
{
    public int Id { get; set; }
    public IdentityUser Owner{ get; set; }// required, never null
    public string Task{ get; set; } // required, never null, 1-200 characters
    public DateTime DueDate{ get; set; } // required, never null, year in 2000-2099 range
    public bool IsDone{ get; set; }
}