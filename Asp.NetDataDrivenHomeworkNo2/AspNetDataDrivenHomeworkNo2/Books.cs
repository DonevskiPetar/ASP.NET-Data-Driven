using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetDataDrivenHomeworkNo2
{
    public class Books
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public int AuthorID { get; set; }

        public virtual Authors Authors { get; set; }
    }
}
