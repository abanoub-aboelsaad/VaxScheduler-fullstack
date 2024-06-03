using Microsoft.AspNetCore.SignalR;
using api.Data;
using api.Dtos.VaccinationCenter;
using System.Linq;
using System.Threading.Tasks;
using api.Service;

namespace api.Hubs
{
    public class VaccinationCenterHub : Hub
    {
        private readonly ApplicationDBContext _context;

        public VaccinationCenterHub(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task SendAllVaccinationCenters()
        {
            var vaccinationCenters = _context.VaccinationCenters.ToList().Select(v => v.ToVaccinationCenterDto());
            await Clients.All.SendAsync("ReceiveAllVaccinationCenters", vaccinationCenters);
        }

        // public async Task SendVaccinationCenterById(int id)
        // {
        //     var vaccinationCenter = _context.VaccinationCenters.Find(id);
        //     if (vaccinationCenter != null)
        //     {
        //         await Clients.Caller.SendAsync("ReceiveVaccinationCenter", vaccinationCenter.ToVaccinationCenterDto());
        //     }
        // }
    }
}
