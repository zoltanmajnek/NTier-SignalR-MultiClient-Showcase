using CABTEX_HFT_2023241.Models;
using CABTEX_HFT_2023241.Repository;
using System;
using System.Collections.Generic;
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

        public void Create(Software software)
        {
            if (software?.Name == null || software.Name.Length < 1)
                throw new ArgumentException("Need a Label, at least 1 character.");

            this.repo.Create(software);
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

        public Software ReadByName(string swName)
        {
            swName = swName.ToUpper();
            return ReadAll().Where(s => s.Name.ToUpper().Equals(swName)).FirstOrDefault();
        }

        public IEnumerable<string> Genre(string genre)
        {
            var genres = ReadAll().Where(s => s.Genre.ToUpper() == genre.ToUpper()).Select(s => s.Name);
            if (!genres.Any()) throw new ArgumentException("No such genre");
            return genres;
        }

        public double? AvgSizeInaGenre(string genre)
        {
            var allInGen = ReadAll().Where(s => s.Genre.ToUpper() == genre.ToUpper()).Select(s => s.Size);
            if (!allInGen.Any())
            {
                if (genre=="*") allInGen = ReadAll().Select(s => s.Size);
                else throw new ArgumentException("No such genre");
            }
            return allInGen.Average();
        }
    }
}
