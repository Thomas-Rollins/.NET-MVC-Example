using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COMP_2007_Assignment1.Controllers;
using COMP2007_Assignment_2.Models;

namespace COMP_2007_Assignment1.Models
{
    [Authorize]
    public class SeriesController : Controller
    {
        private ISeriesRepository db;

        public SeriesController()
        {
            this.db = new EFSeriesRepository();
        }

        public SeriesController(ISeriesRepository smRepo)
        {
            this.db = smRepo;
        }

        private List<Series> FetchIndexData()
        {
            var Series = db.Series.Include(s => s.Genre1);

            //ViewBag.SeriesCount = Series.Count();

            return (Series.OrderBy(s => s.SeriesName).ToList());
        }

        // GET: Series
        [AllowAnonymous]
        public ViewResult Index()
        {
            var series = FetchIndexData();
            return View(series);
        }

        // GET: Series/Details/5
        [AllowAnonymous]
        public ViewResult Details(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Series series = db.Series.FirstOrDefault(s => s.SeriesID == id);
            if (series == null)
            {
                return View("Error");
            }
            
            return View(series);
        }

        // GET: Series/Browse/5
        [AllowAnonymous]
        public ViewResult Browse(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            //Series series = db.Series.Find(id);
            var series = from s in db.Series
                         where s.Genre == id
                         orderby s.SeriesName ascending
                         select s;
            ViewBag.Genre = id;
            return View(series);
        }

        // GET: Series/Create
        public ViewResult Create()
        {
            ViewBag.Genre = new SelectList(db.Genres, "GenreID", "GenreName");
            return View("Create");
        }

        // POST: Series/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult Create([Bind(Include = "SeriesID,SeriesName,Synopsis,RunStartDate,Producer,Raiting,CoverArtURL,Genre")] Series series)
        {
            if (series == null)
            {
                ViewBag.ArtistId = new SelectList(db.Series, "SeriesID", "SeriesName");
                ViewBag.GenreId = new SelectList(db.Genres, "GenreID", "GenreName");
                return View("Create");
            }

            if (ModelState.IsValid)
            {
                if (Request != null)
                {
                    // check for a new cover image upload
                    if (Request.Files.Count > 0)
                    {
                        var file = Request.Files[0];

                        if (file.FileName != null && file.ContentLength > 0)
                        {
                            string path = Server.MapPath("~/Content/Images/") + file.FileName;
                            file.SaveAs(path);

                            // add path to image name before saving
                            series.CoverArtURL = "/Content/Images/" + file.FileName;
                        }
                    }
                }
                
                db.Save(series);
                var seri = FetchIndexData();
                return View("Index", seri);

            }
            ViewBag.SeriesID = new SelectList(db.Series, "SeriesId", "SeriesName", series.SeriesID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", series.Genre);
            return View(series);
        }

        // GET: Series/Edit/5
        public ViewResult Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Series series = db.Series.FirstOrDefault(s => s.SeriesID == id);
            if (series == null)
            {
                return View("Error");
            }
            ViewBag.Genre = new SelectList(db.Genres, "GenreID", "GenreName", series.Genre);
            return View(series);
        }

        // POST: Series/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SeriesID,SeriesName,Synopsis,RunStartDate,Producer,Raiting,CoverArtURL,Genre")] Series series)
        {
            if (ModelState.IsValid)
            {
                if (series == null)
                {
                    return View("Error");
                }
                db.Save(series);
                return RedirectToAction("Index");
            }
            ViewBag.SeriesID = new SelectList(db.Series, "SeriesID", "SeriesName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", series.Genre);
            return View("Edit");
        }

        // GET: Series/Delete/5
        public ViewResult Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Series series = db.Series.FirstOrDefault(s => s.SeriesID == id);
            if (series == null)
            {
                return View("Error");
            }
            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ViewResult DeleteConfirmed(int? id)
        {
            if(id == null)
            {
                return View("Error");
            }

            Series series = db.Series.FirstOrDefault(s => s.SeriesID == id);
            if(series == null)
            {
                return View("Error");
            }
            db.Delete(series);

            var seri = FetchIndexData();
            return View("Index", seri);
        }
    }
}
