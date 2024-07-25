using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Runtime.Intrinsics.X86;
using Verifarma.Data;
using Verifarma.Interfaces;
using Verifarma.Models;

namespace Verifarma.Services
{
    public class FarmacyService : IFarmacyService
    {
        private readonly ApplicationDbContext _context;

        public FarmacyService(ApplicationDbContext context) 
        {
            this._context = context;
        }

        public async Task<Farmacia> GetFarmacia(int id)
        {
            var res = await this._context.Farmacias.SingleOrDefaultAsync(x => x.Id == id);
            return res;
        }

        public async Task<Farmacia> GetFarmaciaCercana(float lat, float lon)
        {
            const float radio = 6371.0F;
            var minDist = -1.0;
            var farmacias = await this._context.Farmacias.ToArrayAsync();
            Farmacia farmaciaCercana = null;
            foreach (var farmacia in farmacias)
            {
                var deltaLat = (lat - farmacia.Latitud) * (Math.PI / 180.0);
                var deltaLong = (lon - farmacia.Longitud) * (Math.PI / 180.0);
                var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2) + Math.Cos(lat * (Math.PI / 180.0)) * Math.Cos(farmacia.Latitud * (Math.PI / 180.0)) * Math.Sin(deltaLong / 2) * Math.Sin(deltaLong / 2);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                if (minDist < 0)
                {
                    minDist = radio * c;
                    farmaciaCercana= farmacia;
                }
                else
                {
                    minDist = Math.Min(minDist, radio * c);
                    farmaciaCercana = (Math.Min(minDist, radio * c) == radio*c) ? farmacia : farmaciaCercana;
                }
            }

            return farmaciaCercana;
        }

    }
}
