using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public class DiskTypeLogic : IDiskTypeLogic
    {
        IRepository<DiskType> repo;

        public DiskTypeLogic(IRepository<DiskType> repo)
        {
            this.repo = repo;
        }

        public void Create(DiskType item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public DiskType Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<DiskType> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(DiskType item)
        {
            this.repo.Update(item);
        }
    }
}
