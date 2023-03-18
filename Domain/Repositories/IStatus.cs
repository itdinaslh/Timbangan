using Timbangan.Domain.Entities;

namespace Timbangan.Domain.Repositories;

public interface IStatus
{
    IQueryable<Status> Statuses { get; }

    Task SaveDataAsync(Status status);
}
