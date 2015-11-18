using System;
using ServerLogic.Repositories;
using ServerLogic.Map;
using System.Data.SqlClient;
using ServerLogic.Sql.Properties;

namespace ServerLogic.Sql
{
    public class A {
    }
    public class B : A {
    }
    public class MeetingRepository : IMeetingRepository {
        public void Create(Meeting meeting) {
            if(meeting == null)
                throw new ArgumentNullException();
            //TODO check existing place
            using (var connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "select * from [dbo].[Meeting] where place = @place";
                    command.Parameters.AddWithValue("@place", meeting.place);
                    using (var reader = command.ExecuteReader()) {
                        reader.Read();
                        if(!reader.HasRows)
                            throw new ArgumentException("this place doesn't exist");
                    }
                }
            }
            using(var connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "insert into [dbo].[Meeting] (idMeet, place, dateTime) values (@idMeet, @place, @dateTime)";
                    command.Parameters.AddWithValue("@idMeet", meeting.idMeet);
                    command.Parameters.AddWithValue("@place", meeting.place);
                    command.Parameters.AddWithValue("@dateTime", meeting.dateTime);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ChangeDateTime(Meeting meeting, DateTime date) {
            if(date == null)
                throw new ArgumentNullException();
            //TODO check existing place
            using(var connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "select * from [dbo].[Meeting] where place = @place";
                    command.Parameters.AddWithValue("@place", meeting.place);
                    using(var reader = command.ExecuteReader()) {
                        reader.Read();
                        if(!reader.HasRows)
                            throw new ArgumentException("this place doesn't exist");
                    }
                }
            }
            using(SqlConnection connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "update [dbo].[Meeting] set dateTime = @dateTime where idMeet = @idMeet, place = @place";
                    command.Parameters.AddWithValue("@dateTime", date);
                    command.Parameters.AddWithValue("@idMeet", meeting.idMeet);
                    command.Parameters.AddWithValue("@place", meeting.place);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
