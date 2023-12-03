using CABTEX_HFT_2023241.Logic;
using CABTEX_HFT_2023241.Repository;
using NUnit.Framework;
using System;
using System.Linq;

namespace CABTEX_HFT_2023241.Test
{
    [TestFixture]
    public class TesterClass
    {
        [Test]
        public void FirstTest()
        {
            var db = new MyDisksDbContext();

            var sw = new SoftwareRepository(db);
            var f = new FriendRepository(db);
            var ddir = new DiskDirectoryRepository(db);
            var MyD = new MyDiskRepository(db);
            var dtype = new DiskTypeRepository(db);
            ;

            Assert.That(sw, Is.Not.Null);

            var sl = new SoftwareLogic(sw);
            var fl = new FriendLogic(f,sw,MyD);
            var ddirl = new DiskDirectoryLogic(ddir);
            var MyDl = new MyDiskLogic(MyD,sw);
            var dtypel = new DiskTypeLogic(dtype);

            //First disk has 780 blocks total, has 3 software: 150+50+300, free space should be 780-500
            Assert.That(MyDl.FreeSpaceOnTheDisk(1), Is.EqualTo(280));

            Assert.That(() => MyDl.Read(15), Throws.TypeOf<ArgumentException>());

            Assert.That(fl.WhoHasTheMostDisks().Name, Is.EqualTo("Én"));

            var WhichDisksContainsHexmon = MyDl.ContainsaGivenSoftware("HexMon").ToList();

            var WhoHasLastNinja = fl.WhoHasThisSoftware("Last Ninja").ToList();
            var ThisShouldbeNull = fl.WhoHasThisSoftware("nothinghere").ToList();;
            var WhoHasHexmon = fl.WhoHasThisSoftware("hexmon").ToList();
            ;

        }

        //[Test]
        //public void FirstTest()
        //{
        //}
    }
}
