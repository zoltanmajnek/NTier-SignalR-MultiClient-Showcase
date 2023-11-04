using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace CABTEX_HFT_2023241.Models
{
    public class MyDisk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(240)]
        public string Label { get; set; }

        [ForeignKey(nameof(Friend))]
        public int FriendId { get; set; }
        public virtual Friend Friend { get; set; }

        [ForeignKey(nameof(DiskType))]
        public int TypeId { get; set; }
        public virtual DiskType DiskType { get; set; }

        public virtual ICollection<Software> Softwares { get; set; }
        public virtual ICollection<DiskDirectory> DiskDirectories { get; set; }

        public MyDisk() { }

        public MyDisk(string line)
        {
            string[] split = line.Split('#');
            Id = int.Parse(split[0]);
            Label = split[1];
            FriendId = int.Parse(split[2]);
            TypeId = int.Parse(split[3]);
        }
    }
}
