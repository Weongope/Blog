using Microsoft.AspNetCore.Mvc;
using Blog.Data.Repository;
using Blog.Data.FileManager;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;
        private IFileManager _fileManager;

        public HomeController(
            IRepository repo,
            IFileManager fileManager
            )
        {
            _repo = repo;
            _fileManager = fileManager;
        }

        public IActionResult Index(string category) =>
            View(string.IsNullOrEmpty(category) ? 
                _repo.GetAllPosts() : 
                _repo.GetAllPosts(category));

      // public IActionResult Index(string category)
      // {
      //     var posts = string.IsNullOrEmpty(category) ? _repo.GetAllPosts() : _repo.GetAllPosts(category);
      //     // boolean ? true : false; 
      //     return View(posts);
      // }


        public IActionResult Post(int id) =>
            View(_repo.GetPost(id));
       // {
       //     var post = _repo.GetPost(id);
       //
       //     return View(_repo.GetPost(id));
       // }

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image)
        {
            var mime = image.Substring(image.LastIndexOf('.') + 1 );
            return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");    
        }

        //[HttpGet("/Image/{image}")]
        //public IActionResult Image(string image)
        //{
        //    var mime = image.Substring(image.LastIndexOf('.') + 1);
        //    return new FileStreamResult(_fileManager.ImageStream(image), $"image/{mime}");
        //}


    }
}
