namespace COMP_2007_Assignment1.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Series
    {
        public int SeriesID { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name ="Series Title")]
        public string SeriesName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Synopsis { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Starting Date")]
        public DateTime? RunStartDate { get; set; }

        [StringLength(1024)]
        public string Producer { get; set; }

        [Range(0, 10)] // Restricts value to 0-10 for form validation
        public decimal? Raiting { get; set; }

        [DataType(DataType.ImageUrl)]
        public string CoverArtURL { get; set; }

        public int Genre { get; set; }

        public virtual Genre Genre1 { get; set; }
    }
}
