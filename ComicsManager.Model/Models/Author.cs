using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComicsManager.Model.Models
{
    public class Author : BaseEntity
    {
        [Required]
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Display(Name = "Date de naissance")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public Nullable<Guid> PhotoId { get; set; }

        public File Photo { get; set; }

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
