using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace RickAndMorty.Web.Pages;

public class Add : PageModel
{
    private readonly IMediator _mediator;
    private readonly ILogger<Add> _logger;

    public Add(IMediator mediator, ILogger<Add> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public void OnGet()
    {
        
    }
    public void OnPost()
    {
        
    }
}