using AutoMapper;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test.Adapters
{
    public class CityDtoToCityViewModelAdapter : ICityDtoToCityViewModelAdapter
    {
        public CityDtoToCityViewModelAdapter()
        {
            ConfigureMappings();
        }
        
        public IEnumerable<CityViewModel> ParallelLinqMap(IEnumerable<CityDto> cities, int degreeOfParallelism = 2)
        {
            return cities.AsParallel().WithDegreeOfParallelism(degreeOfParallelism).Select(city => new CityViewModel
            {
                Country = city.Country,
                EstablishedIn = city.EstablishedIn,
                Mayor = city.Mayor,
                Name = city.Name,
                Population = city.Population,
                Voivodship = city.Voivodship
            });
        }

        public IEnumerable<CityViewModel> ParallelForEachManualMap(IEnumerable<CityDto> cities)
        {
            var citiesArray = cities.ToArray();
            var resultList = new CityViewModel[citiesArray.Length];

            var rangePartitioner = Partitioner.Create(0, citiesArray.Length);
            Parallel.ForEach(rangePartitioner, (range, loopState) =>
            {   
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    var cityViewModel = new CityViewModel
                    {
                        Country = citiesArray[i].Country,
                        EstablishedIn = citiesArray[i].EstablishedIn,
                        Mayor = citiesArray[i].Mayor,
                        Name = citiesArray[i].Name,
                        Population = citiesArray[i].Population,
                        Voivodship = citiesArray[i].Voivodship
                    };
                    resultList[i] = cityViewModel;                   
                }
            });

            return resultList;       
        }

        public IEnumerable<TOut> ExpressionMap<TIn, TOut>(IEnumerable<TIn> sourceCollection) where TOut : new()
        {
            var outputList = new List<TOut>(sourceCollection.Count());
            foreach (var item in sourceCollection)
            {
                outputList.Add(ILMapper.ExpressionMap<TIn, TOut>(item));
            }
            return outputList;
        }

        public IEnumerable<TOut> ILMap<TIn, TOut>(IEnumerable<TIn> sourceCollection) where TOut : new()
        {
            var outputList = new List<TOut>(sourceCollection.Count());
            foreach(var item in sourceCollection)
            {
                outputList.Add(ILMapper.ILMap<TIn, TOut>(item));
            }
            return outputList;
        }

        public IEnumerable<TOut> ReflectionOrderedPropertiesCopy<TIn, TOut>(IEnumerable<TIn> sourceCollection) where TOut : new()
        {
            var destinationList = new List<TOut>(sourceCollection.Count());
            var sourcePropertiesCollection = typeof(TIn).GetProperties().OrderBy(p => p.Name).ToArray();
            var destinationPropertiesCollection = typeof(TOut).GetProperties().OrderBy(p => p.Name).ToArray();
            var itemsCount = sourcePropertiesCollection.Length;

            foreach (var collectionItem in sourceCollection)
            {
                var destinationItem = new TOut();
                for (var i = 0; i < itemsCount; i++)
                {
                    destinationPropertiesCollection[i].SetValue(destinationItem, sourcePropertiesCollection[i].GetValue(collectionItem));
                }
                destinationList.Add(destinationItem);
            }
            return destinationList;
        }

        public IEnumerable<TOut> ReflectionPropertySearchCopy<TIn, TOut>(IEnumerable<TIn> sourceCollection) where TOut : new()
        {
            var destinationList = new List<TOut>(sourceCollection.Count());
            var sourcePropertiesCollection = typeof(TIn).GetProperties();
            var destinationPropertiesCollection = typeof(TOut).GetProperties();

            foreach (var collectionItem in sourceCollection)
            {
                var destinationItem = new TOut();
                foreach (var sourceProp in sourcePropertiesCollection)
                {
                    var value = sourceProp.GetValue(collectionItem);
                    var destinationProp = destinationPropertiesCollection.First(property => property.Name == sourceProp.Name);
                    destinationProp.SetValue(destinationItem, value);
                }
                destinationList.Add(destinationItem);
            }

            return destinationList;
        }

        public IEnumerable<CityViewModel> ManualForArrayMap(CityDto[] cities)
        {
            var citiesArray = new CityViewModel[cities.Length];            
            for (int i = 0; i < cities.Length; i++)
            {
                var newCity = new CityViewModel
                {
                    Country = cities[i].Country,
                    EstablishedIn = cities[i].EstablishedIn,
                    Mayor = cities[i].Mayor,
                    Name = cities[i].Name,
                    Population = cities[i].Population,
                    Voivodship = cities[i].Voivodship
                };
                citiesArray[i] = newCity;
            }

            return citiesArray;
        }

        public IEnumerable<CityViewModel> ManualMap(IEnumerable<CityDto> cities)
        {
            var list = new List<CityViewModel>(cities.Count());
            foreach (var city in cities)
            {
                var newCity = new CityViewModel
                {
                    Country = city.Country,
                    EstablishedIn = city.EstablishedIn,
                    Mayor = city.Mayor,
                    Name = city.Name,
                    Population = city.Population,
                    Voivodship = city.Voivodship
                };
                list.Add(newCity);
            }

            return list;
        }

        public IEnumerable<CityViewModel> LinqMap(IEnumerable<CityDto> cities)
        {
            return cities.Select(city => new CityViewModel
            {
                Country = city.Country,
                EstablishedIn = city.EstablishedIn,
                Mayor = city.Mayor,
                Name = city.Name,
                Population = city.Population,
                Voivodship = city.Voivodship
            });
        }

        public IEnumerable<CityViewModel> AutoMapperMap(IEnumerable<CityDto> cities)
        {
            var list = new List<CityViewModel>(cities.Count());
            foreach (var city in cities)
            {
                list.Add(Mapper.Map<CityViewModel>(city));
            }
            return list;
        }

        public IEnumerable<CityViewModel> AutoMapperCollectionMap(IEnumerable<CityDto> cities)
        {
            return Mapper.Map<IEnumerable<CityViewModel>>(cities);
        }

        public IEnumerable<CityViewModel> AutoMapperLinqMap(IEnumerable<CityDto> cities)
        {
            return cities.Select(city => Mapper.Map<CityViewModel>(city));
        }
        
        private void ConfigureMappings()
        {
            Mapper.Initialize(config => config.CreateMap<CityDto, CityViewModel>());
            ILMapper.PrepareILMapping<CityDto, CityViewModel>();            
            ILMapper.PrepareExpressionMapping<CityDto, CityViewModel>();
        }
    }
}
