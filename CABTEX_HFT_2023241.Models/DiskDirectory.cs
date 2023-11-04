using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CABTEX_HFT_2023241.Models
{
    public class DiskDirectory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual MyDisk MyDisk { get; set; }
        [Required]
        [ForeignKey(nameof(MyDisk))]
        public int DiskId { get; set; }

        public virtual Software Software { get; set; }
        [Required]
        [ForeignKey(nameof(Software))]
        public int SoftwareId { get; set; }

        public DiskDirectory() { }

        public DiskDirectory(string line)
        {
            string[] split = line.Split('#');
            Id = int.Parse(split[0]);
            DiskId = int.Parse(split[1]);
            SoftwareId = int.Parse(split[2]);
        }
    }
}
