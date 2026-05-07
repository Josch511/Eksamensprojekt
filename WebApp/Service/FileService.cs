using Microsoft.AspNetCore.Components.Forms;

namespace WebApp.Services;

public class FileService
{
    private const long MaxFileSize = 5 * 1024 * 1024;

    public IBrowserFile? SelectedFile { get; private set; }

    public string? PreviewUrl { get; private set; }

    public async Task HandleImageSelectedAsync(InputFileChangeEventArgs e)
    {
        var file = e.File;

        if (file is null)
        {
            Clear();
            return;
        }

        SelectedFile = file;

        using var stream = file.OpenReadStream(MaxFileSize);
        using var memoryStream = new MemoryStream();

        await stream.CopyToAsync(memoryStream);

        var base64 = Convert.ToBase64String(memoryStream.ToArray());

        PreviewUrl = $"data:{file.ContentType};base64,{base64}";
    }

    public void Clear()
    {
        SelectedFile = null;
        PreviewUrl = null;
    }
}