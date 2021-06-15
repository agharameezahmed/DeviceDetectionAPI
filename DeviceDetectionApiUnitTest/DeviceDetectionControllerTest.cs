using DeviceDetectionAPI;
using DeviceDetectionAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Wangkanai.Detection.Services;
using Xunit;

namespace DeviceDetectionApiUnitTest
{
    public class DeviceDetectionControllerTest
    {
        private readonly DeviceDetectionController _deviceDetectionController;
        private readonly Mock<IDetectionService> _detectionService;
        private readonly Mock<IDeviceService> _deviceService;
        private readonly Mock<IPlatformService> _platformService;

        public DeviceDetectionControllerTest()
        {
            _detectionService = new Mock<IDetectionService>();
            _deviceService = new Mock<IDeviceService>();
            _platformService = new Mock<IPlatformService>();
            _deviceDetectionController = new DeviceDetectionController(_detectionService.Object);
        }

        [Fact]
        public async Task GetDeviceData_Success()
        {
            // Arrange
            _deviceService.SetupGet(x => x.Type).Returns(Wangkanai.Detection.Models.Device.Mobile);
            _platformService.SetupGet(x => x.Name).Returns(Wangkanai.Detection.Models.Platform.iOS);
            _detectionService.SetupGet(x => x.Device).Returns(_deviceService.Object);
            _detectionService.SetupGet(x => x.Platform).Returns(_platformService.Object);

            // Act
            var result = await _deviceDetectionController.Get() as OkObjectResult;
            var model = result.Value as DeviceDetectionModel;

            // Assert
            Assert.NotNull(model);
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.OK);
            Assert.Equal(model.DeviceType, Wangkanai.Detection.Models.Device.Mobile.ToString());
            Assert.Equal(model.OperatingSystem, Wangkanai.Detection.Models.Platform.iOS.ToString());
        }

        [Fact]
        public async Task GetDeviceData_Fail()
        {
            // Arrange

            // Act
            var result = await _deviceDetectionController.Get() as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(result.StatusCode, (int)HttpStatusCode.BadRequest);
        }
    }
}
