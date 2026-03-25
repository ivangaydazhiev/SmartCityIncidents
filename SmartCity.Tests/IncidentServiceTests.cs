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

        [Fact]
        public async Task UpdateAsync_Shoud_Update_Incident()
        {
            var id = Guid.NewGuid();

            var existingIncident = new Incident
            {
                Id = id,
                Title = "Old Title",
                Description = "Old Desc",
                Type = IncidentType.Fire,
                Status = IncidentStatus.Reported,
                LocationId = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow
            };

            var dto = new UpdateIncidentDto
            {
                Title = "New Title",
                Description = "New Desc",
                Type = IncidentType.PowerOutage,
                Status = IncidentStatus.Resolved,
                LocationId = existingIncident.LocationId
            };

            _repositoryMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync(existingIncident);

            _repositoryMock
                .Setup(r => r.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            var result = await _service.UpdateAsync(id, dto);

            result.Title.Should().Be("New Title");
            result.Description.Should().Be("New Desc");
            result.Type.Should().Be((int)IncidentType.PowerOutage);
            result.Status.Should().Be((int)IncidentStatus.Resolved);

            _repositoryMock .Verify(r => r.Update(It.IsAny<Incident>()),Times.Once);
            _repositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_Should_Throw_When_NotFound()
        {
            var id = Guid.NewGuid();

            _repositoryMock
                .Setup(r => r.GetByIdAsync(id))
                .ReturnsAsync((Incident?)null);

            var dto = new UpdateIncidentDto
            {
                Title = "Test",
                Description = "Test",
                Type = IncidentType.Fire,
                Status = IncidentStatus.Reported,
                LocationId = Guid.NewGuid()
            };

            Func<Task> act = async () => await _service.UpdateAsync(id, dto);

            await act.Should().ThrowAsync<KeyNotFoundException>();
        }

    }
}
