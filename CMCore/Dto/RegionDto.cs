﻿using System.Collections.Generic;

namespace CMCore.DTO
{
    public class RegionDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IList<CountrieDto> Countries { get; set; }
    }
}