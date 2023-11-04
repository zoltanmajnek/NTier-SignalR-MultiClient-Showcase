using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Xml.Linq;

namespace CABTEX_HFT_2023241.Models
{
    public class DiskDirectory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public virtual MyDisk MyDisk { get; set; }
        [Required]
        [ForeignKey(nameof(MyDisk))]
        public int DiskId { get; set; }

        public virtual Software Software { get; set; }
        [Required]
        [ForeignKey(nameof(Software))]
        public int SoftwareId { get; set; }

        public DiskDirectory()
        {
            Id = Guid.NewGuid().ToString();
        }

        public DiskDirectory(string line)
        {
            Id = Guid.NewGuid().ToString();

            string[] split = line.Split('#');
            DiskId = int.Parse(split[0]);
            SoftwareId = int.Parse(split[1]);
        }
    }
}
