using NUnit.Framework;
using System.Linq;
using Test.Adapters;

namespace Test.Tests
{
    [TestFixture]
    public class AdapterTests
    {
        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingILMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.ILMap<CityDto, CityViewModel>(citiesToTest).ToList();

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingExpressionMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.ExpressionMap<CityDto, CityViewModel>(citiesToTest).ToList();

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingAutomapperCollectionMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.AutoMapperCollectionMap(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingLinqMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.LinqMap(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingAutomapperManualMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.AutoMapperCollectionMap(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingParallelForEachManualMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.ParallelForEachManualMap(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingAutomapperParallelLinqMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.ParallelLinqMap(citiesToTest);

            CollectionAssert.AreEqual(result.OrderBy(p => p.Country), citiesToTest.OrderBy(p => p.Country));
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingAutoMapperMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.AutoMapperMap(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingAutoMapperLinqMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.AutoMapperLinqMap(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingAutoMapperCollectionMap()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.AutoMapperCollectionMap(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingReflectionOrderedPropertiesCopy()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.ReflectionOrderedPropertiesCopy<CityDto, CityViewModel>(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }

        [Test]
        public void ExpectDtosCorrectlyMappedWhenUsingReflectionPropertySearchCopy()
        {
            var adapter = new CityDtoToCityViewModelAdapter();
            var citiesToTest = DataSource.GetCitiesDtos(100);
            var result = adapter.ReflectionPropertySearchCopy<CityDto, CityViewModel>(citiesToTest);

            CollectionAssert.AreEqual(result, citiesToTest);
        }
    }
}
