using System;
using System.Linq;
using WebApi.DbOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x=>x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Güncellenecek kitap bulunamadı!");

            if(_context.Authors.Any(x=>x.Name.ToLower() == Model.Name.ToLower() && x.Surname == Model.Surname.ToLower() && x.Id !=AuthorId))
                throw new InvalidOperationException("Aynı kitap ismi zaten mevcut!");

            author.Name = Model.Name.Trim() != default ? Model.Name : author.Name;
            author.Surname = Model.Surname != default ? Model.Surname : author.Surname;
            author.BirthDate = Model.BirthDate != default ? Model.BirthDate : author.BirthDate;
            _context.SaveChanges();
        }
    }
    public class UpdateAuthorModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }    
    }

}
