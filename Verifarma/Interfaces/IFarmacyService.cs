using Verifarma.Models;

namespace Verifarma.Interfaces
{
    public interface IFarmacyService
    {
        Task<double> GetFarmaciaCercana(float lat, float lon);
        Task<Farmacia> GetFarmacia(int id);
    }
}
