using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Collections.Generic;
using System.Linq;
using Test.Adapters;

namespace Test.Benchmarks
{
    public class AdapterBenchmarks
    {
        private ICityDtoToCityViewModelAdapter _adapter;
        private IEnumerable<CityDto> _citiesToTest;

        [Params(50, 250, 500, 1000, 2500, 5000, 10000, 15000, 25000, 50000, 75000, 100000, 150000)]
        public int DtosCount { get; set; }

        [Setup]        
        public void Setup()
        {
            _citiesToTest = DataSource.GetCitiesDtos(DtosCount);
            _adapter = new CityDtoToCityViewModelAdapter();            
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ManualMap()
        {
            return _adapter.ManualMap(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ManualForMap()
        {
            return _adapter.ManualForMap(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> AutoMapperMap()
        {
            return _adapter.AutoMapperMap(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> AutoMapperCollectionMap()
        {
            return _adapter.AutoMapperCollectionMap(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> AutoMapperLinqMap()
        {
            return _adapter.AutoMapperLinqMap(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ILMap()
        {
            return _adapter.ILMap<CityDto, CityViewModel>(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ExpressionMap()
        {
            return _adapter.ExpressionMap<CityDto, CityViewModel>(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ParallelForEach()
        {
            return _adapter.ParallelForEachManualMap(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ParallelLinqMapDOP10()
        {
            return _adapter.ParallelLinqMap(_citiesToTest).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ParallelLinqMapDOP20()
        {
            return _adapter.ParallelLinqMap(_citiesToTest, 3).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ParallelLinqMapDOP30()
        {
            return _adapter.ParallelLinqMap(_citiesToTest, 4).ToList();
        }

        [Benchmark]
        public IEnumerable<CityViewModel> ParallelLinqMapDOP50()
        {
            return _adapter.ParallelLinqMap(_citiesToTest, 5).ToList();
        }
    }
}
