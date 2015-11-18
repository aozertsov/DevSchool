using System;
using ServerLogic.Map;
using ServerLogic.Repositories;
using System.Data.SqlClient;
using ServerLogic.Sql.Properties;

namespace ServerLogic.Sql {
    public class UserRepository : IUsersRepository {

        public void ChangeNumber(Users user, string number) {
            if(user == null)
                throw new ArgumentNullException();
            using(var connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "insert into [dbo].[Users] (contactNumber) values (@contactNumber) where idUser = @id";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    command.Parameters.AddWithValue("@contactNumber", number);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void GetUser(Users user) {
            if(user == null)
                throw new ArgumentNullException();
            using(var connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select idUser, firstName, lastName, contactNumber, email from [dbo].[Users] where idUser = @id";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Delete(Users user) {
            if(user == null)
                throw new ArgumentNullException();
            using(var connection = new SqlConnection(new Settings().connectString)) {
                connection.Open();
                //TODO parse email and contactNumber
                //TODO check for existing
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "delete from [dbo].[Users] where idUser = @id,  firstName = @firstName, lastName = @lastName, contactNumber = @contactNumber, email = @email";
                    command.Parameters.AddWithValue("@id", user.idUser);
                    command.Parameters.AddWithValue("@email", user.email);
                    command.Parameters.AddWithValue("@firstName", user.firstName);
                    command.Parameters.AddWithValue("@lastName", user.lastName);
                    command.Parameters.AddWithValue("@contactNumber", user.contactNumber);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Greate(Users user) {
            if(user == null)
                throw new ArgumentNullException();
            using (var connection = new SqlConnection(new Settings().connectString)) {
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
    }
}
