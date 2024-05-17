using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restorann.Business.Exceptions;
using Restorann.Business.Services.Abstracts;
using Restorann.Core.Models;

namespace Restorann.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class BlogController : Controller
    {
        private readonly IChefService _chefService;

        public BlogController(IChefService chefService)
        {
            _chefService = chefService;
        }

        public IActionResult Index()
        {
            var chefs=  _chefService.GetAllChefs();
            return View(chefs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Chef chef)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                _chefService.AddAsyncChef(chef);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(ImageFileContentException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Update(int id)
        {
            var chef = _chefService.GetChef(x=> x.Id == id);
            if (chef == null)
                return NotFound();

            return View(chef);
        }

        [HttpPost]
        public IActionResult Update(Chef chef)
        {
            if(!ModelState.IsValid)
                return View();

            try
            {
                _chefService.UpdateChef(chef.Id, chef);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch(FileeNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();      
            }
            catch (ImageFileContentException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (ImageSizeException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var chef = _chefService.GetChef(x => x.Id == id);
            if(chef == null)
                return NotFound();  

            return View(chef);
        }


        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            if(!ModelState.IsValid)
                return View();

            try
            {
                _chefService.DeleteChef(id);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound();
            }
            catch (FileeNotFoundException ex)
            {
                ModelState.AddModelError("ImageFile", ex.Message);
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return RedirectToAction("Index");
        }

    }
}
