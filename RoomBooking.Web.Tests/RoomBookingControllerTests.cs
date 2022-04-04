using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using RoomBooking.Core.Services.IServices;
using RoomBooking.Models.Model;
using RoomBooking.Models.Model.VM;
using RoomBooking.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Web
{
    [TestFixture]
    public class RoomBookingControllerTests
    {
        private Mock<IStudyRoomBookingService> _studyRoomBookingService;

        private RoomBookingController _roomBookingController;

        [SetUp]
        public void Setup()
        {
            _studyRoomBookingService = new Mock<IStudyRoomBookingService>();
            _roomBookingController = new RoomBookingController(_studyRoomBookingService.Object);
        }

        [Test]
        public void IndexPage_CallRequest_VerifyGetAllInvoked()
        {
            _roomBookingController.Index();
            _studyRoomBookingService.Verify(x => x.GetAllBooking(), Times.Once);
        }

        [Test]
        public void BookRoomCheck_ModelStateValid_ReturnView()
        {
            _roomBookingController.ModelState.AddModelError("test", "test");
            var result = _roomBookingController.Book(new StudyRoomBooking());
            ViewResult viewResult = result as ViewResult;
            Assert.AreEqual("Book", viewResult.ViewName);
        }

        [Test]
        public void BookRoomCheck_NotSuccessful_NoRoomCode()
        {
            _studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>())).Returns(new StudyRoomBookingResult()
            {
                Code=StudyRoomBookingCode.NoRoomAvailable
            });

            var result = _roomBookingController.Book(new StudyRoomBooking());
            Assert.IsInstanceOf<ViewResult>(result);
            ViewResult viewResult = result as ViewResult;
            Assert.AreEqual("No Study Room available for selected date", viewResult.ViewData["Error"]);
        }
        [Test]
        public void BookRoomCheck_Successful_SuccessCodeAndRedirect()
        {
            //Arrange
            _studyRoomBookingService.Setup(x => x.BookStudyRoom(It.IsAny<StudyRoomBooking>()))
                .Returns((StudyRoomBooking booking)=> new StudyRoomBookingResult()
            {
                Code = StudyRoomBookingCode.Success,
                FirstName = booking.FirstName,
                LastName = booking.LastName,
                Email = booking.Email,
                Date = booking.Date
            });

            var result = _roomBookingController.Book(new StudyRoomBooking()
            {
                FirstName="Hello",
                LastName="Team",
                Email="test@test.com",
                Date= DateTime.Now,
                StudyRoomId=1
            });

            //Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            RedirectToActionResult actionResult = result as RedirectToActionResult;
            Assert.AreEqual("Hello", actionResult.RouteValues["FirstName"]);
            Assert.AreEqual(StudyRoomBookingCode.Success, actionResult.RouteValues["Code"]);
        }
    }
}
