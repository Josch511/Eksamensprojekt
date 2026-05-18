namespace Core;

public class CaseUpdate
{
    public required string message { get; set; }
    public required string timestamp { get; set; }
    public bool isComment { get; set; } = false;
    public string? commentMessage { get; set; }
}