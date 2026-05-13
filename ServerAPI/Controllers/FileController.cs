using Microsoft.AspNetCore.Mvc;
using Repository;
using Interface;

namespace ServerAPI.Controllers;

[ApiController]
[Route("api/files")]
public class FileController : ControllerBase
{

    private IFileRepository mFileRep;

    public FileController(IFileRepository fileRep)
    {
        mFileRep = fileRep;
    }

    // provide fileupload - the file is added to the repo and given
    // a unique filename with the same extension as the uploaded file. 
    [HttpPost]
    [Route("add")]
    public IActionResult Add(IFormFile? file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file uploaded.");

        var url = mFileRep.Add(file);
        return Ok(url);
    }
}