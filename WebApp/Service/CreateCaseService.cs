using Blazored.LocalStorage;
using Core;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;


namespace WebApp.Service;

public class CreateCaseService : ICreateCaseService
{
    private const string Key = "createcase";

    private readonly ILocalStorageService localStorage;
    private readonly HttpClient http;

    public CaseDraft Data { get; set; } = new();

    public CreateCaseService(ILocalStorageService localStorage, HttpClient http)
    {
        this.localStorage = localStorage;
        this.http = http;
    }

    public async Task Load()
    {
        Data = await localStorage.GetItemAsync<CaseDraft>(Key) ?? new CaseDraft();

        if (Data.UserId is null)
        {
            Data.UserId = await localStorage.GetItemAsync<int>("userId");
        }
    }

    public async Task Save()
    {
        await localStorage.SetItemAsync(Key, Data);
    }

    public async Task Clear()
    {
        Data = new CaseDraft();
        await localStorage.RemoveItemAsync(Key);
    }


    public async Task<(bool success, int caseId, string? error)> Submit()
    {
        var newCase = new Cases
        {
            title = Data.CaseInfo.Title ?? string.Empty,
            description = Data.CaseInfo.Description ?? string.Empty,
            media = Data.CaseInfo.AttachmentUrl ?? new List<string>(),
            status = "Case received",
            createdAt = DateOnly.FromDateTime(DateTime.Now),
            orderItemId = Data.OrderId,
            userId = Data.UserId,
            typeId = Data.CaseTypeId,
            departmentId = Data.CaseDepartmentId,
            caseUpdates = new List<CaseUpdate>(),
            caseContact = new Core.CaseContact
            {
            Name = Data.CaseContact.Name,
            Email = Data.CaseContact.Email,
            Telephone = Data.CaseContact.Telephone
        }
        };

        var response = await http.PostAsJsonAsync("cases", newCase);
        
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(error, null, response.StatusCode);
        }

        var created = await response.Content.ReadFromJsonAsync<Cases>();
        await Clear();

        if (created is null)
        {
            return (false, 0, "Case response missing.");
        }

        return (true, created._id, null);
    }
    public class CaseDraft
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int CaseTypeId { get; set; }
        public int CaseDepartmentId { get; set; }
        public string? Serial { get; set; }
        public string? Name { get; set; }
        public CaseInfo CaseInfo { get; set; } = new();
        public CaseContact CaseContact { get; set; } = new();
        
    }

    public class CaseInfo
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<string>? AttachmentUrl { get; set; }
    }

    public class CaseContact
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
    }

    // Validation of data
    public bool CaseOrderIdIsValid()
    {
        return (Data.OrderId > 0);
    }

    public bool CaseTypeIsValid()
    {
        return Data.CaseTypeId > 0 && Data.CaseDepartmentId > 0;
    }

    public bool CaseInfoIsValid()
    {
        return !string.IsNullOrWhiteSpace(Data.CaseInfo.Title)
               && !string.IsNullOrWhiteSpace(Data.CaseInfo.Description);
    }
    public bool CaseContactIsValid()
    {
        return !string.IsNullOrWhiteSpace(Data.CaseContact.Name)
               && !string.IsNullOrWhiteSpace(Data.CaseContact.Telephone)
               && !string.IsNullOrWhiteSpace(Data.CaseContact.Email);
    }

    public bool CaseIsValid()
    {
        return CaseOrderIdIsValid()
               && CaseTypeIsValid()
               && CaseInfoIsValid()
               && CaseContactIsValid();
    }

    public string CaseTypeName => Data.CaseTypeId switch
    {
        1 => "Problem",
        2 => "Feature",
        _ => "Unknown"
    };
    
}
