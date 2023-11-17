using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public interface IDiskDirectoryLogic
    {
        void Create(DiskDirectory item);
        void Delete(int id);
        DiskDirectory Read(int id);
        IQueryable<DiskDirectory> ReadAll();
        void Update(DiskDirectory item);
    }
}
