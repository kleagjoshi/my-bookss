using my_bookss.Data.Models;
using my_bookss.Data;
using my_bookss.Data.ViewModels;

namespace my_bookss.Data.Services

{
    public class BooksService
    {
        //communicates with db

        private AppDbContext _context;
        public BooksService(AppDbContext context)
        {

            _context = context;

        }

        //add a book
        public void AddBookWithAuthors(BookVM book)
        {
            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                DateAdded = DateTime.Now,
                PublisherId = book.PublisherId

            };
            _context.Books.Add(_book);
            _context.SaveChanges();

            //add relations of book and book author to bookauthor tables

            foreach (var id in book.AuthorIds)
            {
                var _book_author = new Book_Author()
                {
                    BookId = _book.Id,
                    AuthorId = id
                };

                _context.Books_Authors.Add(_book_author);
                _context.SaveChanges();
            }
        }

        //get list of books
        public List<Book> GetAllBooks()
        {
            var allbooks = _context.Books.ToList();
            return allbooks;
        }

        //get a single book by id

        //public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(n => n.Id == bookId);

        public BookWithAuthorsVM GetBookById(int bookId)
        {
            var _bookWithAuthors = _context.Books.Where(n => n.Id == bookId).Select(book => new BookWithAuthorsVM()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead.Value : null,
                Rate = book.IsRead ? book.Rate.Value : null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.Publisher.Name,
                AuthorNames = book.Book_Authors.Select(n => n.Author.FullName).ToList()
            }).FirstOrDefault();

            return _bookWithAuthors;
        }
        //update existing data

        public Book UpdateBookById(int bookId, BookVM book)
        {
            //check if this book exist
            var _book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead.Value : null;
                _book.Rate = book.IsRead ? book.Rate.Value : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;

                _context.SaveChanges();
            }

            return _book;

        }

        //delete book by id

        public void DeleteBookById(int bookId)
        {
            //check if the book exists
            var book = _context.Books.FirstOrDefault(n => n.Id == bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
    }
}

