using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System.Collections.Generic;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public class FriendLogic : IFriendLogic
    {
        IRepository<Friend> repo;

        public FriendLogic(IRepository<Friend> repo)
        {
            this.repo = repo;
        }

        public void Create(Friend item)
        {
            this.repo.Create(item);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public Friend Read(int id)
        {
            return this.repo.Read(id);
        }

        public IQueryable<Friend> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(Friend item)
        {
            this.repo.Update(item);
        }

        public Friend WhoHasTheMostDisks()
        {
            var friends = ReadAll();
            int maxDisk = friends.Max(f => f.Disks.Count);
            return friends.First(f => f.Disks.Count == maxDisk);
        }

        public List<Friend> WhoHasThisSoftware(string swName, MyDiskLogic dL, SoftwareLogic sL)
        {
            var friendList = new List<Friend>();
            foreach (var disk in dL.ContainsaGivenSoftware(swName, sL)) friendList.Add(Read(disk.FriendId));

            return friendList.Distinct().ToList();
        }

    }
}
