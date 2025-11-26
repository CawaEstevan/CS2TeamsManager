using CS2Players.Application.Interfaces;
using CS2Players.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CS2Players.Web.Controllers
{
    public class JogadoresController : Controller
    {
        private readonly IJogadorService _jogadorService;
        private readonly ITimeService _timeService;

        public JogadoresController(IJogadorService jogadorService, ITimeService timeService)
        {
            _jogadorService = jogadorService;
            _timeService = timeService;
        }

        // GET: Jogadores
        public async Task<IActionResult> Index()
        {
            var jogadores = await _jogadorService.GetAllAsync();
            return View(jogadores);
        }

        // GET: Jogadores/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var jogador = await _jogadorService.GetByIdAsync(id);
            
            if (jogador == null)
                return NotFound();

            return View(jogador);
        }

        // GET: Jogadores/Create
        public async Task<IActionResult> Create()
        {
            await PopulateTimesDropdown();
            return View();
        }

        // POST: Jogadores/Create
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

        // GET: Jogadores/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var jogador = await _jogadorService.GetByIdAsync(id);
            
            if (jogador == null)
                return NotFound();

            await PopulateTimesDropdown(jogador.TimeId);
            return View(jogador);
        }

        // POST: Jogadores/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, JogadorViewModel jogadorViewModel)
        {
            if (id != jogadorViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _jogadorService.UpdateAsync(jogadorViewModel);
                TempData["Success"] = "Jogador atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            
            await PopulateTimesDropdown(jogadorViewModel.TimeId);
            return View(jogadorViewModel);
        }

        // GET: Jogadores/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var jogador = await _jogadorService.GetByIdAsync(id);
            
            if (jogador == null)
                return NotFound();

            return View(jogador);
        }

        // POST: Jogadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _jogadorService.DeleteAsync(id);
            TempData["Success"] = "Jogador excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Jogadores/Search?termo=...
        [HttpGet]
        public async Task<IActionResult> Search(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
                return Json(new List<JogadorViewModel>());

            var jogadores = await _jogadorService.SearchAsync(termo);
            return Json(jogadores);
        }

        private async Task PopulateTimesDropdown(int? selectedTimeId = null)
        {
            var times = await _timeService.GetAllAsync();
            ViewBag.Times = new SelectList(times, "Id", "Nome", selectedTimeId);
        }
    }
}