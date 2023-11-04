using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CABTEX_HFT_2023241.Models
{
    public class DiskType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Capacity { get; set; }

        [StringLength(240)]
        public string VendorName { get; set; }

        [StringLength(20)]
        public string TypeName { get; set; }

        public virtual ICollection<MyDisk> Disks { get; set; }

        public DiskType(string line)
        {
            Disks = new List<MyDisk>();

            string[] split = line.Split('#');
            Id = int.Parse(split[0]);
            TypeName = split[1];
            VendorName = split[2];
            Capacity = int.Parse(split[3]);
        }

        public DiskType()
        {
            Disks = new List<MyDisk>();
        }
    }
}
