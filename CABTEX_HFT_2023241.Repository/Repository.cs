using System.Linq;

namespace CABTEX_HFT_2023241.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected MyDisksDbContext context;
        public Repository(MyDisksDbContext context)
        {
            this.context = context;
        }
        public void Create(T item)
        {
            context.Set<T>().Add(item);
            context.SaveChanges();
        }

        public IQueryable<T> ReadAll()
        {
            return context.Set<T>();
        }

        public void Delete(int id)
        {
            context.Set<T>().Remove(Read(id));
            context.SaveChanges();
        }

        public abstract T Read(int id);
        public abstract void Update(T item);
    }
}
