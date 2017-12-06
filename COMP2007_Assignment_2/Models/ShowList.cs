namespace COMP_2007_Assignment1.Controllers
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShowList : DbContext
    {
        public ShowList()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Series> Series { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>()
                .HasMany(e => e.Series)
                .WithRequired(e => e.Genre1)
                .HasForeignKey(e => e.Genre)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Series>()
                .Property(e => e.Raiting)
                .HasPrecision(3, 1);
        }
    }
}
