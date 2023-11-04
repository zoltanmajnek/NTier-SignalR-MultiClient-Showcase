using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CABTEX_HFT_2023241.Models
{
    public class Software
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id { get; set; }

        public string Name { get; set; }
        public string Creator { get; set; }
        public int Size { get; set; }

        public Software()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Software(string line)
        {
            Id = Guid.NewGuid().ToString();

            string[] split = line.Split('#');
            Name = split[0];
            Creator = split[1];
            Size = int.Parse(split[2]);
        }
    }
}
//Many disks can contain the same software and 1 disk contains a lot of software