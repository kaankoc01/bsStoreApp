using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Ef_Core
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> filterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>
            books.Where(book => (book.Price >= minPrice && book.Price <= maxPrice));
        

        
    }
}
