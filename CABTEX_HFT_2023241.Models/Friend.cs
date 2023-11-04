using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace CABTEX_HFT_2023241.Models
{
    public class Friend
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id { get; set; }

        [StringLength(240)]
        public string Name { get; set; }

        [StringLength(240)]
        public string PhoneNumber { get; set; }

        [StringLength(240)]
        public string Address { get; set; }

        public virtual ICollection<MyDisk> Disks { get; set; }

        public Friend()
        {
            Id = Guid.NewGuid().ToString();
            Disks = new List<MyDisk>();
        }

        public Friend(string line)
        {
            Id = Guid.NewGuid().ToString();
            Disks = new List<MyDisk>();

            string[] split = line.Split('#');
            Name = split[0];
            PhoneNumber = split[1];
            Address = split[2];
        }
    }
}
