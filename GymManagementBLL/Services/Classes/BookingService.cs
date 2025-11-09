using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Classes
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateBooking(CreateBookingViewModel createdBooking)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(createdBooking.SessionId);
                if (session is null || session.StartDate <= DateTime.Now) return false;

                var HasActiveMembership = _unitOfWork.MembershipRepository.GetAll(X => X.MemberId == createdBooking.MemberId && X.Status == "Active").Any();
                if (!HasActiveMembership) return false;

                var HasAvailableSolts = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(createdBooking.SessionId);
                if (HasAvailableSolts == 0) return false;
                _unitOfWork.BookingRepository.Add(new Booking()
                {
                    MemberId = createdBooking.MemberId,
                    SessionId = createdBooking.SessionId,
                    IsAttended = false,
                    UpdatedAt = DateTime.Now
                });

                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithTrainerAndCategory(x => x.EndDAte >= DateTime.Now)
                                                        .OrderByDescending(x => x.StartDate);

            if (!sessions.Any())
                return [];

            var mappedSessions = _mapper.Map<IEnumerable<SessionViewModel>>(sessions);
            foreach(var session in mappedSessions)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id);
            }

            return mappedSessions;
        }

        public IEnumerable<MemberSelectListViewModel> GetMembersForDropdown()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll(x => x.MemberSessions == null);

            if (members is null || !members.Any())
                return [];

            var mappedMembers = _mapper.Map<IEnumerable<MemberSelectListViewModel>>(members);

            return mappedMembers;
        }

        public IEnumerable<MemberForSessionViewModel>? GetSessionMembers(int sessionId)
        {
            var MemberForSession = _unitOfWork.BookingRepository.GetBySessionId(sessionId);
            return MemberForSession.Select(X => new MemberForSessionViewModel
            {
                MemberId = X.MemberId,
                SessionId = sessionId,
                MemberName = X.Member.Name,
                BookingDate = X.CreatedAt.ToString(),
                IsAttended = X.IsAttended
            });
        }

        public bool MemberAttended(int MemberId, int SessionId)
        {
            try
            {
                var memberSession = _unitOfWork.GetRepository<Booking>()
                                           .GetAll(X => X.MemberId == MemberId && X.SessionId == SessionId)
                                           .FirstOrDefault();
                if (memberSession is null) return false;

                memberSession.IsAttended = true;
                memberSession.UpdatedAt = DateTime.Now;
                _unitOfWork.GetRepository<Booking>().Update(memberSession);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }

        public bool CancelBooking(int MemberId, int SessionId)
        {
            try
            {
                var session = _unitOfWork.SessionRepository.GetById(SessionId);
                if (session is null || session.StartDate <= DateTime.Now) return false;

                var Booking = _unitOfWork.BookingRepository.GetAll(X => X.SessionId == SessionId && X.MemberId == MemberId)
                                                           .FirstOrDefault();
                if (Booking is null) return false;
                _unitOfWork.BookingRepository.Delete(Booking);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
