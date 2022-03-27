using System.ComponentModel.DataAnnotations;

namespace SequenceGenerator.Models
{
    public class FileInfo
    {
        [Required(ErrorMessage = "File Path is required")]
        public string? FilePath { get; set; }
    }
}
