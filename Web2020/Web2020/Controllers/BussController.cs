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
            _log = log;
        }

        /*
        public async Task<bool> SettInnData(Buss buss)
        {

            if (ModelState.IsValid)
            {
                return await _db.SettInnData(buss);
            }
            return false;
        }*/

        /*
        public async Task<List<Reise>> HentReiser()
        {
            _log.LogInformation("HentReiser funket");
            return await _db.HentReiser();
        }*/

        /*
        public async Task<Reise> HentEnReise(int id)
        {
            return await _db.HentEnReise(id);
        }*/

        /*
       public async Task<Buss> SisteBestilling()
       {
           return await _db.SisteBestilling();
       }*/

        public async Task<ActionResult> SettInnData(Buss buss)
        {

            bool returOK = await _db.SettInnData(buss);
            if (!returOK)
            {
                _log.LogInformation("Bestilling ikke lagret");
                return BadRequest("Bestilling ikke lagret");

            }
            return Ok("Bestilling lagret");

        }

       
        public async Task<ActionResult> SisteBestilling()
        {
            Buss sisteBestilling = await _db.SisteBestilling();

            if (sisteBestilling == null)
            {
                _log.LogInformation("Fant ikke siste bestilling");
                return BadRequest("Fant ikke siste bestilling");

            }
            return Ok("bestilling funnet");
            
        }



        public async Task<ActionResult<Reise>> HentReiser()
        {
            List<Reise> alleResier = await _db.HentReiser();
            return Ok(alleResier);
        }


        public async Task<ActionResult> HentEnReise(int id)
        {
            Reise enReise = await _db.HentEnReise(id);

            if (enReise == null!)
            {
                _log.LogInformation("Fant ikke reisen");
                return BadRequest("Fant ikke reisen");

            }
            return Ok("Reise funnet");
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


        public async Task<List<Reise>> HentAlleReiser()
        {
            return await _db.HentAlleReiser();
        }


    }
}
