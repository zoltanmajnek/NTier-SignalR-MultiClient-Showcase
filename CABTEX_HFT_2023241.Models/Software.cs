using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CABTEX_HFT_2023241.Models
{
    public class Software
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        public string Name { get; set; }
        public string Creator { get; set; }
        public string Genre { get; set; }
        public int Size { get; set; }

        public virtual ICollection<DiskDirectory> DiskDirectories { get; set; }

        public Software() { }

        public Software(string line)
        {
            string[] split = line.Split('#');
            Id = int.Parse(split[0]);
            Name = split[1];
            Creator = split[2];
            Genre = split[3];
            Size = int.Parse(split[4]);
        }
    }
}
//Many disks can contain the same software and 1 disk contains a lot of software