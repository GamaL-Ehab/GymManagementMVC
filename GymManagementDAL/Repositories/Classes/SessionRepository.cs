using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _context;

        public SessionRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Session> GetAllSessionsWithTrainerAndCategory(Func<Session, bool>? condition = null)
        {
            if (condition is null) 
            {
                return _context.Sessions
                    .Include(s => s.Trainer)
                    .Include(s => s.Category)
                    .ToList();
            }
            else
            {
                return _context.Sessions
                    .Include(s => s.Trainer)
                    .Include(s => s.Category)
                    .Where(condition)
                    .ToList();
            }
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _context.Bookings.Where(x => x.SessionId == sessionId).Count();
        }

        public Session GetSessionWithTrainerAndCategory(int sessionId)
        {
            return _context.Sessions
                    .Include(s => s.Trainer)
                    .Include(s => s.Category)
                    .FirstOrDefault(x => x.Id == sessionId);
        }
    }
}
