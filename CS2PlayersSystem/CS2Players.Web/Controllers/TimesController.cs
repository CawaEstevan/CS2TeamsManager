using CS2Players.Application.Interfaces;
using CS2Players.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CS2Players.Presentation.Controllers
{
    public class TimeController : Controller
    {
        private readonly ITimeService _timeService;

        public TimeController(ITimeService timeService)
        {
            _timeService = timeService;
        }


        public async Task<IActionResult> Index()
        {
            var times = await _timeService.GetAllAsync();
            return View(times);
        }


        public async Task<IActionResult> Details(int id)
        {
            var time = await _timeService.GetByIdWithJogadoresAsync(id);
            if (time == null)
            {
                return NotFound();
            }
            return View(time);
        }


        public IActionResult Create()
        {
            return View();
        }

   
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TimeViewModel timeViewModel)
        {
            if (ModelState.IsValid)
            {
                await _timeService.AddAsync(timeViewModel);
                TempData["Success"] = "Time cadastrado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(timeViewModel);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var time = await _timeService.GetByIdAsync(id);
            if (time == null)
            {
                return NotFound();
            }
            return View(time);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TimeViewModel timeViewModel)
        {
            if (id != timeViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _timeService.UpdateAsync(timeViewModel);
                TempData["Success"] = "Time atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(timeViewModel);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var time = await _timeService.GetByIdAsync(id);
            if (time == null)
            {
                return NotFound();
            }
            return View(time);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _timeService.DeleteAsync(id);
            TempData["Success"] = "Time excluído com sucesso!";
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Search(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                var allTimes = await _timeService.GetAllAsync();
                return PartialView("_TimeListPartial", allTimes);
            }

            var times = await _timeService.SearchAsync(termo);
            return PartialView("_TimeListPartial", times);
        }
    }
}