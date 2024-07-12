using BlogWebSite.Models;
using static System.Net.Mime.MediaTypeNames;

namespace BlogWebSite.Helpers;

public static class FileUploadHelper
{
    static public async Task<string> UploadAsync(IFormFile file)
    {
        if (file != null)
        {
            string filename = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            using var fs = new FileStream(@$"wwwroot/uploads/{filename}", FileMode.Create);
            await file.CopyToAsync(fs);
            return @$"/uploads/{filename}";
        }
        throw new Exception("File was not uploaded!");
    }   
}
