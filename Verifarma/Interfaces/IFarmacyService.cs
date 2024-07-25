using Verifarma.Models;

namespace Verifarma.Interfaces
{
    public interface IFarmacyService
    {
        Task<Farmacia> GetFarmaciaCercana(float lat, float lon);
        Task<Farmacia> GetFarmacia(int id);
    }
}
