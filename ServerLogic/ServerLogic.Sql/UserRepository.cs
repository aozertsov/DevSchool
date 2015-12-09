using System;
using ServerLogic.Map;
using ServerLogic.Repositories;
using System.Data.SqlClient;
using System.Configuration;
using NLog;
using System.Diagnostics;

namespace ServerLogic.Sql {
    public class UserRepository : IUsersRepository {

        Logger logger;

        public UserRepository() {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void ChangeNumber(Users user, string number) {
            logger.Log(LogLevel.Info, $"Start Changing number user with id = {user.idUser} to number = {number}");
            if(user == null || !Exist(user.idUser)) {
                logger.Log(LogLevel.Error, $"User not exist");
                throw new ArgumentNullException();
            }
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "update [dbo].[Users] set contactNumber = @contactNumber where idUser = @id";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    command.Parameters.AddWithValue("@contactNumber", number);
                    command.ExecuteNonQuery();
                    logger.Log(LogLevel.Info, $"End Changing number user with id = {user.idUser} to number = {number}");
                }
            }
        }

        public Users GetUser(Users user) {
            logger.Log(LogLevel.Info, $"Start Getting user with id = {user.idUser}");
            if(user == null || !Exist(user.idUser)) {
                logger.Log(LogLevel.Error, $"User not exist");
                throw new ArgumentNullException($"User not exist");
            }
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select idUser, firstName, lastName, contactNumber from [dbo].[Users] where idUser = @id";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    using (SqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        logger.Log(LogLevel.Info, $"End Getting user with id = {user.idUser}");
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
            logger.Log(LogLevel.Info, $"Start deleting user with id = {user.idUser}");
            if(user == null) {
                logger.Log(LogLevel.Error, $"User not exist");
                throw new ArgumentNullException();
            }
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                //TODO parse email and contactNumber
                //TODO check for existing
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "delete from [dbo].[Users] where idUser = @id";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    //command.Parameters.AddWithValue("@email", user.email);
                    command.ExecuteNonQuery();
                    logger.Log(LogLevel.Info, $"End deleting user with id = {user.idUser}");
                }
            }
        }

        public void Create(Users user) {
            logger.Log(LogLevel.Info, $"Start creating user with id = {user.idUser}");
            if(user == null) {
                logger.Log(LogLevel.Error, $"User is null");
                throw new ArgumentNullException();
            }
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                //TODO parse email and contactNumber
                //TODO check for unique
                using (var command = connection.CreateCommand()) {
                    command.CommandText = "insert into [dbo].[Users] (idUser,  firstName, lastName, contactNumber, email) values (@Id, @firstName, @lastName, @contactNumber, @email)";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@firstName", user.firstName);
                    command.Parameters.AddWithValue("@lastName", user.lastName);
                    command.Parameters.AddWithValue("@contactNumber", user.contactNumber);
                    command.ExecuteNonQuery();
                    logger.Log(LogLevel.Info, $"Start creating user with id = {user.idUser}");
                }
            }
        }

        public bool Exist(Guid idUser) {
            logger.Log(LogLevel.Info, $"Start checking user with id = {idUser}");
            if(idUser == null) {
                logger.Log(LogLevel.Error, $"id User is null");
                throw new ArgumentNullException();
            }
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select idUser, firstName, lastName, contactNumber from [dbo].[Users] where idUser = @id";
                    command.Parameters.AddWithValue("@id", idUser);
                    using(SqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        logger.Log(LogLevel.Info, $"End checking user with id = {idUser}");
                        return reader.HasRows;
                    }
                }
            }
        }

        public bool Exist(string email) {
            logger.Log(LogLevel.Info, $"Start checking user with email = {email}");
            if(email == null || email == "") {
                logger.Log(LogLevel.Error, $"id User is null");
                throw new ArgumentNullException();
            }
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select idUser, firstName, lastName, contactNumber from [dbo].[Users] where email = @email";
                    command.Parameters.AddWithValue("@email", email);
                    using(SqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        logger.Log(LogLevel.Info, $"End checking user with email = {email}");
                        return reader.HasRows;
                    }
                }
            }
        }
    }
}
