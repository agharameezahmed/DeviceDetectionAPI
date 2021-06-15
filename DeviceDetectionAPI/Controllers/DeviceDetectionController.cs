using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wangkanai.Detection.Services;

namespace DeviceDetectionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceDetectionController : ControllerBase
    {
        private readonly IDetectionService _detectionService;
        public DeviceDetectionController(IDetectionService detectionService)
        {
            _detectionService = detectionService;
        }
        /// <summary>
        /// classify the device in terms of its type (tablet, phone, desktop) and its operating system
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Task.FromResult(new DeviceDetectionModel
                {
                    DeviceType = _detectionService.Device.Type.ToString(),
                    OperatingSystem = _detectionService.Platform.Name.ToString(),
                }));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
