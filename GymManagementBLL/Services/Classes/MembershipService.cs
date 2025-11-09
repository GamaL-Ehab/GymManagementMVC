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
    public class MembershipService : IMembershipService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MembershipService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CancelMembership(int id)
        {
             var membershipToDelete = _unitOfWork.MembershipRepository.GetAll(x => x.MemberId == id && x.Status == "Active").FirstOrDefault();
            if (membershipToDelete is null)
                return false;

            _unitOfWork.MembershipRepository.Delete(membershipToDelete);
            return _unitOfWork.SaveChanges() > 0;
        }

        public bool CreateMembership(CreateMemberShipViewModel input)
        {
            if (!IsPlanExist(input.PlanId) || !IsMemberExist(input.MemberId) || HasActiveMembership(input.MemberId))
                return false;

            var membership = _mapper.Map<Membership>(input);
            membership.UpdatedAt = DateTime.Now;
            membership.EndDate = DateTime.Now.AddDays(_unitOfWork.GetRepository<Plan>().GetById(membership.PlanId).DurationDays);
            _unitOfWork.GetRepository<Membership>().Add(membership);

            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<MemberShipViewModel>? GetAllMemberships()
        {
            var memberships = _unitOfWork.MembershipRepository.GetAllMembershipsWithMemberAndPlan(x => x.Status == "Active");

            if (memberships is null || !memberships.Any())
                return [];
            
            return _mapper.Map<IEnumerable<MemberShipViewModel>> (memberships);
        }

        public IEnumerable<MemberSelectListViewModel>? GetMembersForDropdown()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll(x => x.MemberPlans == null);
            if (members is null)
                return [];

            return _mapper.Map<IEnumerable<MemberSelectListViewModel>>(members);
        }

        public IEnumerable<PlanSelectListViewModel>? GetPlansForDropdown()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll(x => x.IsActive == true);
            if (plans is null)
                return [];

            return _mapper.Map<IEnumerable<PlanSelectListViewModel>>(plans);
        }

        #region Helper Methods
        private bool IsPlanExist(int planId)
        {
            return _unitOfWork.GetRepository<Plan>().GetById(planId) != null;
        }
        private bool IsMemberExist(int memberId)
        {
            return _unitOfWork.GetRepository<Member>().GetById(memberId) != null;
        }
        private bool HasActiveMembership(int memberId)
        {
            return _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == memberId && x.Status == "Active").FirstOrDefault() != null;
        }
        #endregion
    }
}
