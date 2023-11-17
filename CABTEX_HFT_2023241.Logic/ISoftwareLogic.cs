using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public interface ISoftwareLogic
    {
        void Create(Software item);
        void Delete(int id);
        Software Read(int id);
        IQueryable<Software> ReadAll();
        void Update(Software item);
    }
}
