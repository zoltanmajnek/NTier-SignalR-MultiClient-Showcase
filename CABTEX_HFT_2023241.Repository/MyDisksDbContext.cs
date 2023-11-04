using CABTEX_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;

namespace CABTEX_HFT_2023241.Repository
{
    internal class MyDisksDbContext : DbContext
    {
        public DbSet<DiskType> DiskTypes { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<MyDisk> MyDisks { get; set; }
        public DbSet<DiskDirectory> DiskDir { get; set; }

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
                new DiskType("DS/DD#Philips#880"),
            });

            modelBuilder.Entity<Friend>().HasData(new Friend[]
            {
                new Friend("Én#0#Itthon"),
                new Friend("Karcsi#+36-555#Hársfa u. 2"),
                new Friend("Gyuri#+36-123#Fő u. 3"),
                new Friend("Béla#+06-155#Szab u. 1"),
                new Friend("Timi#+36-121#Szab u. 13"),
                new Friend("Niki#+36-557#Hársfa u. 2"),
                new Friend("Juli#+36-623#Fő u. 5"),
                new Friend("Árpi#+36-178#Dobó u. 12"),
                new Friend("Fecó#+36-333#Fő u. 51"),
            });
            
            modelBuilder.Entity<MyDisk>().HasData(new MyDisk[]
            {
                new MyDisk("1#Mindenféle"),
                new MyDisk("2#LastNinja"),
                new MyDisk("3#Demos"),
                new MyDisk("4#MyStuffs"),
                new MyDisk("5#ManiacMansion"),
                new MyDisk("6#ZacMcCrackenD1"),
                new MyDisk("7#ZacD2"),
                new MyDisk("8#Montys"),
                new MyDisk("9#Others"),
                new MyDisk("10#Utilities"),
            });
            
            modelBuilder.Entity<Software>().HasData(new Software[]
            {
                new Software("1#Maniac Mansion#LucasFilm Games#Adventure#1200"),
                new Software("2#Zac McCracken#LucasFilm Games#Adventure#2500"),
                new Software("3#Bard's Tale III#Interplay#RPG#150"),
                new Software("4#Monty on the Run#Gremlin Graphics#Arcade#150"),                
                new Software("5#Auf widersehen Monty#Gremlin Graphics#Arcade#200"),
                new Software("6#Last Ninja#System 3#Arcade#900"),
                new Software("7#Disk Manager#Unknown#Utility#800"),
                new Software("8#HexMon#Admin#Utility#50"),
                new Software("9#Intro Maker#Maniacs of Noise#Demo#500"),
                new Software("10#Elite#David Braben#Simulation#300"),
            });



        }
    }
}
