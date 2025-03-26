using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using HjulinstallningAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace HjulinstallningAPI.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly VeCloudService _veCloudService;

        public VehicleController(VeCloudService veCloudService)
        {
            _veCloudService = veCloudService;
        }
        
        [HttpGet("{licensePlate}")]
        public async Task<IActionResult> GetVehicleData(string licensePlate)
        {
            try
            {
                Console.WriteLine($"🔹 Fetching fresh data from VeCloud API for {licensePlate}");

                var vehicleDataXml = await _veCloudService.GetVehicleDataAsync(licensePlate);

                if (string.IsNullOrEmpty(vehicleDataXml))
                    return NotFound(new { message = "Vehicle data not found" });

                return Ok(new { licensePlate, rawResponse = vehicleDataXml });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error fetching vehicle data: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error", error = ex.Message });
            }
        }
    }
}
