using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AndresVillarreal_Project.Data;
using AndresVillarreal_Project.Models;

namespace AndresVillarreal_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public AthleteController(SummerGamesContext context)
        {
            _context = context;
        }
        #region GET
        // GET: api/Athlete
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthletes()
        {
            var athletes = await _context.Athletes
                .Include(a => a.Contingent)
                .Include(a => a.Sport)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Affiliation = a.Affiliation,
                    MediaInfo = a.MediaInfo,
                    Gender = a.Gender,
                    RowVersion = a.RowVersion,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name
                    },
                    SportID = a.Sport.ID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name
                    }
                })
                .ToListAsync();
            return athletes;
        }

        // GET: api/Athlete/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AthleteDTO>> GetAthlete(int id)
        {
            var athleteDTO = await _context.Athletes
                .Include(a => a.Contingent)
                .Include(a => a.Sport)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Affiliation = a.Affiliation,
                    MediaInfo = a.MediaInfo,
                    Gender = a.Gender,
                    RowVersion = a.RowVersion,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name
                    },
                    SportID = a.Sport.ID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name
                    }
                })
                .FirstOrDefaultAsync(a => a.ID == id);

            if (athleteDTO == null)
            {
                return NotFound(new { message = "Error: Athlete record not found." });
            }

            return athleteDTO;
        }

        //GET: api/AthleteByContingent
        [HttpGet("ByContingent/{id}")]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthleteByContingent(int id)
        {
            var athleteDTO = await _context.Athletes
                .Include(a => a.Contingent)
                .Where(a => a.ID == id)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Affiliation = a.Affiliation,
                    MediaInfo = a.MediaInfo,
                    Gender = a.Gender,
                    RowVersion = a.RowVersion,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name
                    },
                    SportID = a.Sport.ID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name
                    }
                })
                .ToListAsync();

            if (athleteDTO.Count() > 0)
            {
                return athleteDTO;
            }
            else
            {
                return NotFound(new { message = "Error: Athlete record not found." });
            }


        }
        //GET: api/AthleteBySport
        [HttpGet("BySport/{id}")]
        public async Task<ActionResult<IEnumerable<AthleteDTO>>> GetAthleteBySport(int id)
        {
            var athleteDTO = await _context.Athletes
                .Include(a => a.Sport)
                .Where(a => a.ID == id)
                .Select(a => new AthleteDTO
                {
                    ID = a.ID,
                    FirstName = a.FirstName,
                    MiddleName = a.MiddleName,
                    LastName = a.LastName,
                    AthleteCode = a.AthleteCode,
                    DOB = a.DOB,
                    Height = a.Height,
                    Weight = a.Weight,
                    Affiliation = a.Affiliation,
                    MediaInfo = a.MediaInfo,
                    Gender = a.Gender,
                    RowVersion = a.RowVersion,
                    ContingentID = a.ContingentID,
                    Contingent = new ContingentDTO
                    {
                        ID = a.Contingent.ID,
                        Code = a.Contingent.Code,
                        Name = a.Contingent.Name
                    },
                    SportID = a.Sport.ID,
                    Sport = new SportDTO
                    {
                        ID = a.Sport.ID,
                        Code = a.Sport.Code,
                        Name = a.Sport.Name
                    }
                })
                .ToListAsync();

            if (athleteDTO.Count() > 0)
            {
                return athleteDTO;
            }
            else
            {
                return NotFound(new { message = "Error: Athlete record not found." });
            }

        }
        #endregion


        // PUT: api/Athlete/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAthlete(int id, AthleteDTO athleteDTO)
        {
            if (id != athleteDTO.ID)
            {
                return BadRequest(new { message = "Error: ID does not match Athlete." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Get the record you wants update
            var AthleteToUpdate = await _context.Athletes.FindAsync(id);

            //Check that you got it
            if (AthleteToUpdate == null)
            {
                return NotFound(new { message = "Error: Athlete record not found" });
            }

            //Check Concurrency
            if (athleteDTO.RowVersion != null)
            {
                if (!AthleteToUpdate.RowVersion.SequenceEqual(athleteDTO.RowVersion))
                {
                    return Conflict(new { message = "Concurrency Error: Athlete has been changed bu another user. Try Editing the record again." });
                }
            }

            //Update properties:
            AthleteToUpdate.ID = athleteDTO.ID;
            AthleteToUpdate.FirstName = athleteDTO.FirstName;
            AthleteToUpdate.MiddleName = athleteDTO.MiddleName;
            AthleteToUpdate.LastName = athleteDTO.LastName;
            AthleteToUpdate.AthleteCode = athleteDTO.AthleteCode;
            AthleteToUpdate.DOB = athleteDTO.DOB;
            AthleteToUpdate.Height = athleteDTO.Height;
            AthleteToUpdate.Weight = athleteDTO.Weight;
            AthleteToUpdate.Affiliation = athleteDTO.Affiliation;
            AthleteToUpdate.MediaInfo = athleteDTO.MediaInfo;
            AthleteToUpdate.Gender = athleteDTO.Gender;
            AthleteToUpdate.RowVersion = athleteDTO.RowVersion;
            AthleteToUpdate.ContingentID = athleteDTO.ContingentID;
            AthleteToUpdate.SportID = athleteDTO.SportID;

            //Original RowVersion
            _context.Entry(AthleteToUpdate).Property("RowVersion").OriginalValue = athleteDTO.RowVersion;


            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
                {
                    return NotFound(new { message = "Concurrency Error: Athlete has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Athlete has been updated by another user.  Back out and try editing the record again." });
                }
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Duplicate Athlete Code." });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
                }
            }
        }


        // POST: api/Athlete
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AthleteDTO>> PostAthlete(AthleteDTO athleteDTO)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Athlete athlete = new Athlete
            {
                ID = athleteDTO.ID,
                FirstName = athleteDTO.FirstName,
                MiddleName = athleteDTO.MiddleName,
                LastName = athleteDTO.LastName,
                AthleteCode = athleteDTO.AthleteCode,
                DOB = athleteDTO.DOB,
                Height = athleteDTO.Height,
                Weight = athleteDTO.Weight,
                Affiliation = athleteDTO.Affiliation,
                MediaInfo = athleteDTO.MediaInfo,
                Gender = athleteDTO.Gender,
                RowVersion = athleteDTO.RowVersion,
                ContingentID = athleteDTO.ContingentID,
                SportID = athleteDTO.SportID
            };
            try
            {
                _context.Athletes.Add(athlete);
                await _context.SaveChangesAsync();

                //Assign Database Generated values back into the DTO
                athleteDTO.ID = athlete.ID;
                athlete.RowVersion = athlete.RowVersion;

                return CreatedAtAction(nameof(GetAthlete), new { id = athlete.ID }, athleteDTO);
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("UNIQUE"))
                {
                    return BadRequest(new { message = "Unable to save: Duplicate Athlete Code." });
                }
                else
                {
                    return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
                }
            }



        }

        // DELETE: api/Athlete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Athlete>> DeleteAthlete(int id)
        {
            var athlete = await _context.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound(new { message = "Delete Error: Athlete has already been removed." });
            }

            try
            {
                _context.Athletes.Remove(athlete);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Delete Error: Unable to delete Athlete." });
            }
        }



        private bool AthleteExists(int id)
        {
            return _context.Athletes.Any(e => e.ID == id);
        }


    }

}



