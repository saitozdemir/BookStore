using System;
using WebApi.DbOperations;
using WebApi.Entities;

namespace TestsSetup
{
    public static class Authors{
        public static void AddAuthors(this BookStoreDbContext context){
            context.Authors.AddRange(
                    new Author{
                        Name="Eric",
                        Surname="Ries",
                        BirthDate=new DateTime(1978,10,22)
                    },
                    new Author{
                        Name="Charlotte",
                        Surname="Gilman",
                        BirthDate=new DateTime(1870,03,07)
                    },
                    new Author{
                        Name="Frank",
                        Surname="Herbet",
                        BirthDate=new DateTime(1920,11,08)
                    }
                );
        }
    }
}