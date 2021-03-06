﻿using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CMCore.DTO;
using CMCore.Interfaces;
using CMCore.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CMCore.Controllers
{
	[Produces("application/json")]
	[Route("club/")]
	[EnableCors("AllowSpecificOrigin")]
	public class ClubController : Controller
	{

		private readonly IClubService _clubService;
		private readonly IMapper _mapper;

		public ClubController(IClubService clubService, IMapper mapper)
		{
			_clubService = clubService;
			_mapper = mapper;
		}

		// GET club/
		[HttpGet]
		public IActionResult Get(string name = null)
		{
			var clubs = _clubService.FindAll(name).ProjectTo<ClubDto>(_mapper.ConfigurationProvider).ToList();
			if (!clubs.Any())
				return BadRequest("No Clubs");

			return Ok(clubs);
		}

		// GET club/id
		[HttpGet("{id}")]
		public IActionResult Get(int id)
		{
			var clubInDb = _clubService.Exist(id).ProjectTo<ClubDto>(_mapper.ConfigurationProvider).FirstOrDefault();
			if (clubInDb == null)
				return BadRequest("Club doesn't exist!");

			return Ok(clubInDb);
		}

		// PATCH club/id
		[HttpPatch("{id}")]
		public async Task<IActionResult> Edit(int id, [FromBody] ClubDto clubDto)
		{
			if (clubDto == null)
				return BadRequest("You send an invalid club");

			var clubInDb = _clubService.Exist(id)
				.Include(t => t.ClubTypes)
				.Include(r => r.ClubRegions).FirstOrDefault();
			if (clubInDb == null)
				return BadRequest("Club doesn't exist!");

			var errorMsg = _clubService.CheckSameName(clubDto.Name);
			if (errorMsg != null)
				return BadRequest(errorMsg);

			// Clear Relationships
			if (!_clubService.ClearRelations(clubInDb))
				return BadRequest("Club can't be updated!");

			// Validate and Add Types
			var typesErrMsg = _clubService.AddTypeR(clubInDb, clubDto);
			if (!string.IsNullOrWhiteSpace(typesErrMsg))
				return BadRequest(typesErrMsg);

			// Validate and Add Region and/or country
			var regionCountryErrMsg = _clubService.AddRegionCountryR(clubInDb, clubDto);
			if (!string.IsNullOrWhiteSpace(regionCountryErrMsg))
				return BadRequest(regionCountryErrMsg);

			clubDto = _clubService.Edit(clubInDb, clubDto);

			var saved = await _clubService.SaveEf();
			if (!saved)
				return BadRequest();

			return Ok(_clubService.Exist(clubDto.Id).ProjectTo<ClubDto>(_mapper.ConfigurationProvider).FirstOrDefault());
		}

		// Delete club/id
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var clubInDb = _clubService.Exist(id)
				.Include(r => r.ClubRegions)
				.Include(r => r.ClubTypes).FirstOrDefault();
			if (clubInDb == null)
				return BadRequest("Club doesn't exist!");

			var delete = _clubService.Erase(clubInDb);
			if (!delete)
				return BadRequest("Club not deleted!");

			var saved = await _clubService.SaveEf();
			if (!saved)
				return BadRequest();

			return Ok("Region Deleted: " + clubInDb.Name);
		}

		// POST club/
		[HttpPost]
		public async Task<IActionResult> New([FromBody] ClubDto clubDto)
		{
			var errorMsg = _clubService.Validate(clubDto);
			if (errorMsg != null)
				return BadRequest(errorMsg);

			var newClub = _clubService.CreateNew(clubDto);

			// Validate and Add Types
			var typesErrMsg = _clubService.AddTypeR(newClub, clubDto);
			if (!string.IsNullOrWhiteSpace(typesErrMsg))
				return BadRequest(typesErrMsg);

			// Validate and Add Region and/or club
			var regionCountryErrMsg = _clubService.AddRegionCountryR(newClub, clubDto);
			if (!string.IsNullOrWhiteSpace(regionCountryErrMsg))
				return BadRequest(regionCountryErrMsg);

			var saved = await _clubService.SaveEf();
			if (!saved)
				return BadRequest();

			return Ok(Mapper.Map<Club, ClubDto>(newClub));
		}
	}
}