using CS2Players.Application.Interfaces;
using CS2Players.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CS2Players.Presentation.Controllers
{
    public class JogadorController : Controller
    {
        private readonly IJogadorService _jogadorService;
        private readonly ITimeService _timeService;

        public JogadorController(IJogadorService jogadorService, ITimeService timeService)
        {
            _jogadorService = jogadorService;
            _timeService = timeService;
        }

        public async Task<IActionResult> Index()
        {
            var jogadores = await _jogadorService.GetAllAsync();
            return View(jogadores);
        }


        public async Task<IActionResult> Details(int id)
        {
            var jogador = await _jogadorService.GetByIdAsync(id);
            if (jogador == null)
            {
                return NotFound();
            }
            return View(jogador);
        }


        public async Task<IActionResult> Create()
        {
            await PopulateTimesDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JogadorViewModel jogadorViewModel)
        {
            if (ModelState.IsValid)
            {
                await _jogadorService.AddAsync(jogadorViewModel);
                TempData["Success"] = "Jogador cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            await PopulateTimesDropdown(jogadorViewModel.TimeId);
            return View(jogadorViewModel);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var jogador = await _jogadorService.GetByIdAsync(id);
            if (jogador == null)
            {
                return NotFound();
            }
            await PopulateTimesDropdown(jogador.TimeId);
            return View(jogador);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JogadorViewModel jogadorViewModel)
        {
            if (id != jogadorViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _jogadorService.UpdateAsync(jogadorViewModel);
                TempData["Success"] = "Jogador atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            await PopulateTimesDropdown(jogadorViewModel.TimeId);
            return View(jogadorViewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var jogador = await _jogadorService.GetByIdAsync(id);
            if (jogador == null)
            {
                return NotFound();
            }
            return View(jogador);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _jogadorService.DeleteAsync(id);
            TempData["Success"] = "Jogador excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Search(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                var allJogadores = await _jogadorService.GetAllAsync();
                return PartialView("_JogadorListPartial", allJogadores);
            }

            var jogadores = await _jogadorService.SearchAsync(termo);
            return PartialView("_JogadorListPartial", jogadores);
        }

        private async Task PopulateTimesDropdown(int? selectedTimeId = null)
        {
            var times = await _timeService.GetAllAsync();
            ViewBag.Times = new SelectList(times, "Id", "Nome", selectedTimeId);
        }
    }
}