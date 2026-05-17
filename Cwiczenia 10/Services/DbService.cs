using Cwiczenia_10.Data;
using Cwiczenia_10.DTOs;
using Cwiczenia_10.DTOs.GetPcDetails;
using Cwiczenia_10.Entities;
using Cwiczenia_10.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia_10.Services;

public class DbService : IDbService {
    private readonly AppDbContext _context;

    public DbService(AppDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<GetPcs>> getAllPcs() {
        var pcs = await _context.PCs.Select(e => new GetPcs() {
            Id = e.Id,
            Name = e.Name,
            Weight = e.Weight,
            Warranty = e.Warranty,
            CreatedAt = e.CreatedAt,
            Stock = e.Stock
        }).ToListAsync();

        return pcs;
    }

    public async Task<GetPc> GetPc(int id) {
        var pcs = await _context.PCs.Where(e => e.Id == id).Select(e => new GetPc() {
            Id = e.Id,
            Name = e.Name,
            Weight = e.Weight,
            Warranty = e.Warranty,
            CreatedAt = e.CreatedAt,
            Stock = e.Stock,
            Components = e.PCComponents.Select(pc => new ComponentsDetails() {
                Amount = pc.Amount,
                Component = new ComponentDetails() {
                    Code = pc.Components.Code,
                    Name = pc.Components.Name,
                    Description = pc.Components.Description,
                    Manufacturer = new ManufacturerDetails() {
                        Id = pc.Components.ComponentManufacturers.Id,
                        Abbreviation = pc.Components.ComponentManufacturers.Abbreviation,
                        FullName = pc.Components.ComponentManufacturers.FullName,
                        FoundationDate = pc.Components.ComponentManufacturers.FoundationDate
                    },
                    Type = new TypeDetails() {
                        Id = pc.Components.ComponentType.Id,
                        Abbreviation = pc.Components.ComponentType.Abbreviation,
                        Name = pc.Components.ComponentType.Name
                    }
                }
            })
        }).FirstOrDefaultAsync();

        if (pcs == null) {
            throw new NotFoundException();
        }
    
        return pcs;
    }

    public async Task<PostResponse> Post(PostRequest postRequest) {
        var pc = new PCs() {
            Name = postRequest.Name,
            Weight = postRequest.Weight,
            Warranty = postRequest.Warranty,
            CreatedAt = postRequest.CreatedAt,
            Stock = postRequest.Stock
        };
        await _context.PCs.AddAsync(pc);
        await _context.SaveChangesAsync();
        var response = await _context.PCs.Select(e => new PostResponse() {
            Id = e.Id,
            Name = e.Name,
            Weight = e.Weight,
            Warranty = e.Warranty,
            CreatedAt = e.CreatedAt,
            Stock = e.Stock
        }).FirstOrDefaultAsync();
        return response;
    }

    public async Task Put(int id, PostRequest postRequest) {
        var pc = await _context.PCs.FirstOrDefaultAsync(e => e.Id == id);
        if (pc == null) {
            throw new NotFoundException();
        }

        pc.Name = postRequest.Name;
        pc.Weight = postRequest.Weight;
        pc.Warranty = postRequest.Warranty;
        pc.CreatedAt = postRequest.CreatedAt;
        pc.Stock = postRequest.Stock;
        
        await _context.SaveChangesAsync();
    }

    public async Task deletePcs(int id) {
        var pcs = await _context.PCs.FindAsync(id);
        if (pcs == null) {
            throw new NotFoundException();
        }

        _context.PCs.Remove(pcs);
        await _context.SaveChangesAsync();
    }
}