using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Repositories.Ef_Core.Extensions;


namespace Repositories.Extensions
{
    public static class BookRepositoryExtensions
    {
        public static IQueryable<Book> filterBooks(this IQueryable<Book> books, uint minPrice, uint maxPrice) =>
            books.Where(book => book.Price >= minPrice && book.Price <= maxPrice);


        public static IQueryable<Book> Search(this IQueryable<Book> books, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return books;

            var lowerCaseTerm = searchTerm.Trim().ToLower(); // KaRa , kara olcak
            return books.Where(b => b.Title.ToLower().Contains(searchTerm));
        }

        public static IQueryable<Book> Sort(this IQueryable<Book> books, string orderByQueryString)
        {
            if(string.IsNullOrWhiteSpace(orderByQueryString))
                return books.OrderBy(b => b.Id);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Book>(orderByQueryString);

            if (orderQuery is null)
                return books.OrderBy(b => b.Id);

            return books.OrderBy(orderQuery);

        }
    }
}
