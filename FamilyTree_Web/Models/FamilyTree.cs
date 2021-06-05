using Family_Tree.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FamilyTree_Web.Models
{
    public partial class FamilyTree : DbContext
    {
        public FamilyTree()
            : base("name=FamilyTree")
        {
        }

        public virtual DbSet<Family> Families { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<RelationShip> RelationShips { get; set; }
        public virtual DbSet<RelationType> RelationTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Family>()
                .HasMany(e => e.Members)
                .WithRequired(e => e.Family)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Family>()
                .HasMany(e => e.RelationShips)
                .WithRequired(e => e.Family)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.RelationShips)
                .WithRequired(e => e.VirsualMember1)
                .HasForeignKey(e => e.Member1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Member>()
                .HasMany(e => e.RelationShips1)
                .WithRequired(e => e.VirsualMember2)
                .HasForeignKey(e => e.Member2)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RelationType>()
                .HasMany(e => e.RelationShips)
                .WithRequired(e => e.RelationType)
                .HasForeignKey(e => e.Member1_RelationType)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RelationType>()
                .HasMany(e => e.RelationShips1)
                .WithRequired(e => e.RelationType1)
                .HasForeignKey(e => e.Member2_RelationType)
                .WillCascadeOnDelete(false);
        }
    }
}
