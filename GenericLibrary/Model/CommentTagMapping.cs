using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericLibrary.Model
{
    public class CommentTagMapping
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public int TagId { get; set; }
    }
}
