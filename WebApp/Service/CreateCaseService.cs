using Blazored.LocalStorage;
using Core;
using System.Net.Http.Json;


namespace WebApp.Services;

public class CreateCaseService
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


    public async Task Submit()
    {
        var newCase = new Cases
        {
            title = Data.CaseInfo.Title,
            description = Data.CaseInfo.Description,
            media = new List<string> { Data.CaseInfo.AttachmentUrl },
            status = "Open",
            created_at = DateOnly.FromDateTime(DateTime.Now),
            updated_at = DateOnly.FromDateTime(DateTime.Now),
            order_item_id = Data.OrderId,
            user_id = Data.UserId,
            type = Data.CaseType,
            department_name = Data.CaseDepartment
        };

        var response = await http.PostAsJsonAsync("cases", newCase);
        response.EnsureSuccessStatusCode();

        await Clear();
    }
    public class CaseDraft
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public string? CaseType { get; set; }
        public string? CaseDepartment { get; set; }
        public string? Serial { get; set; }
        public string? Name { get; set; }
        public CaseInfo CaseInfo { get; set; } = new();
        public CaseContact CaseContact { get; set; } = new();
    }

    public class CaseInfo
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? AttachmentUrl { get; set; }
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
        return !string.IsNullOrWhiteSpace(Data.CaseType)
               && !string.IsNullOrWhiteSpace(Data.CaseDepartment);
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

    
}
