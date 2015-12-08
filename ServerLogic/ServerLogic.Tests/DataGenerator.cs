using ServerLogic.Map;
using System;
using Moq;
using ServerLogic.Repositories;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace ServerLogic.Tests {
    static class Extension {
        public static T GetObject<T>(this T a) {
            var formatter = new BinaryFormatter();
            using(var stream = new MemoryStream()) {
                formatter.Serialize(stream, a);
                stream.Seek(0, SeekOrigin.Begin);
                return (T) formatter.Deserialize(stream);
            }
        }
    }

    class DataGenerator {
        private static Users user = new Users {
            idUser = Guid.NewGuid(),
            contactNumber = "88005553535",
            email = "abc@gmail.com",
            firstName = "Andrey",
            lastName = "Fedorchuk"
        };

        private static Meeting meet = new Meeting {
            idMeet = 1,
            dateMeet = DateTime.Now,
            place = 2,
        };

        private static Place place = new Place {
            idPlace = 1,
            city = "SPB",
            house = 24,
            country = "Russia",
            street = "Gorodskaya",
        };

        private static Subscription sub = new Subscription {
            idPlace = 1,
            idUser = Guid.NewGuid(),
        };

        public static Users GenerateUser() {
            return user;
        }

        public static Meeting GenerateMeet() {
            return meet;
        }   

        public static Place GeneratePlace() {
            return place;
        }

        public static Subscription GenerateSubscription() {
            return sub;
        }
        
        public static IUsersRepository GetUsersRepositoryMock() {
            var mock = new Mock<IUsersRepository>();
            Users usr = DataGenerator.GenerateUser();
            mock.Setup(userRepository => userRepository.Create(usr));
            mock.Setup(userRepository => userRepository.ChangeNumber(usr, "8800"));
            mock.Setup(userRepository => userRepository.Delete(usr));
            mock.Setup(userRepository => userRepository.GetUser(It.IsAny<Users>())).Returns(usr);
            mock.Setup(userRepository => userRepository.Exist(It.IsAny<Guid>())).Returns(true);

            return mock.Object;
        }

        public static IMeetingRepository GetMeetingRepositoryMock() {
            Mock<IMeetingRepository> mock = new Mock<IMeetingRepository>();
            Meeting mt = DataGenerator.GenerateMeet();

            mock.Setup(meetingRepository => meetingRepository.Create(mt));
            mock.Setup(meetingRepository => meetingRepository.ChangeDateTime(It.IsAny<Meeting>(), It.IsAny<DateTime>()));
            mock.Setup(meetingRepository => meetingRepository.Exist(mt.idMeet)).Returns(true);

            return mock.Object;
        }

        public static IPlaceRepository GetPlaceRepositoryMock() {
            Mock<IPlaceRepository> mock = new Mock<IPlaceRepository>();
            Place pl = DataGenerator.GeneratePlace();

            mock.Setup(placeRep => placeRep.Add(It.IsAny<Place>()));
            mock.Setup(placeRep => placeRep.Exist(It.IsAny<Int32>())).Returns(true);

            return mock.Object;
        }

        public static ISubscriptionRepository GetSubscriptionRepositoryMock(Subscription sub) {
            Mock<ISubscriptionRepository> mock = new Mock<ISubscriptionRepository>();
            Subscription s = DataGenerator.GenerateSubscription();

            mock.Setup(subRepository => subRepository.Subscribe(s.idUser, 0));

            return mock.Object;
        }
    }
}
