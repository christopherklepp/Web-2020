using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web2020.DAL;
using Web2020.Models;

namespace Web2020.BLL
{
    public class BussBLL : IBussBLL
    {

        private IBussRepository _bussRepository;
        public BussBLL(IBussRepository bussRepository)
        {
            _bussRepository = bussRepository;
        }

        //konstruktør for testing
        /*
        public BussBLL(IBussRepository stub)
        {
            _bussRepository = stub;
        }
        */

        //Returnerer en liste med alle steder fra databasen
        public async Task<List<Reise>> HentReiser()
        {
            return await _bussRepository.HentReiser();
        }



        //Leegger til et sted i stedsdatabasen
        public async Task<bool> SettInnData(Buss buss)
        {
            return await _bussRepository.SettInnData(buss);
        }

        
        public async Task<bool> Endre(Reise endretReise)
        {
            return await _bussRepository.Endre(endretReise);
        }


        public async Task<Buss> SisteBestilling()
        {
            return await _bussRepository.SisteBestilling();
        }


        public async Task<Reise> HentEnReise(int id)
        {
            return await _bussRepository.HentEnReise(id);
        }


        public async Task<bool> SlettReise(int id)
        {
            return await _bussRepository.SlettReise(id);
        }




    }
}
