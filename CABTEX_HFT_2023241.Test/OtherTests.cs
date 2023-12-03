using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Repository;
using NUnit.Framework;
using System;
using System.Linq;

namespace CABTEX_HFT_2023241.Test
{
    //With mock, I can't test model connections, so complex query which needs it isn't working there.
    //For example to calculate free space on a disk, i need disk, disktype (max capacity), software(size) and directory (sw list).

    [TestFixture]
    public class OtherTests
    {
        MyDiskLogic myDiskLogic;
        DiskTypeLogic diskTypeLogic;
        SoftwareLogic softwareLogic;
        FriendLogic friendLogic;
        DiskDirectoryLogic diskDirectoryLogic;

        MyDisksDbContext db;
        SoftwareRepository sw;
        FriendRepository f;
        DiskDirectoryRepository ddir;
        MyDiskRepository MyD;
        DiskTypeRepository dtype;

        [SetUp]
        public void Init() 
        {
            db = new MyDisksDbContext();
            sw = new SoftwareRepository(db);
            f = new FriendRepository(db);
            ddir = new DiskDirectoryRepository(db);
            MyD = new MyDiskRepository(db);
            dtype = new DiskTypeRepository(db);

            softwareLogic = new SoftwareLogic(sw);
            friendLogic = new FriendLogic(f, sw, MyD);
            diskDirectoryLogic = new DiskDirectoryLogic(ddir);
            myDiskLogic = new MyDiskLogic(MyD, sw);
            diskTypeLogic = new DiskTypeLogic(dtype);
        }

        [Test]
        public void WhereAreMySoftwaresTest()
        {
            Assert.That(() => myDiskLogic.Read(15), Throws.TypeOf<ArgumentException>());

            var WhichDisksContainsHexmon = myDiskLogic.ContainsaGivenSoftware("HexMon").ToList();
            Assert.That(WhichDisksContainsHexmon.Count, Is.EqualTo(3));

            var WhoHasLastNinja = friendLogic.WhoHasThisSoftware("Last Ninja").ToList();
            Assert.That(WhoHasLastNinja.First().Name, Is.EqualTo("Gyuri"));

            var ThisShouldbeNull = friendLogic.WhoHasThisSoftware("nothinghere").ToList();
            Assert.That(ThisShouldbeNull.Any, Is.EqualTo(false));

            var WhoHasHexmon = friendLogic.WhoHasThisSoftware("hexmon").ToList();
            Assert.That(WhoHasHexmon.Count, Is.EqualTo(2));
        }

        [Test]
        public void FreeSpaceOnDiskTest()
        {
            //First disk has 780 blocks total (decided by Type), has 3 software: 150+50+300, free space should be 780-500
            int? freeSpaceOnDisk1 = myDiskLogic.FreeSpaceOnTheDisk(1);
            Assert.That(freeSpaceOnDisk1, Is.EqualTo(280));
        }

        [Test]
        public void WhoHasTheMostDisksTest()
        {
            var whoIsIt = friendLogic.WhoHasTheMostDisks();
            Assert.That(whoIsIt.Name, Is.EqualTo("Én"));
            Assert.That(whoIsIt.Disks.Count, Is.EqualTo(5));
        }
    }
}
