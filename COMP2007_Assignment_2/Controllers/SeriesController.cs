using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using COMP_2007_Assignment1.Controllers;

namespace COMP_2007_Assignment1.Models
{
    public class SeriesController : Controller
    {
        private ShowList db = new ShowList();

        // GET: Series
        public ActionResult Index()
        {
            var series = db.Series.Include(s => s.Genre1);
            return View(series.ToList());
        }

        // GET: Series/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // GET: Series/Browse/5
        public ActionResult Browse(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
        public ActionResult Create()
        {
            ViewBag.Genre = new SelectList(db.Genres, "GenreID", "GenreName");
            return View();
        }

        // POST: Series/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SeriesID,SeriesName,Synopsis,RunStartDate,Producer,Raiting,CoverArtURL,Genre")] Series series)
        {
            if (ModelState.IsValid)
            {
                db.Series.Add(series);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Genre = new SelectList(db.Genres, "GenreID", "GenreName", series.Genre);
            return View(series);
        }

        // GET: Series/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
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
                db.Entry(series).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Genre = new SelectList(db.Genres, "GenreID", "GenreName", series.Genre);
            return View(series);
        }

        // GET: Series/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.Series.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // POST: Series/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Series series = db.Series.Find(id);
            db.Series.Remove(series);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
