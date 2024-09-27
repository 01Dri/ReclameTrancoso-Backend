using Application.UseCases.Complaint;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;
using Moq;
using ReclameTrancoso.Exceptions.Exceptions;
using Xunit;

namespace ReclameTrancoso.Tests.Unit.UseCases;

public class ComplaintsGetByResidentIdUseCaseTests
{
    private readonly Mock<IResidentComplaintRepository> _residentComplaintRepository;  
    private readonly ComplaintsGetByResidentIdUseCase _useCase;

    public ComplaintsGetByResidentIdUseCaseTests()
    {
        _residentComplaintRepository = new Mock<IResidentComplaintRepository>();
        _useCase = new ComplaintsGetByResidentIdUseCase(_residentComplaintRepository.Object);
    }
    
    [Fact]
    public async Task Test_Successfully()
    {
        var request = new GetRequestPaginatedById { Id = 1, Page = 1, Size = 10 };
        var pagedResponse = new PagedResponseDto<ComplaintDto>
        {
            PageNumber = 1,
            PageSize = 10,
            TotalPages = 1,
            TotalRecords = 1,
            Data = new List<ComplaintDto> { new ComplaintDto { Title = "Test Complaint" } }
        };

        _residentComplaintRepository.Setup(repo => repo.ExistsByResidentIdAsync(request.Id)).ReturnsAsync(true); 
        _residentComplaintRepository.Setup(repo => repo.GetComplaintsByIdAsync(request)).ReturnsAsync(pagedResponse);

        var result = await _useCase.Handle(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(1, result.TotalRecords);
        Assert.Single(result.Data);
        Assert.Equal("Test Complaint", result.Data[0].Title);

        _residentComplaintRepository.Verify(repo => repo.ExistsByResidentIdAsync(request.Id), Times.Once);
        _residentComplaintRepository.Verify(repo => repo.GetComplaintsByIdAsync(request), Times.Once);
    }

    [Fact]
    public async Task Test_ShouldToReturnNotFoundException()
    {
        var request = new GetRequestPaginatedById()
        {
            Id = 1,
            Page = 2,
            Size = 5
        };
        _residentComplaintRepository.Setup(x =>
                x.ExistsByResidentIdAsync(request.Id))
            .ReturnsAsync(false);
        
        var resultException = await Assert.ThrowsAsync<NotFoundException>(() =>
            _useCase.Handle(request, CancellationToken.None));
        
        Assert.Equal("Residente não possui tickets cadastrados.", resultException.Message);

        _residentComplaintRepository.Verify(x => x.ExistsByResidentIdAsync(It.IsAny<long?>()), Times.Once);
        _residentComplaintRepository.Verify(x => x.GetComplaintsByIdAsync(request), Times.Never);

    }
   
}