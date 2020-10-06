using System;
namespace ef_core_sqlite_example.Model
{
    public class Medium_Author
    {
        public int Id { get; set; }
        public Author AuthorId { get; set; }
        public Medium MediumId { get; set; }
    }
}
