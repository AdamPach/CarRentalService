using CarRentalService.Domain.Common.Interfaces;
using FluentResults;

namespace CarRentalService.UseCases.Common;

internal interface IUpdater<TUpdated, in TUpdater>
    where TUpdated : IEntity
    where TUpdater : class
{
    Result<TUpdated> Update(TUpdated updated, TUpdater updater);
}