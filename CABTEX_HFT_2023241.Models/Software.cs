using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CABTEX_HFT_2023241.Models
{
    public class Software 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public Software()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
//Many disks can contain the same software and 1 disk contains a lot of software