using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Web2020.Models;
using Web2020.DAL;
using Microsoft.Extensions.Logging;


namespace Web2020.Controllers
{
    [Route("[controller]/[action]")]

    public class BussController : ControllerBase
    {
        private IBussRepository _db;
        private ILogger<BussController> _log;

        public BussController(IBussRepository db, ILogger<BussController> log)
        {
            _db = db;
        }

        public async Task<bool> SettInnData(Buss buss)
        {

            if (ModelState.IsValid)
            {
                return await _db.SettInnData(buss);
            }
            return false;
        }

        public async Task<Buss> SisteBestilling()
        {
            return await _db.SisteBestilling();
        }

        public async Task<List<Reise>> HentReiser()
        {
            return await _db.HentReiser();
        }
        public async Task<List<Reise>> HentAlleReiser()
        {
            return await _db.HentAlleReiser();
        }

        public async Task<ActionResult> Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.Login(admin);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker" + admin.Brukernavn);
                    return Ok(false);
                }
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }


    }
}
