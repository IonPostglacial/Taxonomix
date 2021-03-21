using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Hierarchy<T>
    {
        public int Id { get; set; }
        public T Entry { get; set; }
        public List<Hierarchy<T>> Children { get; set; }
    }
}