﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebIdentity.Entities;
using MvcWecIdentity.Context;

namespace MvcWebIdentity.Controllers
{
    [Authorize]
	public class ProdutosController : Controller
	{
		private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return _context.Produtos != null ?
                View(await _context.Produtos.ToListAsync()) :
                Problem("Entity set AppDbContext.Produtos é null.");
        }

        [Authorize(Policy = "IsFuncionarioClaimAccess")]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null || _context.Produtos == null) 
                return NotFound();

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);

            if(produto == null)
                return NotFound();

            return View(produto);
        }

		[Authorize(Policy = "IsFuncionarioClaimAccess")]
		public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdutoId,Nome,Preco")] Produto produto)
        {
            if(ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

		[Authorize(Policy = "IsAdminClaimAccess")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Produtos == null)
                return NotFound();

            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
                return NotFound();

            return View(produto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId, Nome, Preco")] Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.ProdutoId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

		[Authorize(Policy = "IsAdminClaimAccess")]
		public async Task<IActionResult> Delete(int? id)
        {
            if(id == null || _context.Produtos == null)
                return NotFound();

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.ProdutoId == id);

            if(produto == null) 
                return NotFound();

            return View(produto);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
		[Authorize(Policy = "IsAdminClaimAccess")]
		public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_context.Produtos == null)
                return BadRequest();

            var produto = await _context.Produtos.FirstOrDefaultAsync(m => m.ProdutoId == id);

            if (produto != null)
            {
                _context.Remove(produto);
                await _context.SaveChangesAsync();
            }
            else
                return NotFound();

            return View();
        }

		private bool ProdutoExists(int produtoId)
		{
			throw new NotImplementedException();
		}
	}
}
