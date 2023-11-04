using CABTEX_HFT_2023241.Repository;
using NUnit.Framework;

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
        }
    }
}
