using Domain.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Complaint;
using Domain.Models.Pagination;
using Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ResidentComplaintRepository : RepositoryBase<ResidentComplaint>, IResidentComplaintRepository
{
    public ResidentComplaintRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public async Task<PagedResponseDto<ComplaintDto>> GetComplaintsById(GetRequestPaginated requestPaginated)
    {
        // condition ? consequent : alternative

        var totalRecords = await this.DbSet.Where(x => x.ResidentId == requestPaginated.Id).CountAsync();
        requestPaginated.Size = requestPaginated.Size == 0 ? 1 : requestPaginated.Size;
        var totalPages = (int)Math.Ceiling(totalRecords / (double)requestPaginated.Size);
        requestPaginated.Page = requestPaginated.Page > totalPages ? 1 : requestPaginated.Page;
        
        var complaints = await this.DbSet
            .AsNoTrackingWithIdentityResolution()
            .Include(x => x.Resident)
            .Where(x => x.ResidentId == requestPaginated.Id)
            .Select(x => x.Complaint)
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
        
        return new PagedResponseDto<ComplaintDto>()
        {
            PageNumber = requestPaginated.Page,
            PageSize = requestPaginated.Size,
            TotalPages = totalPages,
            TotalRecords = totalRecords,
            HasNext = totalPages > requestPaginated.Page,
            HasPrevious = requestPaginated.Page > 1,
            Data = complaintDtos
        };
    }

    public async Task<bool> ExistsByResidentIdAsync(long? id)
    {
        return await this.DbSet.AnyAsync(x => x.ResidentId == id);
    }
}