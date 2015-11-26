using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Repositories;
using System.Data.SqlClient;
using System.Configuration;
using ServerLogic.Map;

namespace ServerLogic.Sql {
    public class SubscriptionRepository : ISubscriptionRepository {
        protected IUsersRepository userReposotory;
        protected IMeetingRepository meetingRepository;


        public SubscriptionRepository(IMeetingRepository meetRep, IUsersRepository usrRep) {
            userReposotory = usrRep;
            meetingRepository = meetRep;
        }

        public bool Exist(Subscription sub) {
            if(sub == null)
                throw new ArgumentNullException();
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select idUser from [dbo].[Subscription] where idUser = @idUser and idMeet = @idMeet";
                    command.Parameters.AddWithValue("@idUser", sub.idUser);
                    command.Parameters.AddWithValue("@idMeet", sub.idPlace);
                    using(SqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        return reader.HasRows;
                    }
                }
            }
        }

        public void Subscribe(Guid idUser, int idMeet) {
            if(idUser == null || idMeet == 0)
                throw new ArgumentNullException();
            //TODO check existing user and meeting
            if (!(userReposotory.Exist(idUser) )) {
                throw new ArgumentException("This user not exist!");
            }
            if (!(meetingRepository.Exist(idMeet) )) {
                throw new ArgumentException("This meeting not exist!");
            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
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
                if (!(userReposotory.Exist(idUser) )) {
                    throw new ArgumentNullException("This user not exists!");
                }
                if (!(meetingRepository.Exist(idMeet) )) {
                    throw new ArgumentNullException("This meeting not exists!");
                }
                using(SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                    connection.Open();
                    using (SqlCommand command = connection.CreateCommand()) {
                        command.CommandText = "delete from [dbo].[Subscription] where idMeet = @idMeet and idUser = @idUser";
                        command.Parameters.AddWithValue("@idMeet", idMeet);
                        command.Parameters.AddWithValue("@idUser", idUser);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
