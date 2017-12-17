using COMP_2007_Assignment1.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace COMP2007_Assignment_2.Models
{
    public interface ISeriesRepository
    {
        IQueryable<Series> Series { get; }
        IQueryable<Genre> Genres { get; }
        Series Save(Series series);
        void Delete(Series series);
    }
}