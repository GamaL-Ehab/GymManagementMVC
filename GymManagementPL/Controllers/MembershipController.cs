using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class MembershipController : Controller
    {
        private readonly IMembershipService _membershipService;

        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }
        public IActionResult Index()
        {
            var memberships = _membershipService.GetAllMemberships();   
            return View(memberships);
        }

        public IActionResult Create()
        {
            LoadMembersDropdown();
            LoadPlansDropdown();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateMemberShipViewModel input)
        {
            if (ModelState.IsValid)
            {
                var result = _membershipService.CreateMembership(input);

                if (result)
                {
                    TempData["Success"] = "Membership created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["Error"] = "Failed to create membership. member have an active membership.";
                }
            }
            LoadMembersDropdown();
            LoadPlansDropdown();
            return View(input);
        }

        [HttpPost]
        public IActionResult Cancel(int id)
        {
            Console.WriteLine(id);
            var result = _membershipService.CancelMembership(id);
            if (result)
                TempData["Success"] = "Membership deleted successfully.";
            else
                TempData["Error"] = "Can not delete membership!.";

            return RedirectToAction(nameof(Index));
        }

        #region Helper Methods
        public void LoadMembersDropdown()
        {
            var members = _membershipService.GetMembersForDropdown();
            ViewBag.Members = new SelectList(members, "Id", "Name");
        }
        public void LoadPlansDropdown()
        {
            var plans = _membershipService.GetPlansForDropdown();
            ViewBag.Plans = new SelectList(plans, "Id", "Name");
        }
        #endregion
    }
}
