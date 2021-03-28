using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Taxon : Item
    {
        public List<State> States { get; set; } = new();
        public List<BookReference> BookReferences { get; set; } = new();
    }
}