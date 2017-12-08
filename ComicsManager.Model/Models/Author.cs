using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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


        [InverseProperty("Scenariste")]
        public ICollection<Comic> Scenaristes { get; set; }

        [InverseProperty("Dessinateur")]
        public ICollection<Comic> Dessinateurs { get; set; }
        

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
    }
}
