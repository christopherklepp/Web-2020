using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2020.Models;

namespace Web2020.DAL
{
    public interface IBussRepository
    {
       Task<List<Buss>> HentAlle();
       Task<bool> SettInnData(Buss buss);
    }
}
