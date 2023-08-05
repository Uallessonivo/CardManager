using CardManager.Web.Models.Dtos;
using CardManager.Web.Services.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                ResponseDto response = await _cardService.CreateNewCardAsync(newCardData);
                if (response.IsSuccess)
                {
                    TempData["success"] = "Cartão criado com sucesso!";
                    return RedirectToAction("CreateCard");
                }
            }

            TempData["error"] = "Verifique os dados do cartão e tente novamente.";
            return View();
        }

        public async Task<IActionResult> GetAllCards()
        {
            List<CardDto>? list = new();
            ResponseDto response = await _cardService.GetAllCardsAsync();

            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CardDto>>(Convert.ToString(response.Result)!);
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(list);
        }

        public IActionResult GenerateFile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GenerateFile(string cardType)
        {
            if (!string.IsNullOrEmpty(cardType))
            {
                ResponseDto response = await _cardService.GenerateCsvReport(cardType);
                // TODO

            }

            return View();
        }

        public IActionResult SeedDatabase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SeedDatabase(IFormFile file)
        {
            ResponseDto response = await _cardService.UpdateDatabaseAsync(file);
            if (response.IsSuccess)
            {
                TempData["success"] = "A base de dados foi atualizada com sucesso!";
                return RedirectToAction("SeedDatabase");
            }

            TempData["error"] = "Verifique o arquivo e tente novamente.";
            return View();
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
                ResponseDto response = await _cardService.GetCardAsync(cardSerial);
                if (response.Result != null)
                {
                    return View("DeleteCardConfirmation", response.Result);
                }

                TempData["error"] = "Cartão não encontrado.";
                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCardConfirmation(string cardSerial)
        {
            if (!string.IsNullOrEmpty(cardSerial))
            {
                ResponseDto response = await _cardService.DeleteCardAsync(cardSerial);
                if (response.IsSuccess)
                {
                    TempData["success"] = "Cartão apagado com sucesso!";
                    return RedirectToAction("DeleteCard");
                }
            }

            return View();
        }

        public IActionResult DeleteCardBase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCardBaseConfirmation()
        {
            ResponseDto response = await _cardService.DeleteAllCardsAsync();
            if (response.IsSuccess)
            {
                TempData["success"] = "A base de cartões foi apagada com sucesso";
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Não foi possível apagar a base de cartães.";
            return View("DeleteCardBase");
        }
    }
}