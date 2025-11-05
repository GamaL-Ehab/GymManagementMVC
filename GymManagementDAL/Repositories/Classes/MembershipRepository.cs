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
    public class MembershipRepository : GenericRepository<Membership>, IMembershipRepository
    {
        private readonly GymDbContext _context;

        public MembershipRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Membership> GetAllMembershipsWithMemberAndPlan(Func<Membership, bool> predicate)
        {
            return _context.Memberships.Include(x => x.Member).Include(x => x.Plan).Where(predicate);
        }
    }
}
