using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MidtermTodos.Data;

namespace MidtermTodos.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly TodoDbContext _context;
    public IList<Model.Todo> Todo { get;set; } = default!;
    
    public IndexModel(TodoDbContext context, ILogger<IndexModel> logger)
    {
        _logger = logger;
        _context = context;
    }


    public async Task OnGetAsync()
    {
        if (_context.Todos != null)
        {
            Todo = await _context.Todos.Include("Owner").ToListAsync();
        }
    }
}