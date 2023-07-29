using CardManager.Web.Models.Dtos;
using CardManager.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace CardManager.Web.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        public IActionResult CreateCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCard(CardDto newCardData)
        {
            if (ModelState.IsValid)
            {
                var success = await _cardService.CreateNewCardAsync(newCardData);
                if (success)
                {
                    // Cartão criado com sucesso
                    return RedirectToAction("CreateCard");
                }
                else
                {
                    // Trate o erro, exiba uma mensagem de erro ou redirecione para uma página de erro
                    return View();
                }
            }

            // Se os dados não forem válidos, retorne a mesma view para mostrar as mensagens de validação
            return View();
        }

        public IActionResult GetCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCard(string cardSerial)
        {
            if (!string.IsNullOrEmpty(cardSerial))
            {
                var card = await _cardService.GetCardBySerialAsync(cardSerial);
                if (card != null)
                {
                    // Cartão encontrado, faça algo com os dados do cartão, como exibir na view ou redirecionar para outra página
                    return RedirectToAction("GetCard");
                }
                else
                {
                    // Cartão não encontrado
                    ModelState.AddModelError(string.Empty, "Cartão não encontrado.");
                }
            }

            // Se o número do cartão não foi fornecido ou não foi encontrado, retorne a mesma view para mostrar as mensagens de erro
            return View();
        }

        public IActionResult GenerateFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateFile(IFormFile csvFile)
        {
            if (csvFile != null && csvFile.Length > 0)
            {
                var success = await _cardService.UpdateDatabaseAsync(csvFile);
                if (success)
                {
                    // Base de dados atualizada com sucesso
                    return RedirectToAction("GenerateFile");
                }
                else
                {
                    // Trate o erro, exiba uma mensagem de erro ou redirecione para uma página de erro
                    return View();
                }
            }

            // Se o arquivo não foi fornecido, retorne a mesma view para mostrar as mensagens de erro
            return View();
        }

        public IActionResult SeedDatabase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SeedDatabase(IFormFile file)
        {
            var success = await _cardService.UpdateDatabaseAsync(file);
            if (success)
            {
                // Base de dados semeada com sucesso
                return RedirectToAction("SeedDatabase");
            }
            else
            {
                // Trate o erro, exiba uma mensagem de erro ou redirecione para uma página de erro
                return View();
            }
        }

        public IActionResult DeleteCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCard(string cardSerial)
        {
            if (!string.IsNullOrEmpty(cardSerial))
            {
                var success = await _cardService.DeleteCardAsync(cardSerial);
                if (success)
                {
                    // Cartão apagado com sucesso
                    return RedirectToAction("DeleteCard");
                }
                else
                {
                    // Trate o erro, exiba uma mensagem de erro ou redirecione para uma página de erro
                    return View();
                }
            }

            // Se o número do cartão não foi fornecido, retorne a mesma view para mostrar as mensagens de erro
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCardBase()
        {
            var success = await _cardService.DeleteAllCardsAsync();
            if (success)
            {
                // Base de dados de cartões apagada com sucesso
                return RedirectToAction("DeleteCardBase");
            }
            else
            {
                // Trate o erro, exiba uma mensagem de erro ou redirecione para uma página de erro
                return View();
            }
        }
    }
}
