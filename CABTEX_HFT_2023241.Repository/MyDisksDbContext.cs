using CABTEX_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;

namespace CABTEX_HFT_2023241.Repository
{
    internal class MyDisksDbContext : DbContext
    {
        public DbSet<DiskType> DiskTypes { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<MyDisk> MyDisks { get; set; }
        public DbSet<DiskContainsSoftware> DiskDir { get; set; }

        public MyDisksDbContext()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("myDisksDb").UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Worker>()
            //    .HasOne(x => x.Company)
            //    .WithMany(x => x.Workers)
            //    .HasForeignKey(x => x.CompanyID)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Company>()
            //    .HasOne(x => x.Country)
            //    .WithMany(x => x.Companies)
            //    .HasForeignKey(x => x.CountryId)
            //    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<DiskType>().HasData(new DiskType[]
            {
                new DiskType("DS/DD#TDK#780"),
                new DiskType("DS/HD#TDK+#1440"),
                new DiskType("DS/DD#Philips+#880"),
            });
        }
    }
}
