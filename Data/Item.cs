using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class Item
    {
        public string Id { get; set; }
        public ItemName Name { get; set; }
        public string Description { get; set; }
        public List<Picture> Pictures { get; set; }

        public void Assign(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            Description = item.Description;
            Pictures = item.Pictures;
        }

        public override bool Equals(object obj)
        {
            return obj is Item it && it.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}