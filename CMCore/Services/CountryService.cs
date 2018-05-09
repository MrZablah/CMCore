﻿using System.Linq;
using AutoMapper;
using CMCore.Data;
using CMCore.DTO;
using CMCore.Interfaces;
using CMCore.Models;

namespace CMCore.Services
{
    public class CountryService : GenericService<Countrie, CountrieDto>, ICountryService
    {
        public CountryService(ContentManagerDbContext context) : base(context)
        {
        }

        public CountrieDto Edit(Countrie countrieInDb, CountrieDto countrieDto)
        {

            if (Compare(countrieInDb, countrieDto) != null)
                return Mapper.Map<Countrie, CountrieDto>(countrieInDb);

            if (string.IsNullOrEmpty(countrieDto.Name))
                return Mapper.Map<Countrie, CountrieDto>(countrieInDb);

            countrieInDb.Name = countrieDto.Name;

            return Mapper.Map<Countrie, CountrieDto>(countrieInDb);
        }

        public string AddRegionR(CountrieDto countrieDto, Region regionInDb)
        {
            var countrieInDb = ExistName(countrieDto.Name).FirstOrDefault();
            if (countrieInDb == null)
            {
                var createCountry = new Countrie
                {
                    Name = countrieDto.Name,
                    RegionId = regionInDb.Id
                };
                AddEf(createCountry);
                return null;
            }

            if (string.IsNullOrEmpty(countrieDto.Name))
                return "You send a null or empty countrie!";

            var regionHasCountry = regionInDb.Countries.Any(cr => cr.RegionId == countrieInDb.RegionId);
            if (!regionHasCountry)
            {
                countrieInDb.RegionId = regionInDb.Id;
            }

            return null;

        }

        public Countrie CreateNew(CountrieDto countrieDto)
        {
            var newCountrie = new Countrie
            {
                Name = countrieDto.Name
            };

            return AddEf(newCountrie) ? newCountrie : default(Countrie);
        }

    }
}
