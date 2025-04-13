using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RickAndMorty.Application.Features.Character.Fetched;
using RickAndMorty.Infrastructure.Common.Enums;
using RickAndMorty.Web.Pages.ViewModels;

namespace RickAndMorty.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IMediator _mediator;
    
    public IndexModel(ILogger<IndexModel> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public async Task OnGet()
    {
        // This method is called on GET requests to the Index page
        // You can add any logic here that you want to execute when the page is loaded
        // For example, you might want to load some data from a database or an API
        
       var characters =  await _mediator.Send(new GetCharacterByStatusQuery(CharacterStatus.Alive));
       
       try
       {
           var charactwers = new CharacterViewModel() { };
           charactwers.Characters = await _mediator.Send(new GetCharacterByStatusQuery(CharacterStatus.Alive));
       }
       catch (Exception ex)
       {
           _logger.LogError(ex, "Error fetching characters");
       }
    }
}