using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CABTEX_HFT_2023241.Models
{
    public class MyDisk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public int Number { get; set; }

        public virtual Friend Friend { get; set; }

        [ForeignKey(nameof(Friend))]
        public string FriendId { get; set; }

        [ForeignKey(nameof(DiskType))]
        public string TypeId { get; set; }

        public MyDisk()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
