using BartsolutionsWebAPI.DTOs;
using BartsolutionsWebAPI.Models;

namespace BartsolutionsWebAPI.Services.Interfaces
{
    public interface IIncidentService
    {
        Task<IBaseResponse<ResponseContact>> CreateContactAsync(NewContact request);
        Task<IBaseResponse<ResponseAccount>> CreateAccountAsync(NewAccount request);
        Task<IBaseResponse<ResponseIncident>> CreateIncidentAsync(IncidentRequest request);
        Task<IBaseResponse<List<ResponseContact>>> GetAllContactsAsync();
        Task<IBaseResponse<List<AccountResponseGet>>> GetAllAccountsAsync();      
        Task<IBaseResponse<List<ResponseIncident>>> GetAllIncidentsAsync();
        
    }
}
