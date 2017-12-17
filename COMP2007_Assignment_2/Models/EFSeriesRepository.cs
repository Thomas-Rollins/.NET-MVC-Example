using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//additional refs
using System.Web;
using System.Data.Entity;
using COMP_2007_Assignment1.Controllers;

namespace COMP2007_Assignment_2.Models
{
    public class EFSeriesRepository : ISeriesRepository
    {
        //db connection
        ShowList db = new ShowList();

        public IQueryable<Series> Series { get { return db.Series; } }
        public IQueryable<Genre> Genres { get { return db.Genres; } }


        public void Delete(Series series)
        {
            db.Series.Remove(series);
            db.SaveChanges();
        }

        public Series Save(Series series)
        {
            if (series.SeriesID == 0)
            {
                db.Series.Add(series);
            }
            else
            {
                db.Entry(series).State = EntityState.Modified;
            }
            db.SaveChanges();
            return series;
        }
    }
}
