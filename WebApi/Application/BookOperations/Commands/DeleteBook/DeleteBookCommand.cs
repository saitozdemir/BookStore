using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly IBookStoreDbContext _dbcontext;
        public int BookId { get; set; }

        public DeleteBookCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Handle()
        {
            var book = _dbcontext.Books.SingleOrDefault(x=>x.Id == BookId);
            if(book is null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı!");
            _dbcontext.Books.Remove(book);
            _dbcontext.SaveChanges();
        }
    }
    
}