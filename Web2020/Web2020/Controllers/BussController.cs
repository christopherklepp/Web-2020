using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Web2020.Models;
using Web2020.DAL;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace Web2020.Controllers
{
    [Route("[controller]/[action]")]

    public class BussController : ControllerBase
    {
        private IBussRepository _db;
        private ILogger<BussController> _log;
        private const string _loggetInn = "loggetInn";

        public BussController(IBussRepository db, ILogger<BussController> log)
        {
            _db = db;
            _log = log;
        }
        public async Task<ActionResult> Endre(Reise endreReise)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Endre(endreReise);
                if (!returOK)
                {
                    _log.LogInformation("Endring kunne ikke utføres");
                    return NotFound("Endring kunne ikke utføres");
                }
                return Ok("Reise endret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> SettInnData(Buss buss)
        {
            //LAGT TIL
            
            if (ModelState.IsValid)
            {
                bool returOK = await _db.SettInnData(buss);
                if (!returOK)
                {
                    _log.LogInformation("Bestilling ikke lagret");
                    return BadRequest("Bestilling ikke lagret");

                }
                _log.LogInformation("Bestilling lagret");
                return Ok("Bestilling lagret");
            }
            else
            {
                _log.LogInformation("Feil i inputvalidering");
                return BadRequest("Feil i inputvalidering på server");
            }

        }

       
        public async Task<ActionResult> SisteBestilling()
        {
            Buss sisteBestilling = await _db.SisteBestilling();

            if (sisteBestilling == null)
            {
                _log.LogInformation("Fant ikke siste bestilling");
                return BadRequest("Fant ikke siste bestilling");

            }
            _log.LogInformation("Fant siste bestilling");
            return Ok(sisteBestilling);
            
        }



        public async Task <ActionResult> HentReiser()
        {
            
            List<Reise> alleResier = await _db.HentReiser();
            return Ok(alleResier);
        }

        public async Task<ActionResult> HentReiserAdmin()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("ikke logget inn");
            }
            List<Reise> alleResier = await _db.HentReiserAdmin();
            return Ok(alleResier);
        }
        

        public async Task<ActionResult> HentEnReise(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            Reise enReise = await _db.HentEnReise(id);

            if (enReise == null)
            {
                _log.LogInformation("Fant ikke reisen");
                return NotFound("Fant ikke reisen");

            }
            _log.LogInformation("Fant reisen");
            return Ok(enReise);
        }



        public async Task<ActionResult> Login(Admin admin)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.Login(admin);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker");
                    HttpContext.Session.SetString(_loggetInn, "");
                    return Ok(false);
                }
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }


        public async Task<ActionResult> LagreReise(Reise nyReise)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.LagreReise(nyReise);
                if (!returOK)
                {
                    _log.LogInformation("Reise kunne ikke lagres");
                    return NotFound("Reise kunne ikke lagres");
                }
                _log.LogInformation("Reise lagret");
                return Ok("Reise lagret");
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        
        public async Task<ActionResult> SlettReise(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("Ikke logget inn");
            }

            bool returOK = await _db.SlettReise(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av Kunden ble ikke utført");
                return NotFound("Sletting av Kunden ble ikke utført");
            }
            _log.LogInformation("Sletting utført");
            return Ok("Reise slettet");

        }

        public async Task<ActionResult> ErLoggetInn()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                return Unauthorized("ikke logget inn");
            }
            return Ok("Logget inn");
           
        }

        public void LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
        }
    }
}
