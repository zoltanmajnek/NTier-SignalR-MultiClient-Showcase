using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public class DiskDirectoryLogic : IDiskDirectoryLogic
    {
        IRepository<DiskDirectory> repo;

        public DiskDirectoryLogic(IRepository<DiskDirectory> repo)
        {
            this.repo = repo;
        }

        public void Create(DiskDirectory item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public DiskDirectory Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<DiskDirectory> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(DiskDirectory item)
        {
            this.repo.Update(item);
        }
    }
}
