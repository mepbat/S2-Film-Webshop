using System.Collections.Generic;
using Film_Webshop.Models;

namespace Film_Webshop.Context
{
    public interface IWinkelmandContext
    {
        List<Film> GetFilmsInWinkelmand(int winkelmandId);
        int GetWinkelmandId(int accId);
        bool Insert(int accountId, int filmId);
        bool Delete(int winkelmandId, int filmId);
    }
}
