using Cwiczenia_10.DTOs;

namespace Cwiczenia_10.Services;

public interface IDbService {
    Task<IEnumerable<GetPcs>> getAllPcs();
    Task<GetPc> GetPc(int id);
    Task<PostResponse> Post(PostRequest postRequest);
    Task Put(int id, PostRequest postRequest);
    Task deletePcs(int id);
}