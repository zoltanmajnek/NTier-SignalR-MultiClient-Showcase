using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;
using System.Text.Json.Serialization;

namespace CABTEX_HFT_2023241.Models
{
    public class Friend
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [StringLength(240)]
        public string Name { get; set; }

        [StringLength(240)]
        public string PhoneNumber { get; set; }

        [StringLength(240)]
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<MyDisk> Disks { get; set; }

        public Friend()
        {
            Disks = new List<MyDisk>();
        }

        public Friend(string line)
        {
            Disks = new List<MyDisk>();

            string[] split = line.Split('#');
            Id = int.Parse(split[0]);
            Name = split[1];
            PhoneNumber = split[2];
            Address = split[3];
        }
    }
}
