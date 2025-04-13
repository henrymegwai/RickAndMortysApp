using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RickAndMorty.Application.Dtos;
using RickAndMorty.Application.Features.Character.Commands;
using RickAndMorty.Application.Features.Character.Queries;
using RickAndMorty.Application.Models;
using RickAndMorty.WebApp.Models;
using RickAndMorty.WebApp.ViewModels;

namespace RickAndMorty.WebApp.Controllers;

public class CharacterController(
    ILogger<CharacterController> logger,
    IMediator mediator,
    IMemoryCache memoryCache)
    : Controller
{
    private const string CacheKey = "CharactersCache";
    private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);

    [HttpGet]
    public async Task<IActionResult> Index(CancellationToken cancellationToken, int page = 1, int pageSize = 20)
    {
        try
        {
            var fromCache = true;

            // Try to get from cache first
            if (!memoryCache.TryGetValue(CacheKey, out List<CharacterDto>? charactersDtos))
            {
                fromCache = false;
                
                charactersDtos =  
                        await mediator.Send(new GetCharacterFromDbQuery(page, pageSize), cancellationToken);
                
                // Cache the results
                memoryCache.Set(CacheKey, charactersDtos, new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(_cacheDuration));
            }
            else
            {
                charactersDtos = memoryCache.Get<List<CharacterDto>>(CacheKey);
            }

            // Set response header
            Response.Headers.Append("from-database", fromCache.ToString());

            var viewModel = new CharacterViewModel
            {
                Characters = charactersDtos,
                FromCache = fromCache,
                CurrentPage = page,
                PageSize = pageSize
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting characters");
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(CharacterModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            // Invalidate the cache when new character is added
            memoryCache.Remove(CacheKey);
            
            await mediator.Send(new AddCharacterCommand(model), cancellationToken);
            
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error adding character");
            ModelState.AddModelError("", "An error occurred while adding the character");
            return View(model);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var character = await mediator.Send(new GetCharacterByIdQuery(id), cancellationToken);

        var viewModel = new CharacterDetailsViewModel
        {
            Name = character.Name,
            Status = character.Status,
            Species = character.Species,
            Gender = character.Gender,
            OriginName = character.OriginName,
            OriginUrl = character.OriginUrl,
            LocationName = character.LocationName,
            LocationUrl = character.LocationUrl,
            Url = character.Url,
            Image = character.Image,
            Created = character.Created
        };

        return View(viewModel);
    }
    
    [HttpGet("GetByName/{name}")]
    public async Task<IActionResult> GetByName(string name, CancellationToken cancellationToken)
    {
        
        var character = await mediator.Send(new GetCharacterByNameQuery(name),cancellationToken);

        var viewModel = new CharacterDetailsViewModel
        {
            Name = character.Name,
            Status = character.Status,
            Species = character.Species,
            Gender = character.Gender,
            OriginName = character.OriginName,
            OriginUrl = character.OriginUrl,
            LocationName = character.LocationName,
            LocationUrl = character.LocationUrl,
            Url = character.Url,
            Image = character.Image,
            Created = character.Created
        };

        return View(viewModel);
    }
    
    [HttpGet]
    public async Task<IActionResult> Planet(
        string name, 
        CancellationToken cancellationToken, 
        int page = 1, 
        int pageSize = 20)
    {
        try
        {
            var characters = 
                await mediator.Send(new GetCharacterByPlanetQuery(page, pageSize, name), cancellationToken);
            
            ViewBag.Title = $"Character(s) from planet: {name}";
            
            var viewModel = new CharacterViewModel
            {
                Characters = characters,
                CurrentPage = page,
                PageSize = pageSize
            };
            
            return View(viewModel);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Error getting characters for planet {name}");
            return View("Error", new ErrorViewModel { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> Planets(CancellationToken cancellationToken)
    {
        try
        {
            var locations = await mediator.Send(new GetCharacterLocationsQuery(), cancellationToken);
            return View(locations);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting character locations");
            return View("Error", new ErrorViewModel { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}