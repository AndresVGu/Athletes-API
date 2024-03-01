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
    public class ContingentController : ControllerBase
    {
        private readonly SummerGamesContext _context;

        public ContingentController(SummerGamesContext context)
        {
            _context = context;
        }

        // GET: api/Contingent
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContingentDTO>>> GetContingents()
        {
            var contingent = await _context.Contingents
                .Select(c => new ContingentDTO
                {
                    ID = c.ID,
                    Code = c.Code,
                    Name = c.Name
                })
                .ToListAsync();
           
            return contingent;
        }

        // GET: api/Contingent/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContingentDTO>> GetContingent(int id)
        {
            var contingentDTO = await _context.Contingents
                .Select(c => new ContingentDTO
                {
                    ID = c.ID,
                    Code = c.Code,
                    Name = c.Name
                })
                .FirstOrDefaultAsync(c => c.ID == id);
             

           if(contingentDTO == null)
            {
                return NotFound(new {message = "Error: Contingent not found."});
            }

            return contingentDTO;
        }

        //GET: api/Contingent/inc -Include Athletes Collection
        [HttpGet("inc")]
        public async Task<ActionResult<IEnumerable<ContingentDTO>>> GetContingentsInc()
        {
            return await _context.Contingents
                .Include(c => c.Athletes)
                .Select(c => new ContingentDTO
                {
                    ID = c.ID,
                    Code = c.Code,
                    Name = c.Name,
                    Athletes = c.Athletes.Select(cAthletes => new AthleteDTO
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
                        ContingentID = cAthletes.ContingentID,
                    }).ToList()
                }).ToListAsync();
        }

        //GET: api/Contingent/inc/5 -Include Athletes Collection
        [HttpGet("inc/{id}")]
        public async Task<ActionResult<ContingentDTO>> GetContingentInc(int id)
        {
            var contingentDTO = await _context.Contingents
                .Include(c => c.Athletes)
                .Select(c => new ContingentDTO
                {
                    ID = c.ID,
                    Code = c.Code,
                    Name = c.Name,
                    Athletes = c.Athletes.Select(cAthletes => new AthleteDTO
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
                        ContingentID = cAthletes.ContingentID,
                    }).ToList()
                }).FirstOrDefaultAsync(c => c.ID == id);

            if(contingentDTO == null)
            {
                return NotFound(new { message = "Error: Contingent not found." });
            }
            return contingentDTO;
        }

        // PUT: api/Contingent/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContingent(int id, ContingentDTO contingentDTO)
        {
            if (id != contingentDTO.ID)
            {
                return BadRequest(new { message = "Error: Incorrect ID for Contingent." });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contingentToUpdate = await _context.Contingents.FindAsync(id);

            if (contingentToUpdate == null)
            {
                return NotFound(new { message = "Error: Contingent record not found." });
            }

            contingentToUpdate.ID = contingentDTO.ID;
            contingentToUpdate.Code = contingentDTO.Code;
            contingentToUpdate.Name = contingentDTO.Name;

            _context.Entry(contingentDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContingentExists(id))
                {
                    return Conflict(new { message = "Concurrency Error: Contingent has been Removed." });
                }
                else
                {
                    return Conflict(new { message = "Concurrency Error: Contingent has been updated by another user.  Back out and try editing the record again." });
                }
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
            }
        }

        // POST: api/Contingent
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContingentDTO>> PostContingent(ContingentDTO contingentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contingent contingent = new Contingent
            {
                ID = contingentDTO.ID,
                Code = contingentDTO.Code,
                Name = contingentDTO.Name
            };

            try
            {
                _context.Contingents.Add(contingent);
                await _context.SaveChangesAsync();
                //Assign Database Generated values back into the DTO
                contingentDTO.ID = contingent.ID;
               // contingentDTO.RowVersion = contingent.RowVersion;
                return CreatedAtAction(nameof(GetContingent), new { id = contingent.ID }, contingentDTO);
            }
            catch (DbUpdateException)
            {
                return BadRequest(new { message = "Unable to save changes to the database. Try again, and if the problem persists see your system administrator." });
            }
            
        }

        // DELETE: api/Contingent/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contingent>> DeleteContingent(int id)
        {
            var contingent = await _context.Contingents.FindAsync(id);
            if (contingent == null)
            {
                return NotFound(new { message = "Delete Error: Contingent has already been removed." });
            }

            try
            {
                _context.Contingents.Remove(contingent);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    return BadRequest(new { message = "Delete Error: Remember, you cannot delete a Contingent that has Athletes assigned." });
                }
                else
                {
                    return BadRequest(new { message = "Delete Error: Unable to delete Contingent. Try again, and if the problem persists see your system administrator." });
                }
            }
        }

        private bool ContingentExists(int id)
        {
            return _context.Contingents.Any(e => e.ID == id);
        }
    }
}
