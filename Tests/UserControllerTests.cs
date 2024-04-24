using NUnit.Framework;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using static NUnit.Framework.Is;

namespace CRUD_application_2.Tests.Controllers
{
    [TestFixture]
    public class UserControllerTest
    {
        private UserController _controller;
        private List<User> _users;

        [SetUp]
        public void Setup()
        {
            _users = new List<User>
            {
                new User { Id = 1, Name = "Test User 1", Email = "test1@example.com" },
                new User { Id = 2, Name = "Test User 2", Email = "test2@example.com" },
                new User { Id = 3, Name = "Test User 3", Email = "test3@example.com" },
            };

            UserController.userlist = _users;
            _controller = new UserController();
        }

        [Test]
        public void Index_ReturnsCorrectViewWithModel()
        {
            var result = _controller.Index() as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as List<User>;
            Assert.That(model.Count, Is.EqualTo(_users.Count));
        }

        [Test]
        public void Details_ReturnsCorrectViewWithModel()
        {
            var result = _controller.Details(1) as ViewResult;

            Assert.That(result, Is.Not.Null);
            var model = result.Model as User;
            Assert.That(model.Id, Is.EqualTo(_users[0].Id));
        }

        [Test]
        public void Create_Post_RedirectsOnSuccess()
        {
            var result = _controller.Create(new User { Id = 4, Name = "Test User 4", Email = "test4@example.com" }) as RedirectToRouteResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Edit_Post_RedirectsOnSuccess()
        {
            var result = _controller.Edit(1, new User { Id = 1, Name = "Updated User", Email = "updated@example.com" }) as RedirectToRouteResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Delete_Post_RedirectsOnSuccess()
        {
            var result = _controller.Delete(1, new FormCollection()) as RedirectToRouteResult;

            Assert.That(result, Is.Not.Null);
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
        }
    }
}