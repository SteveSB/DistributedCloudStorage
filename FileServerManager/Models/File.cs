using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FileServerManager.Models
{
    public class File
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ServerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Size { get; set; }
        [Required]
        public string Owner { get; set; }
        [Required]
        public string Path { get; set; }
        [DefaultValue(false)]
        public bool HasBackup { get; set; }

        public virtual Server Server { get; set; }
    }
}
