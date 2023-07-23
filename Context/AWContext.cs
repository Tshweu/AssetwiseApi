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
        //  Database.SetInitializer<SchoolDBContext>(new CreateDatabaseIfNotExists<SchoolDBContext>());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Site> Sites { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<ServiceComment> ServiceComments { get; set; }
    public DbSet<ServiceReminder> ServiceReminders { get; set; }
    public DbSet<ServiceDocument> ServiceDocuments { get; set; }

}