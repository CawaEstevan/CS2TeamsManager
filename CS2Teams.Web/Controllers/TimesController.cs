using Microsoft.AspNetCore.Mvc;
using CS2Teams.Application.Interfaces;
using CS2Teams.Application.ViewModels;

namespace CS2Teams.Web.Controllers
{
    public class TimesController : Controller
    {
        private readonly ITimeService _timeService;

        public TimesController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        // GET: Times
        public async Task<IActionResult> Index()
        {
            var times = await _timeService.GetAllAsync();
            return View(times);
        }

        // GET: Times/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var time = await _timeService.GetByIdWithJogadoresAsync(id.Value);
            if (time == null)
                return NotFound();

            return View(time);
        }

        // GET: Times/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Times/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimeViewModel timeViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _timeService.AddAsync(timeViewModel);
                if (result)
                {
                    TempData["Success"] = "Time criado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Tag", "Já existe um time com essa tag.");
            }
            return View(timeViewModel);
        }

        // GET: Times/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var time = await _timeService.GetByIdAsync(id.Value);
            if (time == null)
                return NotFound();

            return View(time);
        }

        // POST: Times/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TimeViewModel timeViewModel)
        {
            if (id != timeViewModel.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var result = await _timeService.UpdateAsync(timeViewModel);
                if (result)
                {
                    TempData["Success"] = "Time atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Tag", "Já existe outro time com essa tag.");
            }
            return View(timeViewModel);
        }

        // GET: Times/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var time = await _timeService.GetByIdWithJogadoresAsync(id.Value);
            if (time == null)
                return NotFound();

            return View(time);
        }

        // POST: Times/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _timeService.DeleteAsync(id);
            TempData["Success"] = "Time excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Times/Search?searchTerm=navi
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var times = await _timeService.SearchAsync(searchTerm);
            return PartialView("_TimesListPartial", times);
        }
    }
}
