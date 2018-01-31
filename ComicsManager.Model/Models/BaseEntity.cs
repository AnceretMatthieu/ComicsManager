using System;
using System.ComponentModel.DataAnnotations;

namespace ComicsManager.Model.Models
{
    /// <summary>
    /// Propriétés communes à l'ensemble des entités
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Identifiant de l'entité
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Date de création de l'entité (pour le tracking)
        /// </summary>
        [Required]
        [Display(Name = "Créé le")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date de modification de l'entité (pour le tracking)
        /// </summary>
        [Required]
        [Display(Name = "Modifié le")]
        public DateTime ModifiedOn { get; set; }
    }
}
