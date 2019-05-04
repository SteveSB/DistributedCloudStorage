using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileServerManager.Models
{
    public class Server
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Size { get; set; }
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}
