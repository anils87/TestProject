using Moq;
using NUnit.Framework;
using RoomBooking.Core.Services.IServices;
using RoomBooking.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Web
{
    [TestFixture]
    public class RoomControllerTests
    {
        private Mock<IStudyRoomService> _studyRoomService;
        private RoomsController _roomController;

        [SetUp]
        public void Setup()
        {
            _studyRoomService = new Mock<IStudyRoomService>();
            _roomController = new RoomsController(_studyRoomService.Object);
        }

        [Test]
        public void IndexPage_CallRequest_GetAllInvoked()
        {
            _roomController.Index();
            _studyRoomService.Verify(x => x.GetAll(), Times.Once);
        }       
     }
}
