using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public static class DataSource
    {
        private static Random _random = new Random();
        private const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int MaxStringLength = 8;
        private const int MaxPopulationValue = 100000;
        private const int MaxYearValue = 3000;
        private const int MaxMonthValue = 12;
        private const int MaxDayValue = 20;

        public static IEnumerable<CityDto> GetCitiesDtos(int numberOfDtos)
        {
            return Enumerable.Range(1, numberOfDtos).Select(p =>
            new CityDto
            {
                Country = GetRandomString(),
                EstablishedIn = new DateTime(_random.Next(1, MaxYearValue), _random.Next(1, MaxMonthValue), _random.Next(1, MaxDayValue)),
                Mayor = GetRandomString(),
                Name = GetRandomString(),
                Population = _random.Next(1, MaxPopulationValue),
                Voivodship = GetRandomString()
            }).ToList();
        }

        private static string GetRandomString()
        {
            return new string(
                Enumerable.Range(1, MaxStringLength)
                .Select(p => characters[_random.Next(0, characters.Length)])
                .ToArray());
        }
    }
}
