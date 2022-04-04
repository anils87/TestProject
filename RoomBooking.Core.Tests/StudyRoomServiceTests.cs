using Moq;
using NUnit.Framework;
using RoomBooking.Core.Services;
using RoomBooking.DataAccess.Repository.IRepository;
using RoomBooking.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.Core
{
    [TestFixture]
    public class StudyRoomServiceTests
    {
        private List<StudyRoom> _availableStudyRoom;
        private StudyRoomService _studyRoomService;
        private Mock<IStudyRoomRepository> _studyRoomRepoMock;
       
        [SetUp]
        public void Setup()
        {
            _availableStudyRoom = new List<StudyRoom>()
            {
                new StudyRoom(){Id=10,RoomName="Michgen",RoomNumber="A202"}
            };
            _studyRoomRepoMock = new Mock<IStudyRoomRepository>();
            _studyRoomService = new StudyRoomService(_studyRoomRepoMock.Object);

        }
        [Test]
        public void GetAllStudyRoom_InvokeMethod_CheckIfRepoCalled()
        {
            _studyRoomService.GetAll();
            _studyRoomRepoMock.Verify(x => x.GetAll(), Times.Once);
        }
    }
}
