namespace Core;

public class CaseUpdateRequest
{
    public required string message { get; set; }
    public bool isComment { get; set; } = false;
    public string? commentMessage { get; set; }
}