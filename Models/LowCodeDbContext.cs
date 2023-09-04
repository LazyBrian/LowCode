using LowCode.Models;
using Microsoft.EntityFrameworkCore;
using Attribute = LowCode.Models.Attribute;

public class LowCodeDbContext : DbContext
{
    public LowCodeDbContext(DbContextOptions<LowCodeDbContext> options)
        : base(options)
    {

    }
    public DbSet<Entity> Entities { get; set; }

    public DbSet<Attribute> Attributes { get; set; }

}