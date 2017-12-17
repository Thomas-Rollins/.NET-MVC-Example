using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Web.Mvc;
using COMP_2007_Assignment1.Models;
using COMP2007_Assignment_2.Models;
using Moq;
using COMP_2007_Assignment1.Controllers;
using System.Linq;

namespace COMP2007_Assignment_2.Tests.Controllers
{
    /// <summary>
    /// Summary description for SeriesControllerTest
    /// </summary>
    [TestClass]
    public class SeriesControllerTest
    {
        SeriesController controller;
        Mock<ISeriesRepository> seriesMock;
        Mock<IGenreRepository> genreMock;

        List<Series> series;

        [TestInitialize]
        public void TestInitialize()
        {
            seriesMock = new Mock<ISeriesRepository>();
            genreMock = new Mock<IGenreRepository>();

            series = new List<Series>
            {
                new Series { SeriesID = 1, SeriesName = "series 1", Producer = "series 1 producer", Raiting = 5, RunStartDate = DateTime.Now, CoverArtURL = "testPicture.png",
                    Genre1 = new Genre {GenreID = 1, GenreDescription = "Genre 1 Desc", GenreName = "genre 1" }, Synopsis = "series 1 Synopsis" },
                new Series { SeriesID = 2, SeriesName = "series 2", Producer = "series 2 producer", Raiting = 6, RunStartDate = DateTime.Now, CoverArtURL = "testPicture.png",
                    Genre1 = new Genre {GenreID = 2, GenreDescription = "Genre 2 Desc", GenreName = "genre 2" }, Synopsis = "series 2 Synopsis" },
                new Series { SeriesID = 3, SeriesName = "series 3", Producer = "series 3 producer", Raiting = 7, RunStartDate = DateTime.Now,CoverArtURL = "testPicture.png",
                      Genre1 = new Genre {GenreID = 3, GenreDescription = "Genre 3 Desc", GenreName = "genre 3" }, Synopsis = "series 3 Synopsis" },

            };

            seriesMock.Setup(m => m.Series).Returns(series.AsQueryable());

            controller = new SeriesController(seriesMock.Object);
        }

        [TestMethod]
        public void Index()
        {
            // Act
            var result = (List<Series>)controller.Index().Model;

            // Assert
            CollectionAssert.AreEqual(series, result);
        }

        // GET: Details
        [TestMethod]
        public void DetailsValidId()
        {
            // act
            var actual = (Series)controller.Details(1).Model;

            // assert
            Assert.AreEqual(series.ToList()[0], actual);
        }

