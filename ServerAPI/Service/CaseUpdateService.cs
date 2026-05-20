using Core;
using ServerAPI.Interface;

namespace ServerAPI.Service;

public class CaseUpdateService : ICaseUpdateService
{
    private readonly ICaseRepository _caseRepository;

    public CaseUpdateService(ICaseRepository caseRepository)
    {
        _caseRepository = caseRepository;
    }

    public async Task Build(int caseId, string message, bool isComment = false, string? commentMessage = null)
    {
        if (isComment && string.IsNullOrWhiteSpace(commentMessage))
            throw new ArgumentException("A comment must have a message.");
        
        await _caseRepository.AddCaseUpdate(caseId, new CaseUpdate
        {
            message        = message,
            createdAt      = DateTime.Now,
            isComment      = isComment,
            commentMessage = commentMessage
        });
    }
}