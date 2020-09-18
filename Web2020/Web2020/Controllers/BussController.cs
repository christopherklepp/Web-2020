
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Web2020.Models;
using Web2020.DAL;

namespace Web2020.Controllers
{
    [Route("[controller]/[action]")]

    public class BussController :ControllerBase
    {
        private readonly IBussRepository _db;
        public BussController(IBussRepository db)
        {
            _db = db;
        }

        public async Task<bool> SettInnData(Buss buss)
         {
             return await _db.SettInnData(buss);
         }

         public async Task<List<Buss>> HentAlle()
         {
             return await _db.HentAlle();
         }
    }
}
