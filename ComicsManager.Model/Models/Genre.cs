using System.ComponentModel.DataAnnotations;

namespace ComicsManager.Model.Models
{
    public class Genre : BaseEntity
    {
        [Required]
        [Display(Name = "Titre")]
        public string Title { get; set; }
    }
}
