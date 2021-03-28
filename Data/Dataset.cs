using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Dataset
    {
        public string Id { get; set; }
        public List<Hierarchy<Taxon>> Taxons { get; set; } = new List<Hierarchy<Taxon>>();
        public List<Hierarchy<Character>> Characters { get; set; } = new List<Hierarchy<Character>>();
    }
}