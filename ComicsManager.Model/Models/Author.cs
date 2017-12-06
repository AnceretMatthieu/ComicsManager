using System;
using System.ComponentModel.DataAnnotations;

namespace ComicsManager.Model.Models
{
    public class Author : BaseEntity
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string Photo { get; set; }
    }
}
