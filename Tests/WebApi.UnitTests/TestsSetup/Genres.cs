using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestsSetup
{
    public static class Genres{
        public static void AddGenres(this BookStoreDbContext context){
            context.Genres.AddRange(
                    new Genre{Name = "Personal Growth"},
                    new Genre{Name = "Science Fiction"},
                    new Genre{Name = "Romance"}
                );
        }
    }
}