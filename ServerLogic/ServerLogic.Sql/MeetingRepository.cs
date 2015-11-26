using System;
using ServerLogic.Repositories;
using ServerLogic.Map;
using System.Data.SqlClient;
using System.Configuration;

namespace ServerLogic.Sql
{
    public class MeetingRepository : IMeetingRepository {
        private readonly IPlaceRepository placeRep;

        public MeetingRepository(IPlaceRepository plRep) {
            placeRep = plRep;
        }

        public bool Exist(int idMeet) {
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "select idMeet from [dbo].[Meeting] where idMeet = @idMeet";
                    command.Parameters.AddWithValue("@idMeet", idMeet);
                    using (var reader = command.ExecuteReader()) {
                        reader.Read();
                        return reader.HasRows;
                    }
                }
            }
        }

        public void Create(Meeting meeting) {
            if(meeting == null)
                throw new ArgumentNullException();
            if (!(placeRep.Exist(meeting.place) )) {
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
                }
            }
        }

        public void ChangeDateTime(Meeting meeting, DateTime date) {
            if(date == null)
                throw new ArgumentNullException();
            if(!(placeRep.Exist(meeting.place) )) {
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
        }

    }
}
