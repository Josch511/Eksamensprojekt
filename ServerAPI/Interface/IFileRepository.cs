namespace Interface;

public interface IFileRepository
{
    // represent a repo of files. When a file is added, it is given a unique name
    // to retrieve the content of a file in the repo, you must provide its name.


    // will add [file] to the repo - return the unique name it is given
    string Add(IFormFile file);
}