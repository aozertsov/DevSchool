using ServerLogic.Map;

namespace ServerLogic.Repositories {
    public interface IPlaceRepository : IRepository {
        void Add(Place place);

        bool Exist(int idPlace);
    }
}
