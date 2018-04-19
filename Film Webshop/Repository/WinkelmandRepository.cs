using System.Collections.Generic;
using Film_Webshop.Context.MSSQL;

namespace Film_Webshop.Repository
{
    public class WinkelmandRepository
    {
        private readonly MssqlWinkelmandContext _winkelmandContext;

        public WinkelmandRepository(MssqlWinkelmandContext winkelmandContext)
        {
            this._winkelmandContext = winkelmandContext;
        }

        public List<int> GetFilmsIdsInWinkelmand(int winkelmandId)
        {
            return _winkelmandContext.SelectAll(winkelmandId);
        }

        public void AddFilmWithId(int winkelmandId, int filmId)
        {
            _winkelmandContext.Insert(winkelmandId, filmId);
        }

        public void RemoveFilmWithId(int winkelmandId, int filmId)
        {
            _winkelmandContext.Delete(winkelmandId, filmId);
        }

        public int GetWinkelmandId(int accId)
        {
            return _winkelmandContext.GetWinkelmandId(accId);
        }
    }
}