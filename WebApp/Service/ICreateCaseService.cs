namespace WebApp.Service;

public interface ICreateCaseService
{
    CreateCaseService.CaseDraft Data { get; set; }

    Task Load();
    Task Save();
    Task Clear();
    Task<(bool success, int caseId, string? error)> Submit();

    bool CaseOrderIdIsValid();
    bool CaseTypeIsValid();
    bool CaseInfoIsValid();
    bool CaseContactIsValid();
    bool CaseIsValid();

    string CaseTypeName { get; }
}