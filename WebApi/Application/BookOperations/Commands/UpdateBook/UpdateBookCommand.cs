using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _context;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        public UpdateBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x=>x.Id == BookId);
            if (book is null)
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı!");
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            //book.PageCount = Model.PageCount != default ? Model.PageCount : book.PageCount;  (korumalı olsun)
            //book.PublishDate = Model.PublishDate != default ? Model.PublishDate : book.PublishDate; (korumalı olsun)
            book.Title = Model.Title != default ? Model.Title : book.Title;
            _context.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }        
    }

}
