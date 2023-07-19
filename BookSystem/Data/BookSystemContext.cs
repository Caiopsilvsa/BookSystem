using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookSystem.Models;

namespace BookSystem.Data
{
    public class BookSystemContext : DbContext
    {
        public BookSystemContext (DbContextOptions<BookSystemContext> options)
            : base(options)
        {
        }

        public DbSet<BookSystem.Models.Livro> Livro { get; set; } = default!;

        public DbSet<BookSystem.Models.Autor>? Autor { get; set; }

        public DbSet<BookSystem.Models.Categoria>? Categoria { get; set; }
    }
}
