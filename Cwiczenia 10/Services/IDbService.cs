using Cwiczenia_10.DTOs;

namespace Cwiczenia_10.Services;

public interface IDbService {
    Task<IEnumerable<GetPcs>> getAllPcs();
}