using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public IActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        public IActionResult MemberDetails(int id) 
        {
            var member = _memberService.GetMemberDetails(id);

            if(member is null)
                return RedirectToAction(nameof(Index));

            return View(member);
        }
        public IActionResult HealthRecordDetails(int id) 
        {
            var member = _memberService.GetMemberHealthRecord(id);

            if(member is null)
                return RedirectToAction(nameof(Index));

            return View(member);
        }
        public IActionResult Create()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel input) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataMissed", "Check Missing Data");
                return View(nameof(Create), input);
            }

            bool result = _memberService.CreateMember(input);
            if (result)
                TempData["SuccessMessage"] = "Member Created Successflly!";
            else
                TempData["ErrorMessage"] = "Member Failed To Create, Phone Number or Email Already Exist!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult MemberEdit(int id) 
        {
            var member = _memberService.GetMemberToUpdate(id);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public IActionResult MemberEdit([FromRoute] int id, MemberToUpdateViewModel input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            bool result = _memberService.UpdateMemberDetails(id, input);
            if (result)
                TempData["SuccessMessage"] = "Member Updated Successflly!";
            else
                TempData["ErrorMessage"] = "Member Failed To Update, Phone Number or Email Already Exist!";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete([FromRoute] int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id!";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberDetails(id);

            if (member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MemberId = id;

            return View();
        }
        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {
            var result = _memberService.RemoveMember(id);
            if (result)
                TempData["SuccessMessage"] = "Member Deleted Successflly!";
            else
                TempData["ErrorMessage"] = "Member Can Not Be Deleted!";

            return RedirectToAction(nameof(Index));
        }
    }
}
