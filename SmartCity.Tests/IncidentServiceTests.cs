using FluentAssertions;
using Moq;
using SmartCity.Application.DTOs;
using SmartCity.Application.Interfaces;
using SmartCity.Application.Services;
using SmartCity.Domain.Entities;
using SmartCity.Domain.Enums;

namespace SmartCity.Tests
{
    public class IncidentServiceTests
    {
        private readonly Mock<IIncidentRepository> _repositoryMock;
        private readonly IIncidentService _service;

        public IncidentServiceTests()
        {
            _repositoryMock = new Mock<IIncidentRepository>();
            _service = new IncidentService(_repositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_Should_Create_Incident()
        {
            var dto = new CreateIncidentDto
            {
                Title = "Test incident",
                Description = "Test description",
                Type = Domain.Enums.IncidentType.Fire,
                LocationId = Guid.NewGuid()
            };

            _repositoryMock
                .Setup(r => r.AddAsync(It.IsAny<Incident>()))
                .Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            _repositoryMock
                .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => new Incident
                {
                    Id = id,
                    Title = dto.Title,
                    Description = dto.Description,
                    Type = dto.Type,
                    Status = IncidentStatus.Reported,
                    CreatedAt = DateTime.UtcNow,
                    LocationId = dto.LocationId
                });

            var result = await _service.CreateAsync(dto);

            result.Should().NotBeNull();
            result.Title.Should().Be(dto.Title);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Incident>()), Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_Should_Throw_Exception_When_Title_Is_Empty()
        {
            var dto = new CreateIncidentDto
            {
                Title = "",
                Description = "Test",
                Type = IncidentType.Fire,
                LocationId = Guid.NewGuid()
            };

            Func<Task> act = async () => await _service.CreateAsync(dto);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Title is required");
        }

    }
}
