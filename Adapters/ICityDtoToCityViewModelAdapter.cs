using System.Collections.Generic;

namespace Test.Adapters
{
    public interface ICityDtoToCityViewModelAdapter
    {
        IEnumerable<CityViewModel> ParallelLinqMap(IEnumerable<CityDto> cities, int degreeOfParallelism = 2);

        IEnumerable<CityViewModel> ParallelForEachManualMap(IEnumerable<CityDto> cities);

        IEnumerable<TOut> ExpressionMap<TIn, TOut>(IEnumerable<TIn> sourceCollection) where TOut : new();

        IEnumerable<TOut> ILMap<TIn, TOut>(IEnumerable<TIn> sourceCollection) where TOut : new();

        IEnumerable<TOut> ReflectionOrderedPropertiesCopy<TIn, TOut>(IEnumerable<TIn> sourceCollection) where TOut : new();

        IEnumerable<TOut> ReflectionPropertySearchCopy<TIn, TOut>(IEnumerable<TIn> sourceCollection) where TOut : new();

        IEnumerable<CityViewModel> ManualForArrayMap(CityDto[] cities);

        IEnumerable<CityViewModel> ManualMap(IEnumerable<CityDto> cities);

        IEnumerable<CityViewModel> LinqMap(IEnumerable<CityDto> cities);

        IEnumerable<CityViewModel> AutoMapperMap(IEnumerable<CityDto> cities);

        IEnumerable<CityViewModel> AutoMapperCollectionMap(IEnumerable<CityDto> cities);

        IEnumerable<CityViewModel> AutoMapperLinqMap(IEnumerable<CityDto> cities);
    }
}