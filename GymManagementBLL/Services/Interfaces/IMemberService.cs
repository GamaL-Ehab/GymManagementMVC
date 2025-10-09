﻿using GymManagementBLL.ViewModels.MemberViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberService
    {
        bool CreateMember(CreateMemberViewModel model);
        bool UpdateMemberDetails(int memberId, MemberToUpdateViewModel model);
        bool RemoveMember(int memberId);
        IEnumerable<MemberViewModel> GetAllMembers();
        MemberViewModel? GetMemberDetails(int memberId);
        HealthRecordViewModel? GetMemberHealthRecord(int memberId);
        MemberToUpdateViewModel? GetMemberToUpdate(int memberId); 
    }
}
