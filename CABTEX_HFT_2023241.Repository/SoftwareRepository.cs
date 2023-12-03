using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Repository
{
    public class SoftwareRepository : Repository<Software>, IRepository<Software>
    {
        public SoftwareRepository(MyDisksDbContext context) : base(context) { }

        public override Software Read(int id)
        {
            return context.Softwares.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Software item)
        {
            var old = Read(item.Id);
            foreach (var prop in old.GetType().GetProperties())
            {
                if (prop.GetAccessors().FirstOrDefault(t => t.IsVirtual) == null) prop.SetValue(old, prop.GetValue(item));
            }
            context.SaveChanges();
        }
    }
}
