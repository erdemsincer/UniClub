using EventService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EventService.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Event> Events => Set<Event>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
