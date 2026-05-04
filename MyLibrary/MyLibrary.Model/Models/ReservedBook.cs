using System;

namespace MyLibrary.Model.Models
{
    public class ReservedBook
    {
        public int ID { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }


        public static ReservedBook Empty()
        {
            return new ReservedBook() { ID = 0 };
        }
    }
}
