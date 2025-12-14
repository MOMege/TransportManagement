using AutoMapper;
using Moq;
using TransportManagement.Application.DTOs.Driveres;
using TransportManagement.Application.Features.Drivers.Commands.CreateDriver;
using TransportManagement.Application.Interfaces;
using TransportManagement.Domain.Entites;

namespace TransportManagement.Tests
{
    public class UnitTest1
    {
        public class MathTests
        {
            [Fact]   // „⁄‰«Â«: Â–« Test Ê«Õœ »œÊ‰ »«—«„ —« 
            public void Add_Returns_Correct_Sum()
            {
                // Arrange
                int a = 2;
                int b = 3;

                // Act
                int result = a + b;

                // Assert
                Assert.Equal(5, result);
            }

            [Fact]
            public void Validator_Should_Fail_When_FullName_Is_Empty()
            {
                var validator = new CreateDriverCommandValidator();

                var dto = new DriverDto(Guid.NewGuid(), "", "0568888999", true);
                var command = new CreateDriverCommand(dto);

                var result = validator.Validate(command);

                Assert.False(result.IsValid);
            }
        }
        public class CreateDriverHandlerTests
        {
            [Fact]
            public async Task Handle_Should_Create_Driver_And_Return_Id()
            {
                // Arrange
                var unitOfWork = new Mock<IUnitOfWork>();
                var mapper = new Mock<IMapper>();

                var dto = new DriverDto(
                               Guid.NewGuid(),
                                 "Ali",
                              "0568879909",
                                   true
                             );
                var command = new CreateDriverCommand(dto);

                var driver = Driver.Create(dto.FullName, dto.PhoneNumber, dto.IsActive);


                mapper.Setup(m => m.Map<Driver>(dto)).Returns(driver);
                unitOfWork.Setup(u => u.Drivers.AddSync(driver)).Returns(Task.CompletedTask);

                var handler = new CreateDriverCommandHandler(unitOfWork.Object, mapper.Object);

                // Act
                var result = await handler.Handle(command, CancellationToken.None);

                // Assert
                Assert.True(result.Succeeded);
                Assert.Equal(driver.Id, result.Data);

                unitOfWork.Verify(u => u.Drivers.AddSync(driver), Times.Once);

            }
        


        }



    }
}