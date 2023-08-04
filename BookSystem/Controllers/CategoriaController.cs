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
    public class CategoriaController : Controller
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriaController(ICategoriaRepository categoriaRepository, IUnitOfWork unitOfWork)
        {
            _categoriaRepository = categoriaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _categoriaRepository.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome,Descricao")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.Add(categoria);
                await _unitOfWork.CommitAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaRepository.GetByIdAsync(id);
            
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome,Descricao")] Categoria categoria)
        {
            if (id != categoria.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _categoriaRepository.Update(categoria);
                    await _unitOfWork.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_categoriaRepository.bookExists(categoria.ID))
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
            return View(categoria);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = await _categoriaRepository.GetByIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(id);
            if (categoria != null)
            {
                _categoriaRepository.Delete(id);
            }
            
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
