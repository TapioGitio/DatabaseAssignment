using Data.Entities;

namespace Data.Interfaces;

public interface IProjectRepository : IBaseRepository<ProjectEntity>
{
    Task <IEnumerable<ProjectEntity>> ReadDetailedAsync(ProjectEntity entity);

}
