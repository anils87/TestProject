using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using RoomBooking.DataAccess.Repository;
using RoomBooking.Models.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomBooking.DataAccess
{
    [TestFixture]
    public class StudyRoomRepositoryTests
    {
        private StudyRoom _studyRoom_one;
        private StudyRoom _studyRoom_two;
        private DbContextOptions<ApplicationDbContext> options;
        public StudyRoomRepositoryTests()
        {
            _studyRoom_one = new()
            {
                Id = 1, RoomNumber = "R1", RoomName = "Room 1"
            };
            _studyRoom_two = new()
            {
                Id = 2,
                RoomNumber = "R2",
                RoomName = "Room 2"
            };
        }

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "temp_RoomBooking").Options;
        }

        [Test]
        public void GetAll_StudyRoom_GetAllStudyRoomFromDB()
        {
            //Arrange
            var expectedResult = new List<StudyRoom>() { _studyRoom_one, _studyRoom_two };

            //Act
            using(var context = new ApplicationDbContext(options))
            {
                context.StudyRooms.Add(_studyRoom_one);
                context.StudyRooms.Add(_studyRoom_two);
                context.SaveChanges();
            }
            //Assert
            List<StudyRoom> actualList;
            using (var context = new ApplicationDbContext(options))
            {
                actualList = context.StudyRooms.ToList();
                Assert.AreEqual(2, actualList.Count());
                CollectionAssert.AreEqual(expectedResult, actualList, new StudyRoomCompare());
            }
        }
        private class StudyRoomCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var studyRoom1 = (StudyRoom)x;
                var studyRoom2 = (StudyRoom)y;
                if(studyRoom1.Id != studyRoom2.Id)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
