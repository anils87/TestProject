using Moq;
using NUnit.Framework;
using RoomBooking.Core.Services;
using RoomBooking.DataAccess.Repository;
using RoomBooking.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Core
{
    [TestFixture]
    public class StudyRoomBookingServiceTests
    {
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
        private Mock<IStudyRoomRepository> _studyRoomRepoMock;
        private StudyRoomBookingService _studyRoomBookingService;
        [SetUp]
        public void Setup()
        {
            _studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();
            _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
            _studyRoomBookingService = new StudyRoomBookingService(_studyRoomBookingRepoMock.Object,_studyRoomRepoMock.Object);
        }

        [Test]
        public void GetAllBooking_InvokeMethod_CheckIfRepoIsCalled()
        {
            _studyRoomBookingService.GetAllBooking();
            _studyRoomBookingRepoMock.Verify(x => x.GetAll(null), Times.Once);
        }

        [Test]
        public void BookingException_NullRequest_ThrowException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _studyRoomBookingService.BookStudyRoom(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);
            Assert.AreEqual("request", exception.ParamName);
        }
    }

    
}
