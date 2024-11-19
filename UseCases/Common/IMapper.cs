using FluentResults;

namespace CarRentalService.UseCases.Common;

public interface IMapper<in TFrom, TTo>
{
    Result<TTo> Map(TFrom from);
}