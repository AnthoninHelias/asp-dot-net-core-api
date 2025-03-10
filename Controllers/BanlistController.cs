using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using API.DTO;


[ApiController]
[Route("api/[controller]")]
public class BanlistController : Controller
{
    private readonly ProjetGestionContext _dbContext;

    public BanlistController(ProjetGestionContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetBanlists")]
    public async Task<ActionResult<List<Banlist>>> Get()
    {
        var list = await _dbContext.Set<Banlist>().Select(
            s => new Banlist
            {
                Id = s.Id,
                Nom = s.Nom,
                Limitée = s.Limitée,
                Interdite = s.Interdite
            }
        ).ToListAsync();

        if (list.Count < 0)
        {
            return NotFound();
        }
        else
        {
            return list;
        }
    }

    [HttpGet("GetBanlistById")]
    public async Task<ActionResult<Banlist>> GetBanlistById(int Id)
    {
        Banlist Banlist = await _dbContext.Set<Banlist>().Select(s => new Banlist
        {
            Id = s.Id,
            Nom = s.Nom,
            Limitée = s.Limitée,
            Interdite = s.Interdite
        }).FirstOrDefaultAsync(s => s.Id == Id);
        if (Banlist == null)
        {
            return NotFound();
        }
        else
        {
            return Banlist;
        }
    }

    [HttpPost("InsertBanlist")]
    public async Task<HttpStatusCode> InsertBanlist(BanlistDTO Banlist)
    {
        var entity = new Banlist()
        {
            Nom = Banlist.Nom,
            Limitée = Banlist.Limitée,
            Interdite = Banlist.Interdite
        };
        _dbContext.Set<Banlist>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.Created;
    }

    [HttpPut("UpdateBanlist")]
    public async Task<HttpStatusCode> UpdateBanlist(Banlist Banlist)
    {
        var entity = await _dbContext.Set<Banlist>().FirstOrDefaultAsync(s => s.Id == Banlist.Id);
        if (entity == null)
        {
            return HttpStatusCode.NotFound;
        }
        else
        {
            entity.Nom = Banlist.Nom;
            entity.Limitée = Banlist.Limitée;
            entity.Interdite = Banlist.Interdite;
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }

    [HttpDelete("DeleteBanlist/{Id}")]
    public async Task<HttpStatusCode> DeleteBanlist(int Id)
    {
        var entity = new Banlist()
        {
            Id = Id
        };
        _dbContext.Set<Banlist>().Attach(entity);
        _dbContext.Set<Banlist>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
}
