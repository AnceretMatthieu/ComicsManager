using System;
using System.ComponentModel.DataAnnotations;

namespace ComicsManager.Model.Models
{
    public class Comic : BaseEntity
    {
        [Required]
        public string Title { get; set; }

        public string ISBN { get; set; }

        public Nullable<int> Cycle { get; set; }

        public string Collection { get; set; }

        public Nullable<int> Note { get; set; }

        public Nullable<Guid> CouvertureId { get; set; }

        public File Couverture { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }


        public Nullable<Guid> ScenaristeId { get; set; }

        public Author Scenariste { get; set; }

        public Nullable<Guid> DessinateurId { get; set; }

        public Author Dessinateur { get; set; }
        

        public Nullable<Guid> EditorId { get; set; }

        public Editor Editeur { get; set; }     
        

        public Guid GenreId { get; set; }

        public Genre Genre { get; set; }

       
        public Nullable<Guid> FileId { get; set; }

        public File File { get; set; }
    }
}
