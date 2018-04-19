using System.Collections.Generic;
using Film_Webshop.Models;

namespace Film_Webshop.Context
{
    public interface IWinkelmandContext
    {
        List<int> SelectAll(int accId);
        int GetWinkelmandId(int accId);
        void Insert(int accountId, int filmId);
        void Delete(int winkelmandId, int filmId);

    }
}
