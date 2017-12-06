using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComicsManager.Model.Models
{
    public class Comic : BaseEntity
    {
        public Author Scenariste { get; set; }

        public Author Dessinateur { get; set; }

        public DateTime PublicationDate { get; set; }

        public Editor Editeur { get; set; }
        
        public string ISBN { get; set; }

        [Required]
        public string Title { get; set; }

        public int Cycle { get; set; }

        public string Collection { get; set; }

        public int Note { get; set; }

        public string Couverture { get; set; }
    }
}
