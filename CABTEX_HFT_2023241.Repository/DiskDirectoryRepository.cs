using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Repository
{
    public class DiskDirectoryRepository : Repository<DiskDirectory>, IRepository<DiskDirectory>
    {
        public DiskDirectoryRepository(MyDisksDbContext context) : base(context) { }

        public override DiskDirectory Read(int id)
        {
            return context.DiskDir.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(DiskDirectory item)
        {
            var old = Read(item.Id);
            if (old != null)
            {
                foreach (var prop in old.GetType().GetProperties())
                {
                    if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null) prop.SetValue(old, prop.GetValue(item));
                }
                context.SaveChanges();
            }
        }
    }
}
