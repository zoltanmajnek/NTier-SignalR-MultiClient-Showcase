using CABTEX_HFT_2023241.Models;
using System.Collections.Generic;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public interface IFriendLogic
    {
        void Create(Friend item);
        void Delete(int id);
        Friend Read(int id);
        IQueryable<Friend> ReadAll();
        void Update(Friend item);
        Friend WhoHasTheMostDisks();
        IEnumerable<Friend> WhoHasThisSoftware(string swName);
    }
}
