using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CABTEX_HFT_2023241.Logic
{
    public class MyDiskLogic : IMyDiskLogic
    {
        IRepository<MyDisk> repo;

        public MyDiskLogic(IRepository<MyDisk> repo)
        {
            this.repo = repo;
        }

        public void Create(MyDisk disk)
        {
            if (disk?.Label==null || disk.Label.Length<2)
                throw new ArgumentException("Need a Label, at least 2 char long.");

            this.repo.Create(disk);
        }

        public void Delete(int id)
        {
            this.repo.Delete(id);
        }

        public MyDisk Read(int id)
        {
            var disk = this.repo.Read(id);
            if (disk == null)
            {
                throw new ArgumentException($"{id}. disk not exists");
            }
            return disk;
        }

        public IQueryable<MyDisk> ReadAll()
        {
            return this.repo.ReadAll();
        }

        public void Update(MyDisk item)
        {
            this.repo.Update(item);
        }

        public int FreeSpaceOnTheDisk(int id)
        {
            var disk = Read(id);
            int blocksFree = disk.DiskType.Capacity - disk.Softwares.Sum(s => s.Size);
            if (blocksFree < 0) blocksFree = 0; //if a software doesn't fit on a single disk, free space is zero not negative
            return blocksFree;
        }

        public IEnumerable<MyDisk> ContainsaGivenSoftware(string swName, SoftwareLogic sL)
        {
            return ReadAll().Where(d => d.Softwares.Contains(sL.ReadByName(swName)));
        }
    }
}
