using Interface;
using Microsoft.AspNetCore.Hosting;

public class FileRepository : IFileRepository
{
    private readonly IWebHostEnvironment _env;
    private readonly string _path;
    
    public FileRepository(IWebHostEnvironment env)
    {
        _env = env;
        _path = Path.Combine(_env.WebRootPath, "uploads");
    }

    public string Add(IFormFile file)
    {
        // ensure the folder is there
        if (!Directory.Exists(_path))
            Directory.CreateDirectory(_path);

        // compute a unique new filename
        var fileName = UniqueFilename() + Path.GetExtension(file.FileName);
        var path = Path.Combine(_path, fileName);

        using var stream = new FileStream(path, FileMode.Create);
        file.CopyTo(stream);

        return fileName;
    }

    private string UniqueFilename()
    {
        return Guid.NewGuid().ToString();
    }
}