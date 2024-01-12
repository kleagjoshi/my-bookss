using my_bookss.Data.Enum;

namespace my_bookss.Data.Models
{
    public class Book
    {
        //all properties that will be as table column in db
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsRead { get; set; }
        public DateTime? DateRead { get; set; } //? make the property optional
        public int? Rate { get; set; }
        public string Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }
        //public StatusEnum Status { get; set; }

        //navigation properties

        public int PublisherId { get; set; } //foreign key

        public Publisher Publisher { get; set; }

        public List<Book_Author> Book_Authors { get; set; } //many to many relationship now becomes one to many
    }
}
