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

        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public virtual ICollection<MyDisk> Disks { get; set; }

        public Friend()
        {
            Id = Guid.NewGuid().ToString();
            Disks = new List<MyDisk>();
        }
    }
}
