using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndresVillarreal_Project.Data;
using AndresVillarreal_Project.Models;
using System.Numerics;

namespace AndresVillarreal_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public SportController(SummerGamesContext context)
        {
            _context = context;
        }

        // GET: api/Sport
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetSports()
        {
            var sportsDTO = await _context.Sports
                .Select(s => new SportDTO
                {
                    ID = s.ID,
                    Code = s.Code,
                    Name = s.Name,
                }).ToListAsync();

            return sportsDTO;
        }

        // GET: api/Sport/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SportDTO>> GetSport(int id)
        {
            var sportDTO = await _context.Sports
                .Select(s => new SportDTO
                {
                    ID = s.ID,
                    Code = s.Code,
                    Name = s.Name
                })
                .FirstOrDefaultAsync(s => s.ID == id);
                

            if (sportDTO == null)
            {
                return NotFound();
            }

            return sportDTO;
        }

        //GET: api/Sports/inc -Include Athletes Collection
        [HttpGet("inc")]
        public async Task<ActionResult<IEnumerable<SportDTO>>> GetSportsInc()
        {
            return await _context.Sports
                .Include(s => s.Athletes)
                .Select(s => new SportDTO
                {
                    ID = s.ID,
                    Code = s.Code,
                    Name = s.Name,
                    Athletes = s.Athletes.Select(cAthletes => new AthleteDTO
                    {
                        ID = cAthletes.ID,
                        FirstName = cAthletes.FirstName,
                        MiddleName = cAthletes.MiddleName,
                        LastName = cAthletes.LastName,
                        AthleteCode = cAthletes.AthleteCode,
                        DOB = cAthletes.DOB,
                        Height = cAthletes.Height,
                        Weight = cAthletes.Weight,
                        Affiliation = cAthletes.Affiliation,
                        MediaInfo = cAthletes.MediaInfo,
                        Gender = cAthletes.Gender,
                        SportID = cAthletes.SportID,
                    }).ToList()
                }).ToListAsync();
        }

        //GET: api/Sport/inc/5 -Include Athletes Collection
        [HttpGet("inc/{id}")]
        public async Task<ActionResult<SportDTO>> GetSportInc(int id)
        {
            var sport = await _context.Sports
                .Include(s => s.Athletes)
                .Select(s => new SportDTO
                {
                    ID = s.ID,
                    Code = s.Code,
                    Name = s.Name,
                    Athletes = s.Athletes.Select(sA => new AthleteDTO
                    {
                        ID = sA.ID,
                        FirstName = sA.FirstName,
                        MiddleName = sA.MiddleName,
                        LastName = sA.LastName,
                        AthleteCode = sA.AthleteCode,
                        DOB = sA.DOB,
                        Height = sA.Height,
                        Weight = sA.Weight,
                        Affiliation = sA.Affiliation,
                        MediaInfo = sA.MediaInfo,
                        Gender = sA.Gender,
                        SportID = sA.SportID
                    }).ToList()
                }).FirstOrDefaultAsync(s => s.ID == id);

            if (sport == null)
            {
                return NotFound(new {message ="Error: Sport not Found."});
            }
            return sport;
        }




        

        // PUT: api/Sport/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSport(int id, SportDTO sportDTO)
        {
            if (id != sportDTO.ID)
            {
                return BadRequest(new { message = "Error: Incorrect ID for Sport." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sportToUpdate = await _context.Sports.FindAsync(id);

            //Check that you got it
            if (sportToUpdate == null)
            {
                return NotFound(new { message = "Error: Doctor record not found." });
            }

            sportToUpdate.ID = sportDTO.ID;
            sportToUpdate.Code = sportDTO.Code;
            sportToUpdate.Name = sportDTO.Name;

            _context.Entry(sportDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportExists(id))
                {
                    return Conflict(new { message = "Concurrency Error: Sport has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Sport has been updated by another user.  Back out and try editing the record again." });
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
            }
        }

        // POST: api/Sport
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SportDTO>> PostSport(SportDTO sportDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Sport sport = new Sport
            {
                ID = sportDTO.ID,
                Code = sportDTO.Code,
                Name = sportDTO.Name
            };

            try
            {
                _context.Sports.Add(sport);
                await _context.SaveChangesAsync();
                //Assign Database Generated values back into the DTO
                sportDTO.ID = sport.ID;
                //doctorDTO.RowVersion = doctor.RowVersion;
                return CreatedAtAction(nameof(GetSport), new { id = sport.ID }, sportDTO);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
            }
            
        }

        // DELETE: api/Sport/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSport(int id)
        {
            var sport = await _context.Sports.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            _context.Sports.Remove(sport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SportExists(int id)
        {
            return _context.Sports.Any(e => e.ID == id);
        }
    }
}
