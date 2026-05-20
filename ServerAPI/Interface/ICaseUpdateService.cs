using Core;

namespace ServerAPI.Interface;

public interface ICaseUpdateService
{
    Task Build(int caseId, string message, bool isComment = false, string? commentMessage = null);
}