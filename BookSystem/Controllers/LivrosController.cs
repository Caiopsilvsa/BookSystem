using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookSystem.Data;
using BookSystem.Models;
using BookSystem.ViewModels;

namespace BookSystem.Controllers
{
    public class LivrosController : Controller
    {
        private readonly BookSystemContext _context;

        public LivrosController(BookSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookSystemContext = _context.Livro.Include(l => l.Autor).Include(l => l.Categoria);
            return View(await bookSystemContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        public async Task<IActionResult> Create()
        {
            IQueryable<string> autores = from a in _context.Autor
                        select a.Nome;

            IQueryable<string> categorias = from g in _context.Categoria
                                            select g.Nome;

            CreateLivroViewModel createLivroViewModel = new CreateLivroViewModel()
            {
                Autores = new SelectList(await autores.Distinct().ToListAsync()),
                Categorias = new SelectList(await categorias.Distinct().ToListAsync()),
            };

            return View(createLivroViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLivroViewModel createLivroViewModel)
        {

            if (ModelState.IsValid)
            {
                var findAutor = await _context.Autor.
                    Where(a => a.Nome == createLivroViewModel.AutorEscolhido).FirstOrDefaultAsync();

                var findCategoria =  await _context.Categoria.
                    Where(c => c.Nome == createLivroViewModel.CategoriaEscolhida).FirstOrDefaultAsync();

                if (findAutor != null && findCategoria != null)
                {
                    if (findAutor.Nome != "Sem Autor" && findCategoria.Nome != "Sem Categoria")
                    {
                        var Livro = new Livro()
                        {
                            Titulo = createLivroViewModel.Livro.Titulo,
                            Preco = createLivroViewModel.Livro.Preco,
                            Autor = findAutor,
                            Categoria = findCategoria,
                        };
                        await _context.Livro.AddAsync(Livro);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }                  
                }
                else
                {
                    ModelState.AddModelError("", "Preencha todos os campos!");
                    return View(createLivroViewModel);
                }
               
            }
            return View(createLivroViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            IQueryable<string> autores = from a in _context.Autor
                                         select a.Nome;

            IQueryable<string> categorias = from g in _context.Categoria
                                            select g.Nome;
            EditLivroViewModel editLivroViewModel = new EditLivroViewModel()
            {
                Livro = livro,
                Autores = new SelectList(await autores.Distinct().ToListAsync()),
                Categorias = new SelectList(await categorias.Distinct().ToListAsync()),
            };
            return View(editLivroViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditLivroViewModel editLivroViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var autorEscolhido = await _context.Autor.
                        Where(a => a.Nome == editLivroViewModel.AutorEscolhido).FirstOrDefaultAsync();

                    var categoriaEscolhida = await _context.Categoria.
                        Where(c => c.Nome == editLivroViewModel.CategoriaEscolhida).FirstOrDefaultAsync();

                    var livro = new Livro()
                    {
                        Titulo = editLivroViewModel.Livro.Titulo,
                        Preco = editLivroViewModel.Livro.Preco,
                        Autor = autorEscolhido,
                        Categoria = categoriaEscolhida
                    };

                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(editLivroViewModel.Livro.ID))
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
            return View();
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Livro == null)
            {
                return Problem("Entity set 'BookSystemContext.Livro'  is null.");
            }
            var livro = await _context.Livro.FindAsync(id);
            if (livro != null)
            {
                _context.Livro.Remove(livro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
          return (_context.Livro?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
