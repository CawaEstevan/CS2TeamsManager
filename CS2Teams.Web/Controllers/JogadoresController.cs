using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CS2Teams.Application.Interfaces;
using CS2Teams.Application.ViewModels;
using CS2Teams.Domain.Enums;

namespace CS2Teams.Web.Controllers
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _jogadorService.GetByIdAsync(id.Value);
            if (jogador == null)
            {
                return NotFound();
            }

            return View(jogador);
        }

        // GET: Jogadores/Create
        public async Task<IActionResult> Create()
        {
            await PopulateTimesDropDown();
            return View();
        }

        // POST: Jogadores/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(JogadorViewModel jogadorViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _jogadorService.AddAsync(jogadorViewModel);
                if (result)
                {
                    TempData["Success"] = "Jogador criado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                
                ModelState.AddModelError("Nickname", "Já existe um jogador com esse nickname ou o time não existe.");
            }
            
            await PopulateTimesDropDown(jogadorViewModel.TimeId);
            return View(jogadorViewModel);
        }

        // GET: Jogadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _jogadorService.GetByIdAsync(id.Value);
            if (jogador == null)
            {
                return NotFound();
            }
            
            await PopulateTimesDropDown(jogador.TimeId);
            return View(jogador);
        }

        // POST: Jogadores/Edit/5
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
                var result = await _jogadorService.UpdateAsync(jogadorViewModel);
                if (result)
                {
                    TempData["Success"] = "Jogador atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                
                ModelState.AddModelError("Nickname", "Já existe outro jogador com esse nickname ou o time não existe.");
            }
            
            await PopulateTimesDropDown(jogadorViewModel.TimeId);
            return View(jogadorViewModel);
        }

        // GET: Jogadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jogador = await _jogadorService.GetByIdAsync(id.Value);
            if (jogador == null)
            {
                return NotFound();
            }

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

        // GET: Jogadores/Search?searchTerm=s1mple
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var jogadores = await _jogadorService.SearchAsync(searchTerm);
            return PartialView("_JogadoresListPartial", jogadores);
        }

        private async Task PopulateTimesDropDown(object? selectedTime = null)
        {
            var times = await _timeService.GetAllAsync();
            ViewBag.Times = new SelectList(times, "Id", "Nome", selectedTime);
            ViewBag.Roles = new SelectList(Enum.GetValues(typeof(PlayerRole)));
        }
    }
}