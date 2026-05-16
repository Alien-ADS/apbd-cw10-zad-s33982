using Cwiczenia_10.Data;
using Cwiczenia_10.DTOs;
using Cwiczenia_10.Entities;
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
}