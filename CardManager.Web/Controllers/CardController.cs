﻿using CardManager.Web.Models.Dtos;
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
                    TempData["success"] = "Cartão criado com sucesso!";
                    return RedirectToAction("CreateCard");
                }
                else
                {
                    TempData["error"] = "Verifique os dados do cartão e tente novamente.";
                    return View();
                }
            }

            return View();
        }

        public IActionResult GetCard()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCard(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                var card = await _cardService.GetCardAsync(filter);
                if (card != null)
                {
                    return View(card);
                }
            }

            return View();
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
                var result = await _cardService.GenerateReport(cardType);

                TempData["success"] = "Arquivo gerado com sucesso!";

                return View(result);
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
            var success = await _cardService.UpdateDatabaseAsync(file);
            if (success)
            {
                TempData["success"] = "A base de dados foi atualizada com sucesso!";
                return RedirectToAction("SeedDatabase");
            }
            else
            {
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
                    TempData["success"] = "O cartão foi apagado da base com sucesso";
                    return RedirectToAction("DeleteCard");
                }
                else
                {
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
                TempData["success"] = "A base de cartões foi apagada com sucesso";
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