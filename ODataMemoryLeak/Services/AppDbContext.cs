using Microsoft.EntityFrameworkCore;
using ODataMemoryLeak.Services.Entities;

namespace ODataMemoryLeak.Services;

public class AppDbContext : DbContext
{
    public DbSet<HumanEntity> Humans { get; set; }
    public DbSet<HumanTableEntity> HumanTables { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        var table1 = new HumanTableEntity() { Id = 1, Code = "ABC" };
        var table2 = new HumanTableEntity() { Id = 2, Code = "EFD" };
        var table3 = new HumanTableEntity() { Id = 3, Code = "ABC" };
        var table4 = new HumanTableEntity() { Id = 4, Code = "ABC4+ Dan" };
        AddIfNotExist(table1);
        AddIfNotExist(table2);
        AddIfNotExist(table3);
        AddIfNotExist(table4);

        AddIfNotExist(new HumanEntity()
        {
            Description = "Blue d",
            Name = "John's",
            Id = 1,
            HumanTableId = table1.Id,
            HumanTable = table1,
            Code = "Human code"
        });


        AddIfNotExist(new HumanEntity()
        {
            Id = 2,
            Name = "Marry",
            Description = "Loves walk",
            HumanTableId = table2.Id,
            HumanTable = table2,
            Code = "Human code 2"
        });

        AddIfNotExist(new HumanEntity()
        {
            Id = 3,
            Name = "Steve",
            Description = "Sand drinks",
        });

        SaveChanges();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "testdb", opt => { opt.EnableNullChecks(false); });
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<HumanTableEntity>();
        modelBuilder.Entity<HumanEntity>()
            .HasOne(x => x.HumanTable)
            .WithMany()
            .HasForeignKey(x => x.HumanTableId);
    }

    private void AddIfNotExist(HumanTableEntity humanTableEntity)
    {
        var exist = this.Set<HumanTableEntity>()
            .FirstOrDefault(x => x.Id == humanTableEntity.Id);
        if (exist == null)
        {
            Add(humanTableEntity);
        }
    }

    private void AddIfNotExist(HumanEntity humanEntity)
    {
        var exist = this.Set<HumanEntity>()
            .FirstOrDefault(x => x.Id == humanEntity.Id);
        if (exist == null)
        {
            Add(humanEntity);
        }

    }
}