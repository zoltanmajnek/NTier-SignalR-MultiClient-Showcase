using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System;
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

        public void Create(DiskType dType)
        {
            if (dType.Capacity <= 0)
                throw new ArgumentException("Wrong disk capacity given.");

            this.repo.Create(dType);
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

        public string LargestDiskVendorName()
            => repo.ReadAll().Where(dt => dt.Capacity == repo.ReadAll().Max(dt => dt.Capacity))
            .Select(dt=>dt.VendorName).FirstOrDefault();
    }
}
