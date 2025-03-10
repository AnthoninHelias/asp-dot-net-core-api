using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using API.DTO;

[ApiController]
[Route("api/[controller]")]
public class CarteController : Controller
{
    private readonly ProjetGestionContext _dbContext;

    public CarteController(ProjetGestionContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetCartes")]
    public async Task<ActionResult<List<Carte>>> Get()
    {
        var list = await _dbContext.Set<Carte>().Select(
            s => new Carte
            {
                Id = s.Id,
                Nom = s.Nom,
                Rareté = s.Rareté
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

    [HttpGet("GetCarteById")]
    public async Task<ActionResult<Carte>> GetCarteById(int Id)
    {
        Carte Carte = await _dbContext.Set<Carte>().Select(s => new Carte
        {
            Id = s.Id,
            Nom = s.Nom,
            Rareté = s.Rareté
        }).FirstOrDefaultAsync(s => s.Id == Id);
        if (Carte == null)
        {
            return NotFound();
        }
        else
        {
            return Carte;
        }
    }

    [HttpPost("InsertCarte")]
    public async Task<HttpStatusCode> InsertCarte(Carte Carte)
    {
        var entity = new Carte()
        {
            Nom = Carte.Nom,
            Rareté = Carte.Rareté
        };
        _dbContext.Set<Carte>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.Created;
    }

    [HttpPut("UpdateCarte")]
    public async Task<HttpStatusCode> UpdateCarte(Carte Carte)
    {
        var entity = await _dbContext.Set<Carte>().FirstOrDefaultAsync(s => s.Id == Carte.Id);
        if (entity == null)
        {
            return HttpStatusCode.NotFound;
        }
        else
        {
            entity.Nom = Carte.Nom;
            entity.Rareté = Carte.Rareté;
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }

    [HttpDelete("DeleteCarte/{Id}")]
    public async Task<HttpStatusCode> DeleteCarte(int Id)
    {
        var entity = new Carte()
        {
            Id = Id
        };
        _dbContext.Set<Carte>().Attach(entity);
        _dbContext.Set<Carte>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
}