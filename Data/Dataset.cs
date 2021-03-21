using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Dataset
    {
        public string Id { get; set; }
        public List<Hierarchy<Taxon>> TaxonsHierarchy { get; set; }
        public List<Hierarchy<Character>> CharactersHierarchy { get; set; }
    }
}