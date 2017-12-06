using System;

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
        public Guid Id { get; set; }

        /// <summary>
        /// Date de création de l'entité (pour le tracking)
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Date de modification de l'entité (pour le tracking)
        /// </summary>
        public DateTime ModifiedOn { get; set; }
    }
}
