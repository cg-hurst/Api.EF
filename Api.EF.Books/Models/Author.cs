using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.EF.Books.Models
{
    public class Author
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
    }
}
