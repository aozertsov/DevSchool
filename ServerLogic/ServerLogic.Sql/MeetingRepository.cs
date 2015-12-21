using System;
using ServerLogic.Repositories;
using ServerLogic.Map;
using System.Data.SqlClient;
using System.Configuration;
using NLog;

namespace ServerLogic.Sql
{
    using System.Collections.Generic;

    public class MeetingRepository : IMeetingRepository {
        private readonly IPlaceRepository placeRep;
        Logger logger;

        public MeetingRepository(IPlaceRepository plRep) {
            placeRep = plRep;
            logger = LogManager.GetCurrentClassLogger();
        }

        public bool Exist(int idMeet) {
            logger.Log(LogLevel.Info, $"Begin checking for existing meeting with id = {idMeet}");
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "select idMeet from [dbo].[Meeting] where idMeet = @idMeet";
                    command.Parameters.AddWithValue("@idMeet", idMeet);
                    using (var reader = command.ExecuteReader()) {
                        reader.Read();
                        logger.Log(LogLevel.Info, $"End checking for existing meeting with id = {idMeet}");
                        return reader.HasRows;
                    }
                }
            }
        }

        public bool Exist(int idPlace, DateTime dateMeet) {
            logger.Log(LogLevel.Info, $"Begin checking for existing meeting");
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "select idMeet from [dbo].[Meeting] where place = @idPlace and dateMeet = @dateMeet";
                    command.Parameters.AddWithValue("@idPlace", idPlace);
                    command.Parameters.AddWithValue("@dateMeet", dateMeet);
                    using(var reader = command.ExecuteReader()) {
                        logger.Log(LogLevel.Info, $"End checking for existing meeting");
                        return reader.HasRows;
                    }
                }
            }
        }

        public int GetId(int idPlace, DateTime dateMeet) {
            logger.Log(LogLevel.Info, $"Begin checking for existing meeting");
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "select idMeet from [dbo].[Meeting] where place = @idPlace and dateMeet = @dateMeet";
                    command.Parameters.AddWithValue("@idPlace", idPlace);
                    command.Parameters.AddWithValue("@dateMeet", dateMeet);
                    using(var reader = command.ExecuteReader()) {
                        reader.Read();
                        logger.Log(LogLevel.Info, $"End checking for existing meeting");
                        return reader.GetInt32(reader.GetOrdinal("idMeet"));
                    }
                }
            }
        }

        public void Create(Meeting meeting) {
            logger.Log(LogLevel.Info, $"Start creating mmeting with id = {meeting.idMeet}");
            if(meeting == null) {
                logger.Log(LogLevel.Error, $"Can't create meeting, because it is null");
                throw new ArgumentNullException();
            }
            if (!(placeRep.Exist(meeting.place) )) {
                logger.Log(LogLevel.Error, $"Can't create meeting, because place = {meeting.place} not exist!");
                throw new ArgumentException("This place not exist!");
            }
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "insert into [dbo].[Meeting] (place, dateMeet) values (@place, @dateMeet)";
                    //command.Parameters.AddWithValue("@idMeet", meeting.idMeet);
                    command.Parameters.AddWithValue("@place", meeting.place);
                    command.Parameters.AddWithValue("@dateMeet", meeting.dateMeet);
                    command.ExecuteNonQuery();
                    logger.Log(LogLevel.Info, $"End creating mmeting with id = {meeting.idMeet}");
                }
            }
        }

        public void ChangeDateTime(Meeting meeting, DateTime date) {
            logger.Log(LogLevel.Info, $"Start change date to meeting with id = {meeting.idMeet} to date = {date}");
            if(date == null) {
                logger.Log(LogLevel.Error, $"Can't change date to meeting with id = {meeting.idMeet} because date is null");
                throw new ArgumentNullException();
            }
            if(!(placeRep.Exist(meeting.place) )) {
                logger.Log(LogLevel.Error, $"Can't change date to meeting with id = {meeting.idMeet} because place = {meeting.place} not exist");
                throw new ArgumentException("This place not exist!");
            }
            using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "update [dbo].[Meeting] set dateMeet = @dateMeet where idMeet = @idMeet and place = @place";
                    command.Parameters.AddWithValue("@dateMeet", date.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@idMeet", meeting.idMeet);
                    command.Parameters.AddWithValue("@place", meeting.place);
                    command.ExecuteNonQuery();
                }
            }
            logger.Log(LogLevel.Info, $"End change date to meeting with id = {meeting.idMeet} to date = {date}");
        }

        public List<Place> Get(Guid idUser) {
            logger.Log(LogLevel.Info, $"Get meetings for user with id = {idUser}");
            using (
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText =
                        "select Place.city, Place.country, Place.street, Place.flat, Place.house from Meeting join Subscription on Meeting.idMeet = Subscription.idMeet join Place on PLace.idPlace = Meeting.place where Subscription.idUser = @idUser";
                    command.Parameters.AddWithValue("@idUser", idUser);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    if (reader.HasRows) {
                        List<Place> result = new List<Place>();
                        do {
                            result.Add(new Place {
                                country = reader.GetString(reader.GetOrdinal("country")),
                                city = reader.GetString(reader.GetOrdinal("city")),
                                street = reader.GetString(reader.GetOrdinal("street")),
                                house = reader.GetInt32(reader.GetOrdinal("house"))
                                //flat = reader.GetInt32(reader.GetOrdinal("flat")),
                            });
                        } while (reader.NextResult());
                        return result;
                    }
                    return null;
                }
            }
        }

        public List<Place> Invitations(string email) {
            logger.Log(LogLevel.Info, $"Get meetings for user with email = {email}");
            using(
                SqlConnection connection =
                    new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText =
                        "select Place.city, Place.country, Place.street, Place.flat, Place.house from Sandbox join Meeting on Meeting.idMeet = Sandbox.meeting join Place on PLace.idPlace = Meeting.place where Sandbox.invated = @invated";
                    command.Parameters.AddWithValue("@invated", email);
                    var reader = command.ExecuteReader();
                    reader.Read();
                    if(reader.HasRows) {
                        List<Place> result = new List<Place>();
                        do {
                            result.Add(new Place {
                                country = reader.GetString(reader.GetOrdinal("country")),
                                city = reader.GetString(reader.GetOrdinal("city")),
                                street = reader.GetString(reader.GetOrdinal("street")),
                                house = reader.GetInt32(reader.GetOrdinal("house")),
                                flat = reader.GetInt32(reader.GetOrdinal("flat")),
                            });
                        } while(reader.NextResult());
                        return result;
                    }
                    return null;
                }
            }
        }
    }
}
