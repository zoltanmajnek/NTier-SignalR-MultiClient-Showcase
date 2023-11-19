using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public class SoftwareLogic : ISoftwareLogic
    {
        IRepository<Software> repo;

        public SoftwareLogic(IRepository<Software> repo)
        {
            this.repo = repo;
        }

        public void Create(Software item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Software Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Software> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Software item)
        {
            this.repo.Update(item);
        }
    }
}
