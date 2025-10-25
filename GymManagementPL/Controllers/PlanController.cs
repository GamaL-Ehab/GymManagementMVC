using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }
        public IActionResult Details(int id)
        {

            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id!";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanById(id);

            if(plan is null)
            {
                TempData["ErrorMessage"] = "Plan Not Found!";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        public IActionResult Edit(int id) 
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id!";
                return RedirectToAction(nameof(Index));
            }

            var plan = _planService.GetPlanToUpdate(id);

            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan can not be updated!";
                return RedirectToAction(nameof(Index));
            }

            return View(plan);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdatePlanViewModel input) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Data Validation");
                return View(input);
            }

            var result = _planService.UpdatePlan(id, input);

            if (result)
                TempData["SuccessMessage"] = "Plan updated Successfully!";
            else
                TempData["ErrorMessage"] = "Failed to update plan!";


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Activate(int id)
        {
            var result = _planService.Activate(id);

            if (result)
                TempData["SuccessMessage"] = "Plan status changed successfully!";
            else
                TempData["ErrorMessage"] = "Failed to change plan status!";

            return RedirectToAction(nameof(Index));
        }
    }
}
