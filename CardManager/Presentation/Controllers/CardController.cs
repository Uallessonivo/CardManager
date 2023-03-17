using CardManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CardManager.Presentation.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }
    }
}
