using System;
using System.Collections.Generic;
using System.Text;

namespace GenericLibrary.Model
{
    public class CommentTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string City { get; set; }
        public string UserComment { get; set; }
        public List<Tag> Tag { get; set; }
    }
}
