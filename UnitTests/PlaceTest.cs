using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PlaceUp.Models;
using PlaceUp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using PlaceUp.Context.PlaceRepository;
using System.Threading.Tasks;

namespace PlaceTests
{
    [TestClass]
    public class PlaceTest
    {
        [TestMethod]
        public async Task GetAllPlaces()
        {
            Mock<IPlaceRepository> mock = new Mock<IPlaceRepository>();
            mock.Setup(m => m.GetAll()).ReturnsAsync(new Place[]
            {
                new Place {PlaceId = Guid.NewGuid(), Name = "P1"},
                new Place {PlaceId = Guid.NewGuid(), Name = "P2"},
                new Place {PlaceId = Guid.NewGuid(), Name = "P3"},
            }.AsQueryable());

            PlacesController target = new PlacesController(mock.Object);

            var result = await target.Index();

            Assert.IsNotNull(result);

            var model = (IEnumerable<Place>)((ViewResult)result).Model;

            Assert.AreEqual(model.Count(), 3);
            Assert.AreEqual("P1", model.ToArray()[0].Name);
            Assert.AreEqual("P2", model.ToArray()[1].Name);
            Assert.AreEqual("P3", model.ToArray()[2].Name);
        }

        [TestMethod]
        public async Task GetAllPlacesByCategoryId()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();

            Category cat1 = new Category { CategoryId = guid1, Name = "Cat1" };
            Category cat2 = new Category { CategoryId = guid2, Name = "Cat2" };

            Place place1 = new Place { PlaceId = Guid.NewGuid(), Name = "P1", Category = cat1 };
            Place place2 = new Place { PlaceId = Guid.NewGuid(), Name = "P2", Category = cat1 };
            Place place3 = new Place { PlaceId = Guid.NewGuid(), Name = "P3", Category = cat2 };

            Mock<IPlaceRepository> mock = new Mock<IPlaceRepository>();
            mock.Setup(m => m.GetAllByCategoryId(guid1)).ReturnsAsync(new List<Place>
            {
                place1, place2, place3
            }.Where(x => x.Category.CategoryId == guid1).AsQueryable());

            PlacesController target = new PlacesController(mock.Object);

            var result = await target.GetAllByCategoryId(guid1);

            Assert.IsNotNull(result);

            var model = (IEnumerable<Place>)((ViewResult)result).Model;

            Assert.AreEqual(model.Count(), 2);
            Assert.AreEqual("P1", model.ToArray()[0].Name);
            Assert.AreEqual("P2", model.ToArray()[1].Name);
        }

        [TestMethod]
        public async Task GetAllPlacesByTagId()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();

            Tag tag1 = new Tag { TagId = guid1, Name = "tasty dishes" };
            Tag tag2 = new Tag { TagId = guid2, Name = "don`t like" };
            Tag tag3 = new Tag { TagId = guid3, Name = "I will come" };

            Place place1 = new Place { PlaceId = Guid.NewGuid(), Name = "P1" };
            Place place2 = new Place { PlaceId = Guid.NewGuid(), Name = "P2" };
            Place place3 = new Place { PlaceId = Guid.NewGuid(), Name = "P3" };

            place1.Tags.Add(tag1);
            place1.Tags.Add(tag3);

            place2.Tags.Add(tag2);

            place3.Tags.Add(tag3);

            Mock<IPlaceRepository> mock = new Mock<IPlaceRepository>();
            mock.Setup(m => m.GetAllByTagId(guid3)).ReturnsAsync(new List<Place>
            {
                place1, place2, place3
            }.Where(s => s.Tags.Any(g => g.TagId == guid3)).AsQueryable());

            PlacesController target = new PlacesController(mock.Object);

            var result = await target.GetAllByTagId(guid3);

            Assert.IsNotNull(result);

            var model = (IEnumerable<Place>)((ViewResult)result).Model;

            Assert.AreEqual(model.Count(), 2);
            Assert.AreEqual("P1", model.ToArray()[0].Name);
            Assert.AreEqual("P3", model.ToArray()[1].Name);
        }

        [TestMethod]
        public async Task EditPlace()
        {
            Guid guid1 = Guid.NewGuid();
            Guid guid2 = Guid.NewGuid();
            Guid guid3 = Guid.NewGuid();

            Place place1 = new Place { PlaceId = Guid.NewGuid(), Name = "P1" };
            Place place2 = new Place { PlaceId = Guid.NewGuid(), Name = "P2" };
            Place place3 = new Place { PlaceId = Guid.NewGuid(), Name = "P3" };

            Mock<IPlaceRepository> mock = new Mock<IPlaceRepository>();
            mock.Setup(m => m.Places).Returns(new List<Place>
            {
                place1, place2, place3
            }.AsQueryable());

            PlacesController target = new PlacesController(mock.Object);

            var result1 = await target.Edit(guid1);

            Assert.IsNotNull(result1);

            var model1 = (Place)((ViewResult)result1).Model;

            Assert.AreEqual(guid1, model1.PlaceId);
        }
    }
}