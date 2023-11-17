using CABTEX_HFT_2023241.Models;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public interface IMyDiskLogic
    {
        void Create(MyDisk item);
        void Delete(int id);
        MyDisk Read(int id);
        IQueryable<MyDisk> ReadAll();
        void Update(MyDisk item);
    }
}
