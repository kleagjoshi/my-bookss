using my_bookss.Data.Models;
using my_bookss.Data.ViewModels;
using my_bookss.Exceptions;
using System.Text.RegularExpressions;

namespace my_bookss.Data.Services
{
    public class PublishersService
    {
        AppDbContext _context;
        public PublishersService(AppDbContext context) {
        _context = context;
        }

        public List<Publisher> GetAllPublishers(string sortBy,string searchString)
        {
            var allPublishers = _context.Publishers.OrderBy(n=>n.Name).ToList(); //ascending order

            if(!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "name_desc":
                        allPublishers = allPublishers.OrderByDescending(n => n.Name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if(!string.IsNullOrEmpty(searchString)) 
            { 
                allPublishers = allPublishers.Where(n=>n.Name.Contains(searchString,StringComparison.CurrentCultureIgnoreCase)).ToList();
            }
            return allPublishers;
        }
        public Publisher AddPublisher(PublisherVM publisher)
        {
            if (StringStartWithNumber(publisher.Name)) throw new PublisherNameException("Name starts with number", publisher.Name);
            var _publisher = new Publisher()
            {
                Name = publisher.Name
            };

            _context.Publishers.Add(_publisher); //add to db
            _context.SaveChanges();
            return _publisher;
        }
        public Publisher GetPublisherById(int id)
        {
            return _context.Publishers.FirstOrDefault(n => n.Id == id);
        }
        //
        public PublisherWithBooksAndAuthorsVM GetPublisherData(int publisherId)
        {
            var _publisherData = _context.Publishers.Where(n => n.Id == publisherId).Select(n => new PublisherWithBooksAndAuthorsVM()
            {

                Name = n.Name,
                BookAuthors = n.Books.Select(n => new BookAuthorVM()
                {
                    BookName = n.Title,
                    BookAuthors = n.Book_Authors.Select(n => n.Author.FullName).ToList()

                }).ToList()

            }).FirstOrDefault();

            return _publisherData;
        }

        public void DeletePublisherById(int id)
        {
            var _publisher = _context.Publishers.FirstOrDefault(n => n.Id == id);
            if(_publisher != null )
            {
                _context.Publishers.Remove(_publisher);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception($"The publisher with id {id} does not exist");
            }
        }

        private bool StringStartWithNumber(string name)
        {
            if (Regex.IsMatch(name, @"^\d")) return true;
            return false;
        }
    }
}
