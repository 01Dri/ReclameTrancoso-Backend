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

    public async Task<PagedResponseOffsetDto<ComplaintDto>> GetComplaintsAsync(
        GetByIdRequestPaginated requestPaginated)
    {
        var totalRecords = await this.DbSet.CountAsync();
        requestPaginated.Size = requestPaginated.Size == 0 ? 1 : requestPaginated.Size;
        var totalPages = (int)Math.Ceiling(totalRecords / (double)requestPaginated.Size);
        requestPaginated.Page = requestPaginated.Page > totalPages ? 1 : requestPaginated.Page;
        
        var complaints =  await this.DbSet
            .AsNoTrackingWithIdentityResolution()
            .OrderBy(x => x.Id)
            .Skip((requestPaginated.Page - 1) * requestPaginated.Size)
            .Take(requestPaginated.Size)
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

        return new PagedResponseOffsetDto<ComplaintDto>()
        {
            PageNumber = requestPaginated.Page,
            PageSize = requestPaginated.Size,
            TotalPages = totalPages,
            TotalRecords = totalRecords,
            HasNext = requestPaginated.Page < totalPages,
            HasPrevious = requestPaginated.Page > 1,
            Data = complaintDtos
        };
    }
}