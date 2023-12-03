using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public class FriendLogic : IFriendLogic
    {
        IRepository<Friend> friendRepo;
        IRepository<Software> swRepo;
        IRepository<MyDisk> diskRepo;

        public FriendLogic(IRepository<Friend> friendRepo, IRepository<Software> swRepo, IRepository<MyDisk> diskRepo)
        {
            this.friendRepo = friendRepo;
            this.swRepo = swRepo;
            this.diskRepo = diskRepo;
        }

        public void Create(Friend friend)
        {
            if (friend?.Name == null || friend.Name.Length < 2)
                throw new ArgumentException("Need a Name, at least 2 char long.");

            this.friendRepo.Create(friend);
        }

        public void Delete(int id)
        {
            this.friendRepo.Delete(id);
        }

        public Friend Read(int id)
        {
            return this.friendRepo.Read(id);
        }

        public IQueryable<Friend> ReadAll()
        {
            return this.friendRepo.ReadAll();
        }

        public void Update(Friend item)
        {
            this.friendRepo.Update(item);
        }

        public Friend WhoHasTheMostDisks()
        {
            var friends = ReadAll();
            int maxDisk = friends.Max(f => f.Disks.Count);
            return friends.First(f => f.Disks.Count == maxDisk);
        }

        public IEnumerable<Friend> WhoHasThisSoftware(string swName)
        {
            //Először sw és disk logic paraméterben átadásával meghívtam a már kész logicokat,
            //de inkább csak a repokat ismerheti a logic, más logic-ot nem?
            swName = swName.ToUpper();
            var software = swRepo.ReadAll().Where(s => s.Name.ToUpper().Equals(swName)).FirstOrDefault();
            var whichdisks = diskRepo.ReadAll().Where(d => d.Softwares.Contains(software));            
            return whichdisks.Select(d => d.Friend).Distinct();
        }

        public IEnumerable<string> WhoLiveInMyStreet(string address)
        {
            return ReadAll().Where(f=>f.Address.Contains(address)).Select(f=>f.Name);
        }
    }
}
