using RestWithAspNet.Data.Converters;
using RestWithAspNet.Data.VO;
using RestWithAspNet.Model;
using RestWithAspNet.Repository.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithAspNet.Business.Implementations
{
    public class BookBusinessImpl : IBookBusiness
    {
        private IRepository<Book> _repository;
        private readonly BookConverter _bookConverter;

        public BookBusinessImpl(IRepository<Book> repository)
        {
            _repository = repository;
            _bookConverter = new BookConverter();
        }

        public BookVO Create(BookVO book)
        {
            var bookEntity = _bookConverter.Parse(book);
            return _bookConverter.Parse(_repository.Create(bookEntity));
        }

        public BookVO FindById(long id)
        {
            return _bookConverter.Parse(_repository.FindById(id));
        }

        public BookVO Update(BookVO book)
        {
            var bookEntity = _bookConverter.Parse(book);
            return _bookConverter.Parse(_repository.Update(bookEntity));
        }

        public List<BookVO> FindAll()
        {
            return _bookConverter.ParseList(_repository.FindAll());
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }
    }
}
