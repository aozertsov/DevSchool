using System;
using ServerLogic.Map;
using ServerLogic.Repositories;
using System.Data.SqlClient;
using System.Configuration;

namespace ServerLogic.Sql {
    public class UserRepository : IUsersRepository {

        public void ChangeNumber(Users user, string number) {
            if(user == null)
                throw new ArgumentNullException();
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "update [dbo].[Users] set contactNumber = @contactNumber where idUser = @id";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    command.Parameters.AddWithValue("@contactNumber", number);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Users GetUser(Users user) {
            if(user == null)
                throw new ArgumentNullException();
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select idUser, firstName, lastName, contactNumber from [dbo].[Users] where idUser = @id";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        return new Users {
                            idUser = reader.GetGuid(reader.GetOrdinal("idUser")),
                            contactNumber = reader.GetString(reader.GetOrdinal("contactNumber")),
                     //       email = reader.GetString(reader.GetOrdinal("email")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                        };
                    }
                }
            }
        }

        public void Delete(Users user) {
            if(user == null)
                throw new ArgumentNullException();
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                //TODO parse email and contactNumber
                //TODO check for existing
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "delete from [dbo].[Users] where idUser = @id";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    //command.Parameters.AddWithValue("@email", user.email);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Create(Users user) {
            if(user == null)
                throw new ArgumentNullException();
            var c = ConfigurationManager.ConnectionStrings;
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                //TODO parse email and contactNumber
                //TODO check for unique
                using (var command = connection.CreateCommand()) {
                    command.CommandText = "insert into [dbo].[Users] (idUser,  firstName, lastName, contactNumber) values (@Id, @firstName, @lastName, @contactNumber)";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    //command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@firstName", user.firstName);
                    command.Parameters.AddWithValue("@lastName", user.lastName);
                    command.Parameters.AddWithValue("@contactNumber", user.contactNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        public bool Exist(Guid idUser) {
            if(idUser == null)
                throw new ArgumentNullException();
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select idUser, firstName, lastName, contactNumber from [dbo].[Users] where idUser = @id";
                    command.Parameters.AddWithValue("@id", idUser);
                    using(SqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        return reader.HasRows;
                    }
                }
            }
        }
    }
}
