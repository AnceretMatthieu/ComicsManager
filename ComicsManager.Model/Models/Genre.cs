using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicsManager.Model.Models
{
    public class Genre : BaseEntity
    {
        [Required]
        public string Title { get; set; }
    }
}
