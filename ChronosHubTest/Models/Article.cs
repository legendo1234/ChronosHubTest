using System;
using System.Collections.Generic;

namespace ChronosHubTest.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public DateTime PublicationDate { get; set; }
        public Journal Journal { get; set; }
        public List<Author> Authors { get; set; }
    }
}
