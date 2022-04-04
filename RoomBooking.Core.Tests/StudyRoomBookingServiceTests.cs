using Moq;
using NUnit.Framework;
using RoomBooking.Core.Services;
using RoomBooking.DataAccess.Repository;
using RoomBooking.DataAccess.Repository.IRepository;
using RoomBooking.Models.Model;
using RoomBooking.Models.Model.VM;
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
        private StudyRoomBooking _request;
        private List<StudyRoom> _availableStudyRoom;
        private Mock<IStudyRoomBookingRepository> _studyRoomBookingRepoMock;
        private Mock<IStudyRoomRepository> _studyRoomRepoMock;
        private StudyRoomBookingService _studyRoomBookingService;
        [SetUp]
        public void Setup()
        {
            _request = new StudyRoomBooking()
            {
                FirstName = "Anil",
                LastName = "Yadav",
                Email = "Test123@Test.com",
                Date = new DateTime(2022, 03, 28)
            };
            _availableStudyRoom = new List<StudyRoom>()
            {
                new StudyRoom(){Id=10,RoomName="Michgen",RoomNumber="A202"}
            };
            _studyRoomBookingRepoMock = new Mock<IStudyRoomBookingRepository>();
            _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
            _studyRoomRepoMock.Setup(x => x.GetAll()).Returns(_availableStudyRoom);
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
            //Assert.AreEqual("Value cannot be null. (Parameter 'request')", exception.Message);
            Assert.AreEqual("request", exception.ParamName);
        }
        [Test]
        public void StudyRoomBooking_saveBookingWithAvailableRoom_ReturnResultWithAllValues()
        {
            //Arrange
            StudyRoomBooking savedStudyRoomBooking=null;
            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
               .Callback<StudyRoomBooking>(booking =>
               {
                   savedStudyRoomBooking = booking;
               });

            //Act
            _studyRoomBookingService.BookStudyRoom(_request);

            //Assert
            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Once);
            Assert.NotNull(savedStudyRoomBooking);
            Assert.AreEqual(_request.FirstName,savedStudyRoomBooking.FirstName);
            Assert.AreEqual(_request.LastName, savedStudyRoomBooking.LastName);
            Assert.AreEqual(_request.Email, savedStudyRoomBooking.Email);
            Assert.AreEqual(_request.Date, savedStudyRoomBooking.Date);
            Assert.AreEqual(_availableStudyRoom.First().Id, savedStudyRoomBooking.StudyRoomId);
        }

        [Test]
        public void StudyRoomBookingResultCheck_InputRequest_ValuesMatchInRequest()
        {         

            //Act
           var result= _studyRoomBookingService.BookStudyRoom(_request);

            //Assert
            Assert.NotNull(result);
            Assert.AreEqual(_request.FirstName, result.FirstName);
            Assert.AreEqual(_request.LastName, result.LastName);
            Assert.AreEqual(_request.Email, result.Email);
            Assert.AreEqual(_request.Date, result.Date);
            
        }
        [Test]
        [TestCase(true,ExpectedResult = StudyRoomBookingCode.Success)]
        [TestCase(false, ExpectedResult = StudyRoomBookingCode.NoRoomAvailable)]
        public StudyRoomBookingCode ResultCodeSuccess_RoomAvailability_ReturnSuccessResultCode(bool roomAvailable)
        {
            if (!roomAvailable)
            {
                _availableStudyRoom.Clear();
            }

            //Assert
            return _studyRoomBookingService.BookStudyRoom(_request).Code;

        }

        [Test]
        [TestCase(0, false)]
        [TestCase(55, true)]
        public void StudyRoomBooking_BookRoomWithAvailability_ReturnBookingId(int expectedBookingId,bool roomAvailable)
        {
            if (!roomAvailable)
            {
                _availableStudyRoom.Clear();
            }

            _studyRoomBookingRepoMock.Setup(x => x.Book(It.IsAny<StudyRoomBooking>()))
                .Callback<StudyRoomBooking>(booking =>
                {
                    booking.BookingId = 55;
                });

            //Act
            var result= _studyRoomBookingService.BookStudyRoom(_request);

            //Assert
            Assert.AreEqual(expectedBookingId, result.BookingId);

        }

        [Test]
        public void BookNotInvoked_SaveBookingWithoutAvailableRoom_BookMethodNotAllowed()
        {
            _availableStudyRoom.Clear();

            var result = _studyRoomBookingService.BookStudyRoom(_request);

            _studyRoomBookingRepoMock.Verify(x => x.Book(It.IsAny<StudyRoomBooking>()), Times.Never);
        }
    }

    
}
