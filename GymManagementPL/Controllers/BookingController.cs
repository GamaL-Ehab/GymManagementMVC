using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        public IActionResult Index()
        {
            var bookings = _bookingService.GetAllSessions();

            return View(bookings);
        }

        public IActionResult GetMembersForOngoingSessions(int id) 
        {
            var sessionMembers = _bookingService.GetSessionMembers(id);
            if (sessionMembers is null || !sessionMembers.Any())
                return View();

            return View(sessionMembers);
        }
        public IActionResult GetMembersForUpcomingSession(int id) 
        {
            var sessionMembers = _bookingService.GetSessionMembers(id);
            if (sessionMembers is null || !sessionMembers.Any())
                return View();

            return View(sessionMembers);
        }

        public IActionResult Create()
        {
            LoadMembersDropdown();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateBookingViewModel createdBooking)
        {
            var result = _bookingService.CreateBooking(createdBooking);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking Created successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to Create Booking.";
            }

            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = createdBooking.SessionId });
        }

        [HttpPost]
        public IActionResult Attended(int MemberId, int SessionId)
        {
            var result = _bookingService.MemberAttended(MemberId, SessionId);
            if (result)
            {
                TempData["SuccessMessage"] = "Member Status Updated successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Status Updated successfully!";
            }

            return RedirectToAction(nameof(GetMembersForOngoingSessions), new {id = SessionId});
        }

        [HttpPost]
        public IActionResult Cancel(int MemberId, int SessionId)
        {
            var result = _bookingService.CancelBooking(MemberId, SessionId);
            if (result)
            {
                TempData["SuccessMessage"] = "Booking canceled successfully!";
            }
            else
            {
                TempData["ErrorMessage"] = "Can not cancel booking!";
            }

            return RedirectToAction(nameof(GetMembersForUpcomingSession), new { id = SessionId });
        }

        public void LoadMembersDropdown()
        {
            var members = _bookingService.GetMembersForDropdown();
            ViewBag.Members = new SelectList(members, "Id", "Name");
        }
    }
}
