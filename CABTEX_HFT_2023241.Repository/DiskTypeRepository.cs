using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Repository
{
    public class DiskTypeRepository : Repository<DiskType>, IRepository<DiskType>
    {
        public DiskTypeRepository(MyDisksDbContext context) : base(context) { }

        public override DiskType Read(int id)
        {
            return context.DiskTypes.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(DiskType item)
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
