using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using AssetwiseApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace AssetwiseApi.Context;
public class AWContext : IdentityDbContext
{
    public AWContext()
    {
    }

    public AWContext(DbContextOptions<AWContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Site> Sites { get; set; }

}