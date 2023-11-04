using CABTEX_HFT_2023241.Models;
using Microsoft.EntityFrameworkCore;

namespace CABTEX_HFT_2023241.Repository
{
    public class MyDisksDbContext : DbContext
    {
        public DbSet<DiskType> DiskTypes { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<MyDisk> MyDisks { get; set; }
        public DbSet<DiskDirectory> DiskDir { get; set; }
        public DbSet<Software> Softwares { get; set; }

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
            modelBuilder.Entity<MyDisk>(type => type
                .HasOne(type => type.DiskType)
                .WithMany(disk => disk.Disks)
                .HasForeignKey(type => type.TypeId)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<MyDisk>()
                .HasMany(x => x.Softwares)
                .WithMany(x => x.Disks)
                .UsingEntity<DiskDirectory>(
                    x => x.HasOne(x => x.Software)
                        .WithMany().HasForeignKey(x => x.SoftwareId).OnDelete(DeleteBehavior.Cascade),
                    x => x.HasOne(x => x.MyDisk)
                        .WithMany().HasForeignKey(x => x.DiskId).OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<MyDisk>(friend => friend
                .HasOne(disk => disk.Friend)
                .WithMany(friend => friend.Disks)
                .HasForeignKey(disk => disk.FriendId)
                .OnDelete(DeleteBehavior.Cascade));

            modelBuilder.Entity<DiskType>().HasData(new DiskType[]
            {
                new DiskType("1#DS/DD#TDK#780"),
                new DiskType("2#DS/HD#TDK+#1440"),
                new DiskType("3#DS/DD#Philips#880"),
            }); //ID#Type#Vendor#Capacity

            modelBuilder.Entity<Friend>().HasData(new Friend[]
            {
                new Friend("1#Én#+36-007#Itthon"),
                new Friend("2#Karcsi#+36-555#Hársfa u. 2"),
                new Friend("3#Gyuri#+36-123#Fő u. 3"),
                new Friend("4#Béla#+06-155#Szab u. 1"),
                new Friend("5#Timi#+36-121#Szab u. 13"),
                new Friend("6#Niki#+36-557#Hársfa u. 2"),
                new Friend("7#Juli#+36-623#Fő u. 5"),
                new Friend("8#Árpi#+36-178#Dobó u. 12"),
                new Friend("9#Fecó#+36-333#Fő u. 51"),
            }); //#Id#Name#Tel#Address

            modelBuilder.Entity<MyDisk>().HasData(new MyDisk[]
            {
                new MyDisk("1#Mindenféle#5#1"),
                new MyDisk("2#LastNinja#3#2"),
                new MyDisk("3#Demos#1#1"),
                new MyDisk("4#MyStuffs#1#2"),
                new MyDisk("5#ManiacMansion#1#2"),
                new MyDisk("6#ZacMcCrackenD1#8#2"),
                new MyDisk("7#ZacD2#8#2"),
                new MyDisk("8#Montys#9#1"),
                new MyDisk("9#Others#1#1"),
                new MyDisk("10#Utilities#1#3"),
            }); //ID#Label#FriendId#DiskTypeId

            modelBuilder.Entity<Software>().HasData(new Software[]
            {
                new Software("1#Maniac Mansion#LucasFilm Games#Adventure#1200"),
                new Software("2#Zac McCracken#LucasFilm Games#Adventure#2500"),
                new Software("3#Bard's Tale III#Interplay#RPG#550"),
                new Software("4#Monty on the Run#Gremlin Graphics#Arcade#150"),
                new Software("5#Auf widersehen Monty#Gremlin Graphics#Arcade#200"),
                new Software("6#Last Ninja#System 3#Arcade#900"),
                new Software("7#Disk Manager#Unknown#Utility#800"),
                new Software("8#HexMon#Admin#Utility#50"),
                new Software("9#Intro Maker#Maniacs of Noise#Demo#500"),
                new Software("10#Elite#David Braben#Space Sim#300"),
            }); //ID#SWName#Creator#Genre#Size

            modelBuilder.Entity<DiskDirectory>().HasData(new DiskDirectory[]
            {
                new DiskDirectory("1#1#4"),
                new DiskDirectory("2#1#10"),
                new DiskDirectory("3#1#8"),
                new DiskDirectory("4#2#6"),
                new DiskDirectory("5#3#9"),
                new DiskDirectory("6#4#8"),
                new DiskDirectory("7#5#1"),
                new DiskDirectory("8#6#2"),
                new DiskDirectory("9#7#2"),
                new DiskDirectory("10#8#4"),
                new DiskDirectory("11#8#5"),
                new DiskDirectory("12#9#3"),
                new DiskDirectory("13#10#7"),
                new DiskDirectory("14#10#8"),
            }); //ID # DiskId # SwID
        }
    }
}
