using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CABTEX_HFT_2023241.Test
{
    [TestFixture]
    public class MockTests
    {
        MyDiskLogic diskLogic;
        DiskTypeLogic diskTypeLogic;
        SoftwareLogic softwareLogic;
        FriendLogic friendLogic;
        DiskDirectoryLogic diskDirectoryLogic;

        Mock<IRepository<MyDisk>> mockMyDiskRepo;
        Mock<IRepository<DiskType>> mockDiskTypeRepo;
        Mock<IRepository<Software>> mockSoftwareRepo;
        Mock<IRepository<Friend>> mockFriendRepo;
        Mock<IRepository<DiskDirectory>> mockDiskDirectoryRepo;

        [SetUp]
        public void Init()
        {
            var disks = new List<MyDisk>()
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
            };

            var types = new List<DiskType>()
            {
                new DiskType("1#DS/DD#TDK#780"),
                new DiskType("2#DS/HD#TDK+#1440"),
                new DiskType("3#DS/DD#Philips#880"),
            };

            var softwares = new List<Software>()
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
            };

            var friends = new List<Friend>()
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
            };

            var directories = new List<DiskDirectory>() {
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
            };

            mockMyDiskRepo = new Mock<IRepository<MyDisk>>();
            mockMyDiskRepo.Setup(m => m.ReadAll()).Returns(disks.AsQueryable());
            mockMyDiskRepo.Setup(m => m.Read(It.IsAny<int>())).Returns((int id) => disks[id - 1]);

            mockDiskTypeRepo = new Mock<IRepository<DiskType>>();
            mockDiskTypeRepo.Setup(m => m.ReadAll()).Returns(types.AsQueryable());
            mockDiskTypeRepo.Setup(m => m.Read(It.IsAny<int>())).Returns((int id) => types[id - 1]);

            mockFriendRepo = new Mock<IRepository<Friend>>();
            mockFriendRepo.Setup(m => m.ReadAll()).Returns(friends.AsQueryable());
            mockFriendRepo.Setup(m => m.Read(It.IsAny<int>())).Returns((int id) => friends[id - 1]);

            mockSoftwareRepo = new Mock<IRepository<Software>>();
            mockSoftwareRepo.Setup(m => m.ReadAll()).Returns(softwares.AsQueryable());
            mockSoftwareRepo.Setup(m => m.Read(It.IsAny<int>())).Returns((int id) => softwares[id - 1]);

            mockDiskDirectoryRepo = new Mock<IRepository<DiskDirectory>>();
            mockDiskDirectoryRepo.Setup(m => m.ReadAll()).Returns(directories.AsQueryable());
            mockDiskDirectoryRepo.Setup(m => m.Read(It.IsAny<int>())).Returns((int id) => directories[id - 1]);

            diskLogic = new MyDiskLogic(mockMyDiskRepo.Object, mockSoftwareRepo.Object);
            diskTypeLogic = new DiskTypeLogic(mockDiskTypeRepo.Object);
            friendLogic = new FriendLogic(mockFriendRepo.Object, mockSoftwareRepo.Object, mockMyDiskRepo.Object);
            softwareLogic = new SoftwareLogic(mockSoftwareRepo.Object);
            diskDirectoryLogic = new DiskDirectoryLogic(mockDiskDirectoryRepo.Object);
        }

        [Test]
        public void CreateDiskWithGoodLabel()
        {
            var disk = new MyDisk() { Label = "Baldur's Gate" };
            diskLogic.Create(disk);

            mockMyDiskRepo.Verify(r => r.Create(disk), Times.Once);
        }

        [Test]
        public void CreateDiskWithWrongLabel()
        {
            var disk = new MyDisk() { Label = "y" };
            try
            {
                diskLogic.Create(disk);
            }
            catch { }

            mockMyDiskRepo.Verify(r => r.Create(disk), Times.Never);
        }

        [Test]
        public void CreateDiskTypeWithWrongCapacity()
        {
            var dType = new DiskType() { Capacity = -1 };
            try
            {
                diskTypeLogic.Create(dType);
            }
            catch { }

            mockDiskTypeRepo.Verify(r => r.Create(dType), Times.Never);
        }

        [Test]
        public void CreateGoodDiskType()
        {
            var dType = new DiskType() { Capacity = 101 };
            diskTypeLogic.Create(dType);
            mockDiskTypeRepo.Verify(r => r.Create(dType), Times.Once);
        }

        [Test]
        public void SoftwareSearchByNameTest()
        {
            var software = softwareLogic.ReadByName("Elite");
            Assert.That(software.Id, Is.EqualTo(10));
        }

        [Test]
        public void GenreSelectTest()
        {
            var swList = softwareLogic.Genre("RPG").ToList();
            Assert.That(swList.First(), Is.EqualTo("Bard's Tale III"));

            swList = softwareLogic.Genre("Adventure").ToList();
            Assert.That(swList.Count, Is.EqualTo(2));
        }

        [Test]
        public void AvgGenreSizeTest()
        {
            var avgSize = softwareLogic.AvgSizeInaGenre("Arcade");
            var avgSizeOfall = softwareLogic.AvgSizeInaGenre("*");

            Assert.That(Math.Round((decimal)avgSize), Is.EqualTo(417));
            Assert.That(Math.Round((decimal)avgSizeOfall), Is.EqualTo(715));
        }

        [Test]
        public void AvgException()
        {
            Assert.That(() => softwareLogic.AvgSizeInaGenre("Arcaade"), Throws.TypeOf<ArgumentException>());
            Assert.That(() => softwareLogic.Genre("NoSuchGenreShouldExists"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WhoLiveInThisStreetTest()
        {
            var friends = friendLogic.WhoLiveInMyStreet("Fő").ToList();
            Assert.That(friends.Count, Is.EqualTo(3));
        }

        [Test]
        public void DiskWithTheMostSoftwareTest()
        {
            var diskId = diskDirectoryLogic.DiskIdWithMaxSoftware();
            Assert.That(diskId, Is.EqualTo(1));
        }

        [Test]
        public void LargestDiskVendorName()
        {
            var vendorName = diskTypeLogic.LargestDiskVendorName();
            Assert.That(vendorName, Is.EqualTo("TDK+"));
        }
    }
}
