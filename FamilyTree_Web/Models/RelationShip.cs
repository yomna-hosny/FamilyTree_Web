namespace Family_Tree.Models
{
    using FamilyTree_Web.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RelationShip")]
    public partial class RelationShip
    {
        public int Id { get; set; }

        public int Member1 { get; set; }

        public int Member2 { get; set; }

        public int Member1_RelationType { get; set; }

        public int Member2_RelationType { get; set; }

        public int FamilyId { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RelationStart { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RelationEnd { get; set; }

        public virtual Family Family { get; set; }

        public virtual Member VirsualMember1 { get; set; }

        public virtual Member VirsualMember2 { get; set; }

        public virtual RelationType RelationType { get; set; }

        public virtual RelationType RelationType1 { get; set; }
    }
}
