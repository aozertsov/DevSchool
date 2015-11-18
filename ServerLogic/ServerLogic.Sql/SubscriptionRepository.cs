using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Repositories;
using System.Data.SqlClient;
using ServerLogic.Sql.Properties;

namespace ServerLogic.Sql {
    public class SubscriptionRepository : ISubscriptionRepository {
        public void Subscribe(Guid idUser, int idMeet) {
            if(idUser == null || idMeet == 0)
                throw new ArgumentNullException();
            //TODO check existing user and meeting
            using(var connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "select * from [dbo].[Users] where idUser = @idUser";
                    command.Parameters.AddWithValue("@idUser", idUser);
                    using(var reader = command.ExecuteReader()) {
                        reader.Read();
                        if(!reader.HasRows)
                            throw new ArgumentException("this user doesn't exist");
                    }
                }
            }
            using(var connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using(SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "select * from [dbo].[Meeting] where idMeet = @idMeet";
                    command.Parameters.AddWithValue("@idMeet", idMeet);
                    using(var reader = command.ExecuteReader()) {
                        reader.Read();
                        if(!reader.HasRows)
                            throw new ArgumentException("this meet doesn't exist");
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "insert into [dbo].[Subscription] (idMeet, idUser) values (@idMeet, @idUser)";
                    command.Parameters.AddWithValue("@idMeet", idMeet);
                    command.Parameters.AddWithValue("@idUser", idUser);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UnSubscribe(Guid idUser, int idMeet) {
            if (idUser != null || idMeet != 0 ){
                //TODO check existing user and meeting
                using(var connection = new SqlConnection(new Settings().connectString)) {
                    connection.Open();
                    using(SqlCommand command = connection.CreateCommand()) {
                        command.CommandText = "select * from [dbo].[Users] where idUser = @idUser";
                        command.Parameters.AddWithValue("@idUser", idUser);
                        using(var reader = command.ExecuteReader()) {
                            reader.Read();
                            if(!reader.HasRows)
                                throw new ArgumentException("this user doesn't exist");
                        }
                    }
                }
                using(var connection = new SqlConnection(new Settings().connectString)) {
                    connection.Open();
                    using(SqlCommand command = connection.CreateCommand()) {
                        command.CommandText = "select * from [dbo].[Meeting] where idMeet = @idMeet";
                        command.Parameters.AddWithValue("@idMeet", idMeet);
                        using(var reader = command.ExecuteReader()) {
                            reader.Read();
                            if(!reader.HasRows)
                                throw new ArgumentException("this meet doesn't exist");
                        }
                    }
                }
                using(SqlConnection connection = new SqlConnection(new Settings().connectString)) {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand()) {
                        command.CommandText = "delete from [dbo].[Subscription] (idMeet, idUser) where idMeet = @idMeet, idUser = @idUser";
                        command.Parameters.AddWithValue("@idMeet", idMeet);
                        command.Parameters.AddWithValue("@idUser", idUser);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
