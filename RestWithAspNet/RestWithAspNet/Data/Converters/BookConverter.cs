using RestWithAspNet.Data.Converter;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Data.Converters
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin != null)
            {
                return new Book
                {
                    Id = origin.Id,
                    Author = origin.Author,
                    LaunchDate = origin.LaunchDate,
                    Price = origin.Price,
                    Title = origin.Title
                };
            }
            return null;
        }

        public BookVO Parse(Book origin)
        {
            if (origin != null)
            {
                return new BookVO
                {
                    Id = origin.Id,
                    Author = origin.Author,
                    LaunchDate = origin.LaunchDate,
                    Price = origin.Price,
                    Title = origin.Title
                };
            }

            return null;
        }

        public List<Book> ParseList(List<BookVO> origins)
        {
            if(origins != null && origins.Any())
            {
                return origins.Select(item => Parse(item)).ToList();
            }

            return null;
        }

        public List<BookVO> ParseList(List<Book> origins)
        {
            if (origins != null && origins.Any())
            {
                return origins.Select(item => Parse(item)).ToList();
            }

            return null;
        }
    }
}
