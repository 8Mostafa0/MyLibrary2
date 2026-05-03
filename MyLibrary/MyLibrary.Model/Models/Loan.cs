using System;

namespace MyLibrary.Model.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int BookId { get; set; }
        public string BookName { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime? ReturnedDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
