using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ComplaintRepository : RepositoryBase<Complaint>, IComplaintRepository
{
    public ComplaintRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<PagedResponseDto<ComplaintDto>> GetComplaintsAsync(
        GetRequestPaginatedById requestPaginatedById)
    {
        var totalRecords = await this.DbSet.CountAsync();
        requestPaginatedById.Size = requestPaginatedById.Size == 0 ? 1 : requestPaginatedById.Size;
        var totalPages = (int)Math.Ceiling(totalRecords / (double)requestPaginatedById.Size);
        requestPaginatedById.Page = requestPaginatedById.Page > totalPages ? 1 : requestPaginatedById.Page;
        
        var complaints =  await this.DbSet
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(x => x.Id)
            .Skip((requestPaginatedById.Page - 1) * requestPaginatedById.Size)
            .Take(requestPaginatedById.Size)
            .ToListAsync();
        

        var complaintDtos = complaints.Select(x => new ComplaintDto()
        {
            Id = x.Id,
            Title = x.Title,
            AdditionalInformation1 = x.AdditionalInformation1,
            AdditionalInformation2 = x.AdditionalInformation2,
            AdditionalInformation3 = x.AdditionalInformation3,
            ComplaintType = x.ComplaintType,
            Description = x.Description,
            IsAnonymous = x.IsAnonymous,
            Status = x.Status
        }).ToList();

        return new PagedResponseDto<ComplaintDto>()
        {
            PageNumber = requestPaginatedById.Page,
            PageSize = requestPaginatedById.Size,
            TotalPages = totalPages,
            TotalRecords = totalRecords,
            HasNext = requestPaginatedById.Page < totalPages,
            HasPrevious = requestPaginatedById.Page > 1,
            Data = complaintDtos
        };
    }
}