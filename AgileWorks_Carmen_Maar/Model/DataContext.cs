namespace AgileWorks_Carmen_Maar.Model;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) {}

    public DbSet<Inquiry> InquiryList { get; set; }
    public DbSet<InsertionTime> InsertionTimeList { get; set; }
    public DbSet<DeadLine> DeadLineList { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
            
        modelBuilder.Entity<Inquiry>().ToTable("Inquiry").HasKey(x => x.Id);
        modelBuilder.Entity<InsertionTime>().ToTable("InsertionTime").HasKey(x => x.Time);
        modelBuilder.Entity<DeadLine>().ToTable("DeadLine").HasKey(x => x.DeadLineTime);
            
        modelBuilder.Entity<Inquiry>().HasData(
            new Inquiry
            {
                Id = 1,
                Description = "Veebilehe arendus",
                InsertionTime = new DateTime(2024, 3, 2, 13, 43, 55),
                Deadline = new DateTime(2024, 4, 13, 19, 0, 0)
            },
            new Inquiry
            {
                Id = 2,
                Description = "Powerpoint esitlus",
                InsertionTime = new DateTime(2024, 3, 21, 21, 0, 0),
                Deadline = new DateTime(2024, 3, 26, 10, 30, 0)
            }
        );
    }
}