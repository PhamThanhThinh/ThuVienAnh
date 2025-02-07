using Microsoft.AspNetCore.Mvc;

namespace ThuVienAnh.Controllers
{
  public class ThuVienAnhController : Controller
  {
    private readonly string rootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//img");

    public IActionResult Index()
    {
      bool directoryExists = Directory.Exists(rootPath);

      if (!directoryExists)
      {
        Directory.CreateDirectory(rootPath);
      }

      List<string> image = Directory.GetFiles(rootPath).Select(Path.GetFileName).ToList();
      return View(image);
    }

    [HttpPost]
    public async Task<IActionResult> Index(IFormFile file)
    {
      if (file != null)
      {
        // lấy tên file
        //var path = Path.Combine(rootPath, file.Name);
        var path = Path.Combine(rootPath, Guid.NewGuid() 
          + Path.GetExtension(file.FileName));

        // Lưu file vào thư mục
        // file directory
        using (var fileDirectory = new FileStream(path, FileMode.Create))
        {
          await file.CopyToAsync(fileDirectory);
        }

        return RedirectToAction("Index");
        //return Redirect(nameof(Index));

      }
      //else
      //{

      //}
      return View();
    }

  }
}
