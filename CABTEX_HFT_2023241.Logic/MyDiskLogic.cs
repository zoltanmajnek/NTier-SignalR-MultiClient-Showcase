using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public class MyDiskLogic : IMyDiskLogic
    {
        IRepository<MyDisk> repo;

        public MyDiskLogic(IRepository<MyDisk> repo)
        {
            this.repo = repo;
        }

        public void Create(MyDisk item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public MyDisk Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<MyDisk> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(MyDisk item)
        {
            this.repo.Update(item);
        }
    }
}
