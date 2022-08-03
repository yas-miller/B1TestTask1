using B1TestTask1.Models;
using Microsoft.EntityFrameworkCore;

namespace B1TestTask1;

public class ApplicationDBContext: DbContext
{
    public DbSet<CustomRecord5> CustomRecords { get; set; }

    public ApplicationDBContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "Server=127.0.0.1;Port=3306;Uid=root;Pwd=11111111;Database=b1testtask1;",
            new MySqlServerVersion(new Version(8, 0, 30)),
            options => options.EnableRetryOnFailure());
    }
}