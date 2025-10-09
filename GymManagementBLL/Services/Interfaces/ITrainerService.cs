using GymManagementBLL.ViewModels.TrainerViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        bool CreateTrainer(CreateTrainerViewModel trainer);
        bool RemoveTrainer(int trainerId)
        bool UpdateTrainerDetails(int trainerId, UpdateTrainerViewModel model);
        IEnumerable<TrainerViewModel> GetAllTrainers();
        TrainerViewModel? GetTrainerDetails(int trainerId);
        UpdateTrainerViewModel? GetTrainerToUpdate(int trainerId); 
    }
}
