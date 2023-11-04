using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Repository
{
    public class MyDiskRepository : Repository<MyDisk>, IRepository<MyDisk>
    {
        public MyDiskRepository(MyDisksDbContext context) : base(context) { }

        public override MyDisk Read(int id)
        {
            return context.MyDisks.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(MyDisk item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                prop.SetValue(old, prop.GetValue(item));
            }
            context.SaveChanges();
        }
    }
}
