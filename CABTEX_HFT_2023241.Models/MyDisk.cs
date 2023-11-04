using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CABTEX_HFT_2023241.Models
{
    public class MyDisk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(240)]
        public string Label { get; set; }

        public virtual Friend Friend { get; set; }
        public virtual DiskType DiskType { get; set; }

        [ForeignKey(nameof(Friend))]
        public string FriendId { get; set; }

        [ForeignKey(nameof(DiskType))]
        public string TypeId { get; set; }

        public MyDisk() { }

        public MyDisk(string line)
        {
            string[] split = line.Split('#');
            Id = int.Parse(split[0]);
            Label = split[1];
        }
    }
}
