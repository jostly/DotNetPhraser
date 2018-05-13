using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Phraser.Models;
using Phraser.Services;

namespace Phraser.Controllers
{
	[Route("/")]
	public class PhraseController : Controller
	{
		readonly IPhraseService phraseService;
		public PhraseController(IPhraseService phraseService)
		{
			this.phraseService = phraseService;
		}

		[HttpGet]
		public IActionResult GeneratePhrase()
		{			
			return Wrap(phraseService.AlliterativePhrase());
		}

		[Route("{initial:regex(^[[a-wyz]]$)}")]
		[HttpGet]
		public IActionResult GeneratePhraseStartingWith(string initial)
		{
			return Wrap(phraseService.PhraseStartingWith(initial));
		}

        [Route("random")]
        [HttpGet]
        public IActionResult GenerateRandomPhrase()
		{
			return Wrap(phraseService.RandomPhrase());
		}

		IActionResult Wrap(Phrase model)
		{
			if (HtmlRequested())
			{
				return View("Phrase", model);
			}
			else
			{
				return Ok(model);
			}
		}

        bool HtmlRequested()
		{
			string accept = Request.Headers["Accept"];
			return (accept != null) && (accept.Contains("text/html"));
		}
	}
}
