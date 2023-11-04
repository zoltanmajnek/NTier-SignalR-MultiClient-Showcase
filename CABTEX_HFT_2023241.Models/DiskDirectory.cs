using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CABTEX_HFT_2023241.Models
{
    public class DiskDirectory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public string Id { get; set; }

        [Required]
        [ForeignKey(nameof(MyDisk))]
        public string DiskId { get; set; }

        [Required]
        [ForeignKey(nameof(Software))]
        public string SoftwareId { get; set; }



        public DiskDirectory()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
