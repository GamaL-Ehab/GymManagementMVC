using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface IPlanRepository
    {
<<<<<<< Updated upstream:GymManagementDAL/Repositories/Interfaces/IPlanRepository.cs
        IEnumerable<Plan> GetAll();
        Plan? GetById(int id);
        int Add(Plan plan);
        int Update(Plan plan);
        int Delete(int id);
=======
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
        ISessionRepository SessionRepository { get; set; }
        int SaveChanges();
>>>>>>> Stashed changes:GymManagementDAL/Repositories/Interfaces/IUnitOfWork.cs
    }
}
