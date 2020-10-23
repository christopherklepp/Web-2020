using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2020.Models;

namespace Web2020.DAL
{
    public interface IBussRepository
    {
       Task<Buss> SisteBestilling();
       Task<bool> SettInnData(Buss buss);
       Task<List<Reise>> HentReiser();
       Task<bool> Endre(Reise endretReise);
       Task<Reise> HentEnReise(int id);
       Task<bool> SlettReise(int id);
       Task<bool> Login(Admin admin);
       Task<bool> LagreReise(Reise nyReise);
       Task<bool> ErLoggetInn();
       Task<List<Reise>> HentReiserAdmin();
    }
}
