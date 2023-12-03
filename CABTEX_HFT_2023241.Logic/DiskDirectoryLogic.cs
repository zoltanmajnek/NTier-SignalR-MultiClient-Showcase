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
            repo.Create(item);
        }

        public void Delete(int id)
        {
            repo.Delete(id);
        }

        public DiskDirectory Read(int id)
        {
            return repo.Read(id);
        }

        public IQueryable<DiskDirectory> ReadAll()
        {
            return repo.ReadAll();
        }

        public void Update(DiskDirectory item)
        {
            repo.Update(item);
        }

        public int? DiskIdWithMaxSoftware()
        {
            var result = ReadAll().GroupBy(d=> d.DiskId)
                           .Select(group => new
                           {
                               DiskId = group.Key,
                               Count = group.Count()
                           })
                           .OrderByDescending(group => group.Count)
                           .FirstOrDefault();
            return result?.DiskId;
        }

    }
}
