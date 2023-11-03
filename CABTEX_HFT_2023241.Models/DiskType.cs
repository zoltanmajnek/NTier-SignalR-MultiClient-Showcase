using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CABTEX_HFT_2023241.Models
{
    public class DiskType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public int Capacity { get; set; }
        public string VendorName { get; set; }

        public DiskType()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
