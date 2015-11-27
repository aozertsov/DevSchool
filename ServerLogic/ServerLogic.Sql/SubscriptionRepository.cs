using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Repositories;
using System.Data.SqlClient;
using System.Configuration;
using ServerLogic.Map;
using NLog;

namespace ServerLogic.Sql {
    public class SubscriptionRepository : ISubscriptionRepository {
        protected IUsersRepository userReposotory;
        protected IMeetingRepository meetingRepository;
        Logger logger;


        public SubscriptionRepository(IMeetingRepository meetRep, IUsersRepository usrRep) {
            userReposotory = usrRep;
            meetingRepository = meetRep;
            logger = LogManager.GetCurrentClassLogger();
        }

        public bool Exist(Subscription sub) {
            logger.Log(LogLevel.Info, $"Start checking subscription with idUser = {sub.idUser}, place = {sub.idPlace}");
            if(sub == null) {
                logger.Log(LogLevel.Error, $"Subscription is null");
                throw new ArgumentNullException();
            }
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select idUser from [dbo].[Subscription] where idUser = @idUser and idMeet = @idMeet";
                    command.Parameters.AddWithValue("@idUser", sub.idUser);
                    command.Parameters.AddWithValue("@idMeet", sub.idPlace);
                    using(SqlDataReader reader = command.ExecuteReader()) {
                        reader.Read();
                        logger.Log(LogLevel.Info, $"End checking subscription with idUser = {sub.idUser}, place = {sub.idPlace}");
                        return reader.HasRows;
                    }
                }
            }
        }

        public void Subscribe(Guid idUser, int idMeet) {
            logger.Log(LogLevel.Info, $"Start subscribe idUser = {idUser} to meet = {idMeet}");
            if(idUser == null || idMeet == 0) {
                logger.Log(LogLevel.Error, $"Some argument idUser = {idUser} or meet = {idMeet} is null");
                throw new ArgumentNullException();
            }
            //TODO check existing user and meeting
            if (!(userReposotory.Exist(idUser) )) {
                logger.Log(LogLevel.Error, $"Not exist idUser = {idUser}");
                throw new ArgumentException("This user not exist!");
            }
            if (!(meetingRepository.Exist(idMeet) )) {
                logger.Log(LogLevel.Error, $"Not exist meet = {idMeet}");
                throw new ArgumentException("This meeting not exist!");
            }
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand()) {
                    command.CommandText = "insert into [dbo].[Subscription] (idMeet, idUser) values (@idMeet, @idUser)";
                    command.Parameters.AddWithValue("@idMeet", idMeet);
                    command.Parameters.AddWithValue("@idUser", idUser);
                    command.ExecuteNonQuery();
                    logger.Log(LogLevel.Info, $"End subscribe idUser = {idUser} to meet = {idMeet}");
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
