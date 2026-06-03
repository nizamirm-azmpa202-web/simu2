using System.ComponentModel.DataAnnotations;

namespace simu2.Models
{
    public class Chef: BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        [StringLength(100)]
        public string Job { get; set; }
    }
}
