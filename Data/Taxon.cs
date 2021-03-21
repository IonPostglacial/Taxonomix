using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Taxon : Item
    {
        public List<BookReference> BookReferences { get; set; }
    }
}