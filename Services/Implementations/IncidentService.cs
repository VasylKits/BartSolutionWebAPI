using BartsolutionsWebAPI.DB;
using BartsolutionsWebAPI.DTOs;
using BartsolutionsWebAPI.Models;
using BartsolutionsWebAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BartsolutionsWebAPI.Services.Implementations
{
    public class IncidentService : IIncidentService
    {
        private readonly DatabaseContext _myDbContext;
        public IncidentService(DatabaseContext myDbContext)
        {
            _myDbContext = myDbContext;
        }

        public async Task<IBaseResponse<List<AccountResponseGet>>> GetAllAccountsAsync()
        {
            var accountList = await _myDbContext.Accounts.ToListAsync();

            if (accountList.Count == 0)
                return new BaseResponse<List<AccountResponseGet>>
                {
                    IsError = true,
                    ErrorMessage = "Error! Database is empty"
                };

            var responseAccountsList = new List<AccountResponseGet>();

            foreach (var accounts in accountList)
            {
                responseAccountsList.Add(new AccountResponseGet
                {
                    Name = accounts.Name
                });
            }

            return new BaseResponse<List<AccountResponseGet>>()
            {
                Response = responseAccountsList
            };
        }

        public async Task<IBaseResponse<List<ResponseContact>>> GetAllContactsAsync()
        {
            var contactList = await _myDbContext.Contacts.ToListAsync();

            if (contactList.Count == 0)
                return new BaseResponse<List<ResponseContact>>
                {
                    IsError = true,
                    ErrorMessage = "Error! Database is empty"
                };

            var responseContactsList = new List<ResponseContact>();

            foreach (var contact in contactList)
            {
                responseContactsList.Add(new ResponseContact
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    AccountName = contact.AccountName
                });
            }

            return new BaseResponse<List<ResponseContact>>()
            {
                Response = responseContactsList
            };
        }

        public async Task<IBaseResponse<List<ResponseIncident>>> GetAllIncidentsAsync()
        {
            var incidentList = await _myDbContext.Incidents.ToListAsync();

            if (incidentList.Count == 0)
                return new BaseResponse<List<ResponseIncident>>
                {
                    IsError = true,
                    ErrorMessage = "Error! Database is empty"
                };

            var responseIncidentList = new List<ResponseIncident>();

            foreach (var incident in incidentList)
            {
                responseIncidentList.Add(new ResponseIncident
                {
                    IncidentName = incident.Name,
                    IncidentDescription = incident.Description
                });
            }

            return new BaseResponse<List<ResponseIncident>>()
            {
                Response = responseIncidentList
            };
        }

        public async Task<IBaseResponse<ResponseContact>> CreateContactAsync(NewContact request)
        {
            try
            {
                var contact = new Contact()
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };

                await _myDbContext.Contacts.AddAsync(contact);
                await _myDbContext.SaveChangesAsync();

                var responce = new ResponseContact()
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email
                };

                return new BaseResponse<ResponseContact>
                {
                    Response = responce
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<ResponseContact>()
                {
                    IsError = true,
                    ErrorMessage = $"[CreateContactAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<ResponseAccount>> CreateAccountAsync(NewAccount request)
        {
            try
            {
                var contact = await _myDbContext.Contacts.FindAsync(request.ContactId);
                if (contact == null)
                    return new BaseResponse<ResponseAccount>()
                    {
                        IsError = true,
                        ErrorMessage = $"Contact is not in the system!"
                    };

                var account = new Account { Name = request.Name };

                _myDbContext.Accounts.Add(account);

                contact.AccountName = account.Name;
                await _myDbContext.SaveChangesAsync();

                var responce = new ResponseAccount
                {
                    Name = request.Name,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email
                };
                return new BaseResponse<ResponseAccount>
                {
                    Response = responce
                };
            }

            catch (Exception ex)
            {
                return new BaseResponse<ResponseAccount>()
                {
                    IsError = true,
                    ErrorMessage = $"[AddAccountAsync] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<ResponseIncident>> CreateIncidentAsync(IncidentRequest request)
        {
            try
            {
                var accountExist = await _myDbContext.Accounts.FirstOrDefaultAsync(a => a.Name == request.AccountName);

                if (accountExist is null)
                    return new BaseResponse<ResponseIncident>()
                    {
                        IsError = true,
                        ErrorMessage = $"Account is not in the system! Enter an existing account name!"
                    };

                var mailExist = await _myDbContext.Contacts.FirstOrDefaultAsync(c => c.Email == request.Email);

                if (mailExist is null)
                {
                    var contact = new Contact()
                    {
                        Email = request.Email,
                        FirstName = request.FirstName,
                        LastName = request.LastName,
                        AccountName = request.AccountName
                    };

                    await _myDbContext.Contacts.AddAsync(contact);
                }
                else
                {
                    if (mailExist.AccountName is null)
                    {
                        mailExist.FirstName = request.FirstName;
                        mailExist.LastName = request.LastName;
                        mailExist.AccountName = request.AccountName;
                    }
                    else
                    {
                        mailExist.FirstName = request.FirstName;
                        mailExist.LastName = request.LastName;
                    }
                }

                var incident = new Incident() { Name = Guid.NewGuid().ToString(), Description = request.Description };

                await _myDbContext.Incidents.AddAsync(incident);

                accountExist.IncidentName = incident.Name;

                await _myDbContext.SaveChangesAsync();

                var responce = new ResponseIncident
                {
                    AccountName = request.AccountName,
                    IncidentName = incident.Name,
                    IncidentDescription = request.Description
                };

                return new BaseResponse<ResponseIncident>
                {
                    Response = responce
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ResponseIncident>()
                {
                    IsError = true,
                    ErrorMessage = $"[CreateIncidentAsync] : {ex.Message}"
                };
            }
        }
    }
}