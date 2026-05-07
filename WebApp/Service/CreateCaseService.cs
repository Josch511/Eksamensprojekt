using Blazored.LocalStorage;
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
        if (CaseIsValid())
        {
            var response = await http.PostAsJsonAsync("/api/cases", Data);
            response.EnsureSuccessStatusCode();
            await Clear();   
        }
    }
    
    public class CaseDraft
    {
        public int OrderId { get; set; }
        public int CaseTypeId { get; set; }
        public int CaseCategoryId { get; set; }

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
        return (Data.CaseTypeId > 0)
               && (Data.CaseCategoryId > 0);
    }
    
    public bool CaseInfoIsValid()
    {
        return !string.IsNullOrWhiteSpace(Data.CaseInfo.Title)
               && !string.IsNullOrWhiteSpace(Data.CaseInfo.Description)
               && !string.IsNullOrWhiteSpace(Data.CaseInfo.AttachmentUrl);
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