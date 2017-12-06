namespace COMP_2007_Assignment1.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Genre
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Genre()
        {
            Series = new HashSet<Series>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GenreID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Genre")]
        public string GenreName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Description")]
        public string GenreDescription { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Series> Series { get; set; }
    }
}
