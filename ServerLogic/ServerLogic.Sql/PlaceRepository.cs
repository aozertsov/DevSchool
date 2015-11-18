using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLogic.Sql;
using ServerLogic.Map;
using ServerLogic.Repositories;
using System.Data.SqlClient;
using ServerLogic.Sql.Properties;

namespace ServerLogic.Sql {
    public class PlaceRepository : IPlaceRepository {
        public void Add(Place place) {
            if (place == null)
                throw new ArgumentNullException();
            using (var connection = new SqlConnection(new Settings().connectString)) {
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
        }
    }
}
