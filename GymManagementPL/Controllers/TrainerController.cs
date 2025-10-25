using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        public IActionResult Index()
        {
            var trainers = _trainerService.GetAllTrainers();
            return View(trainers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateTrainer(CreateTrainerViewModel input)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data");
                return View(nameof(Create), input);
            }

            bool result = _trainerService.CreateTrainer(input);
            if (result)
                TempData["SuccessMessage"] = "Trainer Created Successflly!";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Create, Phone Number or Email Already Exist!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult TrainerDetails(int id)
        {
            var trainer = _trainerService.GetTrainerDetails(id);

            if (trainer is null)
                return RedirectToAction(nameof(Index));

            return View(trainer);
        }

        public IActionResult TrainerEdit(int id)
        {
            var trainer = _trainerService.GetTrainerToUpdate(id);

            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(trainer);
        }

        [HttpPost]
        public IActionResult TrainerEdit([FromRoute] int id, UpdateTrainerViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            bool result = _trainerService.UpdateTrainerDetails(id, input);
            if (result)
                TempData["SuccessMessage"] = "Trainer Updated Successflly!";
            else
                TempData["ErrorMessage"] = "Trainer Failed To Update, Phone Number or Email Already Exist!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id!";
                return RedirectToAction(nameof(Index));
            }

            var trainer = _trainerService.GetTrainerDetails(id);

            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.TrainerId = id;

            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var result = _trainerService.RemoveTrainer(id);
            if (result)
                TempData["SuccessMessage"] = "Trainer Deleted Successflly!";
            else
                TempData["ErrorMessage"] = "Trainer Can Not Be Deleted!";

            return RedirectToAction(nameof(Index));
        }
    }
}
