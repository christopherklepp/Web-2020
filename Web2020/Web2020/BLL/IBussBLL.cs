using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web2020.Models;

namespace Web2020.BLL
{
    public interface IBussBLL
    {
        Task<Buss> SisteBestilling();
        Task<bool> SettInnData(Buss buss);
        Task<List<Reise>> HentReiser();
        Task<bool> Endre(Reise endretReise);
        Task<Reise> HentEnReise(int id);
        Task<bool> SlettReise(int id);
    }
}
