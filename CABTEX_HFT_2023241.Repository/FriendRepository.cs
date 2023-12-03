using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Repository
{
    public class FriendRepository : Repository<Friend>, IRepository<Friend>
    {
        public FriendRepository(MyDisksDbContext context) : base(context) { }

        public override Friend Read(int id)
        {
            return context.Friends.FirstOrDefault(t => t.Id == id);
        }

        public override void Update(Friend item)
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
