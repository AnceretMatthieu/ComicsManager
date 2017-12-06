using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicsManager.Model.Models
{
    public class Editor : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
