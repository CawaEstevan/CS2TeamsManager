using CS2Players.Application.Interfaces;
using CS2Players.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CS2Players.Web.Controllers
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
        public async Task<IActionResult> Details(int id)
        {
            var time = await _timeService.GetByIdWithJogadoresAsync(id);
            
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
                await _timeService.AddAsync(timeViewModel);
                TempData["Success"] = "Time criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            
            return View(timeViewModel);
        }

        // GET: Times/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var time = await _timeService.GetByIdAsync(id);
            
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
                await _timeService.UpdateAsync(timeViewModel);
                TempData["Success"] = "Time atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            
            return View(timeViewModel);
        }

        // GET: Times/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var time = await _timeService.GetByIdAsync(id);
            
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

        // GET: Times/Search?termo=...
        [HttpGet]
        public async Task<IActionResult> Search(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
                return Json(new List<TimeViewModel>());

            var times = await _timeService.SearchAsync(termo);
            return Json(times);
        }
    }
}