        [TestMethod]
        public void DetailsInvalidId()
        {
            // act
            ViewResult actual = controller.Details(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DetailsInvalidNoId()
        {
            // act
            ViewResult actual = controller.Details(null);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Create
        [TestMethod]
        public void CreateViewLoads()
        {
            // act
            ViewResult actual = controller.Create();

            // assert

            Assert.AreEqual("Create", actual.ViewName);
        }

        // POST: Create
        [TestMethod]
        public void CreateValid()
        {
            // arrange
            Series series = new Series
            {
                SeriesID = 4,
                SeriesName = "Series 4",
                Producer = "Series 4 Producer",
                Synopsis = "series 4 Synposis",
                RunStartDate = DateTime.Now,
                Raiting = 9,
                Genre1 = new Genre { GenreID = 5, GenreName = "Genre 4", GenreDescription = "Genre 4 Desc" }
            };

            // act
            ViewResult actual = controller.Create(series);

            // assert
            Assert.AreEqual("Index", actual.ViewName);
        }

        [TestMethod]
        public void CreateInvalidSeries()
        {
            // arrange
            Series series = null;

            // act
            ViewResult actual = controller.Create(series);

            // assert
            Assert.AreEqual("Create", actual.ViewName);
        }

        // GET: Edit
        [TestMethod]
        public void EditValidId()
        {
            // act
            var actual = (Series)controller.Edit(1).Model;

            // assert
            Assert.AreEqual(series.ToList()[0], actual);
        }

        [TestMethod]
        public void EditInvalidId()
        {
            // act
            ViewResult actual = controller.Edit(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void EditInvalidNoId()
        {
            // arrange
            int? SeriesID = null;

            // act
            ViewResult actual = controller.Edit(SeriesID);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // POST: Edit
        [TestMethod]
        public void EditValid()
        {
            // arrange
            Series series = new Series
            {
                SeriesID = 1,
                SeriesName = "Series 1 - Edited",
                Producer = "Series 1 Producer",
                Synopsis = "series 1Synposis",
                RunStartDate = DateTime.Now,
                Raiting = 9
            };

            // act
            RedirectToRouteResult actual = (RedirectToRouteResult)controller.Edit(series);

            // assert
            Assert.AreEqual("Index", actual.RouteValues["action"]);
        }

        [TestMethod]
        public void EditInvalidSeries()
        {
            // arrange
            Series series = null;

            // act
            ViewResult actual = (ViewResult)controller.Edit(series);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // GET: Delete
        [TestMethod]
        public void DeleteValidId()
        {
            // act
            var actual = (Series)controller.Delete(1).Model;

            // assert
            Assert.AreEqual(series.ToList()[0], actual);
        }

        [TestMethod]
        public void DeleteInvalidId()
        {
            // act
            ViewResult actual = controller.Delete(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteInvalidNoId()
        {
            // arrange
            int? id = null;

            // act
            ViewResult actual = controller.Delete(id);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // POST: DeleteConfirmed
        [TestMethod]
        public void DeleteConfirmedValidId()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(1);

            // assert
            Assert.AreEqual("Index", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidId()
        {
            // act
            ViewResult actual = controller.DeleteConfirmed(4);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        [TestMethod]
        public void DeleteConfirmedInvalidNoId()
        {
            // arrange
            int? id = null;

            // act
            ViewResult actual = controller.DeleteConfirmed(id);

            // assert
            Assert.AreEqual("Error", actual.ViewName);
        }

        // Just used for forcing all get/set properties to be checked
        #region Get/Set Properties
        [TestMethod]
        public void GetSetSeriesName()
        {
            // arrange
            Series testSeries = new Series { SeriesName = series.First().SeriesName };
            //  assert
            Assert.AreEqual(testSeries.SeriesName, series.First().SeriesName);
        }

        [TestMethod]
        public void GetSetSeriesID()
        {
            // arrange
            Series testSeries = new Series { SeriesID = series.First().SeriesID };
            // assert
            Assert.AreEqual(testSeries.SeriesID, series.First().SeriesID);
        }
        [TestMethod]
        public void GetSetGenre()
        {
            // arrange
            Series testSeries = new Series { Genre = series.First().Genre };
            // assert
            Assert.AreEqual(testSeries.Genre, series.First().Genre);
        }
        [TestMethod]
        public void GetSetGenre1()
        {
            // arrange
            Series testSeries = new Series { Genre1 = series.First().Genre1 };
            // assert
            Assert.AreEqual(testSeries.Genre1, series.First().Genre1);
        }
        [TestMethod]
        public void GetSetProducer()
        {
            // arrange
            Series testSeries = new Series { Producer = series.First().Producer };
            // assert
            Assert.AreEqual(testSeries.Producer, series.First().Producer);
        }
        [TestMethod]
        public void GetSetRaiting()
        {
            // arrange
            Series testSeries = new Series { Raiting = series.First().Raiting };
            // assert
            Assert.AreEqual(testSeries.Raiting, series.First().Raiting);
        }
        [TestMethod]
        public void GetSetRunDate()
        {
            // arrange
            Series testSeries = new Series { RunStartDate = series.First().RunStartDate };
            // assert
            Assert.AreEqual(testSeries.RunStartDate, series.First().RunStartDate);
        }
        [TestMethod]
        public void GetSetSynopsis()
        {
            // arrange
            Series testSeries = new Series { Synopsis = series.First().Synopsis };
            // assert
            Assert.AreEqual(testSeries.Synopsis, series.First().Synopsis);
        }
        [TestMethod]
        public void GetSetCoverArtURL()
        {
            // arrange
            Series testSeries = new Series { CoverArtURL = series.First().CoverArtURL };
            // assert
            Assert.AreEqual(testSeries.CoverArtURL, series.First().CoverArtURL);
        }
        #endregion
    }
}

