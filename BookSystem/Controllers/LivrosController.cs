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
using BookSystem.Interfaces;

namespace BookSystem.Controllers
{
    public class LivrosController : Controller
    {
        private readonly ILivroRepository _livroRepostory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAutorRepository _autorRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public LivrosController
            (
            ILivroRepository livroRepostory ,IUnitOfWork unitOfWork,
            IAutorRepository autorRepository, ICategoriaRepository categoriaRepository
            )
        {
            _livroRepostory = livroRepostory;
            _unitOfWork = unitOfWork;
            _autorRepository = autorRepository;
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _livroRepostory.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroRepostory.GetByIdAsync(id);
            
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        public async Task<IActionResult> Create()
        {
            CreateLivroViewModel createLivroViewModel = new CreateLivroViewModel()
            {
                Autores = new SelectList(await _autorRepository.GetAutorByNameList()),
                Categorias = new SelectList(await _categoriaRepository.GetCatByNameList()),
            };

            return View(createLivroViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLivroViewModel createLivroViewModel)
        {

            if (ModelState.IsValid)
            {
                var findAutor = await _autorRepository.GetByNameAsync(createLivroViewModel.AutorEscolhido);
                var findCategoria = await _categoriaRepository.GetByNameAsync(createLivroViewModel.CategoriaEscolhida);

                if (findAutor != null && findCategoria != null)
                {
                    if (findAutor.Nome != "Sem Autor" && findCategoria.Nome != "Sem Categoria")
                    {
                        var livro = new Livro()
                        {
                            Titulo = createLivroViewModel.Livro.Titulo,
                            Preco = createLivroViewModel.Livro.Preco,
                            Quantidade = createLivroViewModel.Livro.Quantidade,
                            Autor = findAutor,
                            Categoria = findCategoria,
                        };
                        await _livroRepostory.AddAsync(livro);
                        await _unitOfWork.CommitAsync();
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
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroRepostory.GetByIdAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
      
            EditLivroViewModel editLivroViewModel = new EditLivroViewModel()
            {
                Livro = livro,
                Autores = new SelectList(await _autorRepository.GetAutorByNameList()),
                Categorias = new SelectList(await _categoriaRepository.GetCatByNameList())
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
                    var autorEscolhido = await _autorRepository.GetByNameAsync(editLivroViewModel.AutorEscolhido);
                    var categoriaEscolhida = await _categoriaRepository.GetByNameAsync(editLivroViewModel.CategoriaEscolhida);
                     
                    var livro = new Livro()
                    {
                        Titulo = editLivroViewModel.Livro.Titulo,
                        Preco = editLivroViewModel.Livro.Preco,
                        Quantidade = editLivroViewModel.Livro.Quantidade,
                        Autor = autorEscolhido,
                        Categoria = categoriaEscolhida
                    };

                    _livroRepostory.Update(livro);
                    await _unitOfWork.CommitAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_livroRepostory.bookExists(editLivroViewModel.Livro.ID))
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
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _livroRepostory.GetByIdAsync(id);
           
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
            var livro = _livroRepostory.GetByIdAsync(id);
            if (livro != null)
            {
               await _livroRepostory.DeleteAsync(id);
            }
            
            await _unitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
