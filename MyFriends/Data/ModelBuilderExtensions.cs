using Microsoft.EntityFrameworkCore;
using MyFriends.Models;

namespace MyFriends.Data;

public static class ModelBuilderExtensions
{
    public static ModelBuilder Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Friend>().HasData(
            new Friend
            {
                Id = 1,
                Name = "Jonny",
                Age = 5
            },
            new Friend
            {
                Id = 2,
                Name = "Ponny",
                Age = 6
            }
        );
        return modelBuilder;
    }
}