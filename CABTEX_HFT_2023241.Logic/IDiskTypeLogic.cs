using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public interface IDiskTypeLogic
    {
        void Create(DiskType item);
        void Delete(int id);
        DiskType Read(int id);
        IQueryable<DiskType> ReadAll();
        void Update(DiskType item);

        public string LargestDiskVendorName();
    }
}
