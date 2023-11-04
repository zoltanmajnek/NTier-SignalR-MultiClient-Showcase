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
                new DiskType("DS/DD#Philips+#880"),
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
                new Software("Maniac Mansion#LucasFilm Games#1200"),
                new Software("Zac McCracken#LucasFilm Games#2500"),
                new Software("Monty on the Run#Gremlin Graphics#150"),
                new Software("Auf widersehen Monty#Gremlin Graphics#200"),
                new Software("Last Ninja#System 3#900"),
                new Software("Disk Manager#Util#800"),
                new Software("HexMon#Util#50"),
                new Software("Intro Maker#Maniacs of Noise#500"),
                new Software("Elite#David Braben#300"),
            });
        }
    }
}
