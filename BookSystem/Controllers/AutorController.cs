using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookSystem.Data;
using BookSystem.Models;
using BookSystem.Interfaces;

namespace BookSystem.Controllers
{
    public class AutorController : Controller
    {
        private readonly IAutorRepository _autorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AutorController(IAutorRepository autorRepository, IUnitOfWork unitOfWork)
        {
            _autorRepository = autorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _autorRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _autorRepository.GetByIdAsync(id);
  
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Nacionalidade")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                await _autorRepository.AddAsync(autor);
               await _unitOfWork.CommitAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _autorRepository.GetByIdAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Nacionalidade")] Autor autor)
        {
            if (id != autor.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _autorRepository.Update(autor);
                    _unitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_autorRepository.AutorExists(autor.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _autorRepository.GetByIdAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autor = await _autorRepository.GetByIdAsync(id);
            if (autor != null)
            {
                _autorRepository.Delete(id);
            }
            
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
