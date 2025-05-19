using ClubService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClubService.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Club> Clubs => Set<Club>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}
