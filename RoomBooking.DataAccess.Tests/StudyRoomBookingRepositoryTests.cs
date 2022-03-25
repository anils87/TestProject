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
   public class StudyRoomBookingRepositoryTests
    {
        private StudyRoomBooking studyRoomBooking_one;
        private StudyRoomBooking studyRoomBooking_two;
        private DbContextOptions<ApplicationDbContext> options;
        public StudyRoomBookingRepositoryTests()
        {
            studyRoomBooking_one = new StudyRoomBooking()
            {
                BookingId = 1,
                StudyRoomId = 1,
                FirstName = "Anil",
                LastName = "Yadav",
                Email = "booking1@text.com",
                Date = DateTime.Now.AddMinutes(10)
            };
            studyRoomBooking_two = new StudyRoomBooking()
            {
                BookingId = 2,
                StudyRoomId = 2,
                FirstName = "Anil1",
                LastName = "Yadav",
                Email = "booking2@text.com",
                Date = DateTime.Now.AddMinutes(10)
            };
        }
        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: "temp_RoomBookingDB").Options;
        }

        [Test]
        [Order(1)]
        public void SaveBooking_Booking_One_CheckTheValuesFromDatabase()
        {
            //Arrange
           

            //Act
            using(var context= new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new StudyRoomBookingRepository(context);
                repository.Book(studyRoomBooking_one);
            }

            //Assert
            using (var context = new ApplicationDbContext(options))
            {
                var bookingFromDB = context.StudyRoomBookings.FirstOrDefault(u => u.BookingId == 1);
                Assert.AreEqual(studyRoomBooking_one.BookingId, bookingFromDB.BookingId);
                Assert.AreEqual(studyRoomBooking_one.StudyRoomId, bookingFromDB.StudyRoomId);
                Assert.AreEqual(studyRoomBooking_one.FirstName, bookingFromDB.FirstName);
                Assert.AreEqual(studyRoomBooking_one.LastName, bookingFromDB.LastName);
                Assert.AreEqual(studyRoomBooking_one.Email, bookingFromDB.Email);
                Assert.AreEqual(studyRoomBooking_one.Date, bookingFromDB.Date);

            }
        }

        [Test]
        [Order(2)]
        public void SaveBooking_Booking_Two_CheckTheValuesFromDatabase()
        {
            //Arrange
           

            //Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new StudyRoomBookingRepository(context);
                repository.Book(studyRoomBooking_two);
            }

            //Assert
            using (var context = new ApplicationDbContext(options))
            {
                var bookingFromDB = context.StudyRoomBookings.FirstOrDefault(u => u.BookingId == 2);
                Assert.AreEqual(studyRoomBooking_two.BookingId, bookingFromDB.BookingId);
                Assert.AreEqual(studyRoomBooking_two.StudyRoomId, bookingFromDB.StudyRoomId);
                Assert.AreEqual(studyRoomBooking_two.FirstName, bookingFromDB.FirstName);
                Assert.AreEqual(studyRoomBooking_two.LastName, bookingFromDB.LastName);
                Assert.AreEqual(studyRoomBooking_two.Email, bookingFromDB.Email);
                Assert.AreEqual(studyRoomBooking_two.Date, bookingFromDB.Date);

            }
        }

        [Test]
        [Order(3)]
        public void GetAll_Booking_OneAndTwo_CheckAllValuesFromDatabase()
        {
            //Arrange
            var expectedResult = new List<StudyRoomBooking>() { studyRoomBooking_one, studyRoomBooking_two };
            

            //Act
            using (var context = new ApplicationDbContext(options))
            {
                context.Database.EnsureDeleted();
                var repository = new StudyRoomBookingRepository(context);
                repository.Book(studyRoomBooking_one);
                repository.Book(studyRoomBooking_two);
            }

            //Assert
            List<StudyRoomBooking> actualList;
            using (var context = new ApplicationDbContext(options))
            {
                var repository = new StudyRoomBookingRepository(context);
                actualList = repository.GetAll(null).ToList();
            }
            CollectionAssert.AreEqual(expectedResult, actualList,new BookingCompare());
        }
        private class BookingCompare : IComparer
        {
            public int Compare(object x, object y)
            {
                var booking1 = (StudyRoomBooking)x;
                var booking2 = (StudyRoomBooking)y;
                if (booking1.BookingId != booking2.BookingId)
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
