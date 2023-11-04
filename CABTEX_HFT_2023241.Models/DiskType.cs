using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CABTEX_HFT_2023241.Models
{
    public class DiskType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public int Capacity { get; set; }

        [StringLength(240)]
        public string VendorName { get; set; }

        [StringLength(20)]
        public string TypeName { get; set; }

        public DiskType(string line)
        {
            Id = Guid.NewGuid().ToString();
            string[] split = line.Split('#');
            VendorName = split[0];
            TypeName = split[1];
            Capacity = int.Parse(split[2]);
        }

        public DiskType()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
