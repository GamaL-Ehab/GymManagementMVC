using GymManagementBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        IEnumerable<MemberForSessionViewModel>? GetSessionMembers(int sessionId);
        IEnumerable<MemberSelectListViewModel> GetMembersForDropdown();
        bool CreateBooking(CreateBookingViewModel createdBooking);
        bool MemberAttended(int MemberId, int SessionId);
        bool CancelBooking(int MemberId, int SessionId);
    }
}
