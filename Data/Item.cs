using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Item
    {
        public string Id { get; set; }
        public ItemName Name { get; set; }
        public string Description { get; set; }
        public List<Picture> Pictures { get; set; }
    }
}