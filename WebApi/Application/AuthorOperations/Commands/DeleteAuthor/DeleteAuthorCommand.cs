using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _dbcontext;
        public int AuthorId { get; set; }

        public DeleteAuthorCommand(IBookStoreDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public void Handle()
        {
            var author = _dbcontext.Authors.Include(x=>x.Books).SingleOrDefault(x=>x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Silinecek kitap bulunamadı!");
           
            if(author.Books.Any())
                throw new InvalidOperationException("Yazarın kitapları mevcut, lütfen önce kitapları siliniz.");

            _dbcontext.Authors.Remove(author);
            _dbcontext.SaveChanges();
        }
    }
    
}