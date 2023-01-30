using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MidtermTodos.Model;

namespace MidtermTodos.Data;

public class TodoDbContext : IdentityDbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) {}

    public DbSet<Todo> Todos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data source=Todos.db");
    }
}