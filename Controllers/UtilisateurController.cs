using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using API.DTO;

[ApiController]
[Route("api/[controller]")]
public class UtilisateurController : Controller
{
    private readonly ProjetGestionContext _dbContext;

    public UtilisateurController(ProjetGestionContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("GetUtilisateurs")]
    public async Task<ActionResult<List<UtilisateurDTO>>> Get()
    {
        var List = await _dbContext.Set<Utilisateur>().Select(
            s => new UtilisateurDTO
            {
                Id = s.Id,
                Nom = s.Nom,
                MotDePasse = s.MotDePasse
            }
        ).ToListAsync();

        if (List.Count < 0)
        {
            return NotFound();
        }
        else
        {
            return List;
        }
    }

    [HttpGet("GetUtilisateurById")]
    public async Task<ActionResult<UtilisateurDTO>> GetUtilisateurById(int Id)
    {
        UtilisateurDTO Utilisateur = await _dbContext.Set<Utilisateur>().Select(s => new UtilisateurDTO
        {
            Id = s.Id,
            Nom = s.Nom,
            MotDePasse = s.MotDePasse
        }).FirstOrDefaultAsync(s => s.Id == Id);
        if (Utilisateur == null)
        {
            return NotFound();
        }
        else
        {
            return Utilisateur;
        }
    }

    [HttpPost("InsertUtilisateur")]
    public async Task<HttpStatusCode> InsertUtilisateur(UtilisateurDTO Utilisateur)
    {
        var entity = new Utilisateur()
        {
            Nom = Utilisateur.Nom,
            MotDePasse = Utilisateur.MotDePasse
        };
        _dbContext.Set<Utilisateur>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.Created;
    }

    [HttpPut("UpdateUtilisateur")]
    public async Task<HttpStatusCode> UpdateUtilisateur(UtilisateurDTO Utilisateur)
    {
        var entity = await _dbContext.Set<Utilisateur>().FirstOrDefaultAsync(s => s.Id == Utilisateur.Id);
        if (entity == null)
        {
            return HttpStatusCode.NotFound;
        }
        else
        {
            entity.Nom = Utilisateur.Nom;
            entity.MotDePasse = Utilisateur.MotDePasse;
            await _dbContext.SaveChangesAsync();
            return HttpStatusCode.OK;
        }
    }

    [HttpDelete("DeleteUtilisateur/{Id}")]
    public async Task<HttpStatusCode> DeleteUtilisateur(int Id)
    {
        var entity = new Utilisateur()
        {
            Id = Id
        };
        _dbContext.Set<Utilisateur>().Attach(entity);
        _dbContext.Set<Utilisateur>().Remove(entity);
        await _dbContext.SaveChangesAsync();
        return HttpStatusCode.OK;
    }
}
