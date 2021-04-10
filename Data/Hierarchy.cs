using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Hierarchy<T>
    {
        public int Id { get; set; }
        public T Entry { get; set; }
        public List<Hierarchy<T>> Children { get; set; } = new();

        public IEnumerable<T> IterTree()
        {
            yield return Entry;
            foreach (var child in Children)
            {
                foreach (var result in child.IterTree())
                {
                    yield return result;
                }
            }
        }

        public IEnumerable<(T, Hierarchy<T>)> AllChildren()
        {
            foreach (var child in Children)
            {
                yield return (this.Entry, child);
                foreach (var result in child.AllChildren())
                {
                    yield return result;
                }
            }
        }
    }
}