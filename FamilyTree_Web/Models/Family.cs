namespace FamilyTree_Web.Models
{
    using Family_Tree.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Family")]
    public partial class Family
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Family()
        {
            Members = new HashSet<Member>();
            RelationShips = new HashSet<RelationShip>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [StringLength(70)]
        public string Comment { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Member> Members { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RelationShip> RelationShips { get; set; }
    }
}
