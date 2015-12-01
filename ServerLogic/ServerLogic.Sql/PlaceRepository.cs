using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Sql;
using ServerLogic.Map;
using ServerLogic.Repositories;
using System.Data.SqlClient;
using System.Configuration;
using NLog;

namespace ServerLogic.Sql {
    public class PlaceRepository : IPlaceRepository {
        Logger logger;

        public PlaceRepository() {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Add(Place place) {
            logger.Log(LogLevel.Info, $"Start adding new place with id = {place.idPlace}");
            if(place == null) { 
                logger.Log(LogLevel.Error, $"Adding place is null");
                throw new ArgumentNullException();
            }
            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using (var command = connection.CreateCommand()) {
                    command.CommandText = "insert into [dbo].[Place] (country, city, street, house) values (@country, @city, @street, @house)";
                    command.Parameters.AddWithValue("@country", place.country);
                    command.Parameters.AddWithValue("@city", place.city);
                    command.Parameters.AddWithValue("@street", place.street);
                    command.Parameters.AddWithValue("@house", place.house);
                    command.ExecuteNonQuery();
                }
            }
            logger.Log(LogLevel.Info, $"End adding new place with id = {place.idPlace}");
        }

        public bool Exist(int idPlace) {
            logger.Log(LogLevel.Info, $"Start checking place with id = {idPlace}");
            using(var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSchoolDB"].ConnectionString)) {
                connection.Open();
                using(var command = connection.CreateCommand()) {
                    command.CommandText = "select house from [dbo].[Place] where idPlace = @idPlace";
                    command.Parameters.AddWithValue("@idPlace", idPlace);
                    using(var reader = command.ExecuteReader()) {
                        reader.Read();
                        logger.Log(LogLevel.Info, $"End checking place with id = {idPlace}");
                        return reader.HasRows;
                    }
                }
            }
        }

    }
}
