using MembershipService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MembershipService.Persistence.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Membership> Memberships => Set<Membership>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
