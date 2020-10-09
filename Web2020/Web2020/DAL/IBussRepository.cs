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
        Task<List<Reise>> HentAlleReiser();
        Task<bool> Login(Admin admin);
    }
}
