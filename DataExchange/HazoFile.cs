using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Taxonomix.Data;

namespace Taxonomix.DataExchange
{
    class EncodedPhoto
    {
        public string id { get; set; }
        public object url { get; set; }
        public object label { get; set; }

        public static EncodedPhoto From(Picture pic)
        {
            return new()
            {
                id = pic.Id,
                label = pic.Legend,
                url = pic.Source,
            };
        }

        public Picture Decode()
        {
            return new Picture
            {
                Id = id,
                Legend = label switch
                {
                    string[] arr => arr[0],
                    string s => s,
                    _ => "",
                },
                Source = url switch
                {
                    string[] arr => arr[0],
                    string s => s,
                    _ => "",
                }
            };
        }
    }

    abstract class EncodedHierarchicalItem<T> where T : Item
    {
        public string id { get; set; }
		public string type { get; set; }
		public bool topLevel { get; set; }
		public string[] children { get; set; } = new string[]{};
		public string name { get; set; }
		public string nameEN { get; set; }
		public string nameCN { get; set; }
		public EncodedPhoto[] photos { get; set; } = new EncodedPhoto[]{};
		public string author { get; set; }
		public string vernacularName { get; set; }
		public string vernacularName2 { get; set; }
		public string name2 { get; set; }
		public string meaning { get; set; }
		public string herbariumPicture { get; set; }
		public string website { get; set; }
		public string noHerbier { get; set; }
		public string fasc { get; set; }
		public string page { get; set; }
		public string detail { get; set; }

        public static void From(EncodedHierarchicalItem<T> encoded, Item item)
        {
            encoded.id = item.Id;
            encoded.name = item.Name.Scientific;
            encoded.nameEN = item.Name.English;
            encoded.nameCN = item.Name.Chinese;
            encoded.vernacularName = item.Name.Vernacular;
            encoded.photos = item.Pictures.Select(pic => EncodedPhoto.From(pic)).ToArray();
        }

        public Item DecodeItem()
        {
            return new Item
            {
                Id = id,
                Name = new ItemName
                {
                    Scientific = name,
                    English = nameEN,
                    Chinese = nameCN,
                    Vernacular = vernacularName,
                },
                Pictures = photos.Select(photo => photo.Decode()).ToList(),
            };
        }

        public abstract T DecodeItem(Dictionary<string, State> statesByIds);

        public Hierarchy<T> Decode(Dictionary<string, EncodedHierarchicalItem<T>> itemsByIds, Dictionary<string, State> statesByIds)
        {
            return new Hierarchy<T>
            {
                Entry = this.DecodeItem(statesByIds),
                Children = children.Select(id => itemsByIds.GetValueOrDefault(id).Decode(itemsByIds, statesByIds)).ToList(),
            };
        }
    }

    class EncodedState
    {
        public string id { get; set; }
	    public string name { get; set; }
	    public string nameEN { get; set; }
	    public string nameCN { get; set; }
	    public EncodedPhoto[] photos { get; set; } = new EncodedPhoto[]{};
        public string description { get; set; }
        public string color { get; set; }

        public static EncodedState From(State state)
        {
            return new()
            {
                id = state.Id,
                name = state.Name.Scientific,
                nameEN = state.Name.English,
                nameCN = state.Name.Chinese,
                photos = state.Pictures.Select(pic => EncodedPhoto.From(pic)).ToArray(),
            };
        }

        public State Decode()
        {
            return new State
            {
                Id = id,
                Name = new ItemName { Scientific = name, English = nameEN, Chinese = nameCN },
                Description = description,
                Pictures = photos.Select(photo => photo.Decode()).ToList(),
            };
        }
    }

    class EncodedCharacter : EncodedHierarchicalItem<Character>
    {
        public string[] states { get; set; } = new string[]{};
		public string inherentStateId { get; set; }
		public string[] inapplicableStatesIds { get; set; } = new string[]{};
		public string[] requiredStatesId { get; set; } = new string[]{};

        public static EncodedCharacter From(Hierarchy<Character> ch, Character parent)
        {
            var encoded = new EncodedCharacter()
            {
                states = ch.Entry.States.Select(s => s.Id).ToArray(),
                requiredStatesId = ch.Entry.RequiredStates.Select(s => s.Id).ToArray(),
                children = ch.Children.Select(child => child.Entry.Id).ToArray(),
                topLevel = parent == null,
            };
            EncodedHierarchicalItem<Character>.From(encoded, ch.Entry);
            return encoded;
        }

        public override Character DecodeItem(Dictionary<string, State> statesByIds)
        {
            var character = new Character
            {
                States = states?.Select(stateId => statesByIds.GetValueOrDefault(stateId)).ToList() ?? new(),
                RequiredStates = requiredStatesId?.Select(stateId => statesByIds.GetValueOrDefault(stateId)).ToList() ?? new(),
            };
            character.Assign(base.DecodeItem());
            return character;
        }
    }

    class EncodedDescription
    {
        public string descriptorId { get; set; }
        public string[] statesIds { get; set; } 
    }

    class EncodedBookInfo
    {
        public string fasc { get; set; }
        public string page { get; set; }
        public string detail { get; set; }
    }

    class EncodedTaxon : EncodedHierarchicalItem<Taxon>
    {
        public Dictionary<string, EncodedBookInfo> bookInfoByIds { get; set; }
		public EncodedDescription[] descriptions { get; set; }

        public static EncodedTaxon From(Hierarchy<Taxon> taxon, IEnumerable<Character> characters, Taxon parent)
        {
            var encoded = new EncodedTaxon()
            {
                descriptions = characters
                    .Select(ch => new EncodedDescription
                    {
                        descriptorId = ch.Id,
                        statesIds = ch.States.Intersect(taxon.Entry.States).Select(s => s.Id).ToArray()
                    })
                    .Where(desc => desc.statesIds.Length > 0).ToArray(),
                topLevel = parent == null,
                children = taxon.Children.Select(child => child.Entry.Id).ToArray(),
                bookInfoByIds = new(),
            };
            EncodedHierarchicalItem<Taxon>.From(encoded, taxon.Entry);
            return encoded;
        }

        public override Taxon DecodeItem(Dictionary<string, State> statesByIds)
        {
            var taxon = new Taxon
            {
                States = descriptions.SelectMany(d => d.statesIds.Select(s => statesByIds.GetValueOrDefault(s))).ToList()
            };
            taxon.Assign(base.DecodeItem());
            return taxon;
        }
    }

    class EncodedBook
    {
        public string id { get; set; }
        public string @for { get; set; }
        public string index { get; set; }
    }

    class EncodedDictionaryEntry
    {
        public int id { get; set; }
	    public string nameCN { get; set; }
	    public string nameEN { get; set; }
	    public string nameFR { get; set; }
	    public string defCN { get; set; }
	    public string defEN { get; set; }
	    public string defFR { get; set; }
	    public string url { get; set; }
    }

    class EncodedField
    {
        bool std { get; set; }
        string id { get; set; }
        string label { get; set; }
        string icon { get; set; }
    }

    class EncodedDataset
    {
        public string id { get; set; }

        public EncodedTaxon[] taxons { get; set; }

        public EncodedCharacter[] characters { get; set; }
        
        public EncodedState[] states { get; set; }
        
        public EncodedBook[] books { get; set; }
        
        public EncodedField[] extraFields { get; set; }
        
        public Dictionary<string, EncodedDictionaryEntry> dictionaryEntries { get; set; }

        public static EncodedDataset From(Dataset dataset)
        {
            var allCharacters = dataset.Characters.SelectMany(ch => ch.IterTree());
            return new()
            {
                id = dataset.Id,
                taxons = dataset.Taxons
                    .SelectMany(t => {
                        var topLevel = new EncodedTaxon[]{EncodedTaxon.From(t, allCharacters, null)};
                        var children = t.AllChildren()
                                .Where(child => child.Item2.Entry.Id != t.Entry.Id)
                                .Select(child => EncodedTaxon.From(child.Item2, allCharacters, child.Item1));
                        return topLevel.Concat(children);
                    }).ToArray(),
                characters = dataset.Characters.SelectMany(ch => {
                        var topLevel = new EncodedCharacter[]{EncodedCharacter.From(ch, null)};
                        var children = ch.AllChildren()
                                .Where(child => child.Item2.Entry.Id != ch.Entry.Id)
                                .Select(child => EncodedCharacter.From(child.Item2, child.Item1));
                        return topLevel.Concat(children);
                    }).ToArray(),
                states = allCharacters.SelectMany(ch => ch.States.Select(s => EncodedState.From(s))).ToArray(),
                books = new EncodedBook[] {},
                extraFields = new EncodedField[]{},
                dictionaryEntries = new(),
            };
        }

        public Dataset Decode()
        {
            var statesByIds = states.Select(s => s.Decode()).ToDictionary(s => s.Id);
            var taxonsByIds = taxons.ToDictionary<EncodedHierarchicalItem<Taxon>, string>(t => t.id);
            var charactersByIds = characters.ToDictionary<EncodedHierarchicalItem<Character>, string>(c => c.id);
            return new Dataset
            {
                Id = id,
                Characters = characters.Where(c => c.topLevel).Select(c => c.Decode(charactersByIds, statesByIds)).ToList(),
                Taxons = taxons.Where(t => t.topLevel).Select(t => t.Decode(taxonsByIds, statesByIds)).ToList(),
            };
        }
    }

    public class HazoFile
    {
        public static async Task<Dataset> Parse(Stream stream)
        {
            var encodedDataset = await JsonSerializer.DeserializeAsync<EncodedDataset>(stream);
            return encodedDataset.Decode();
        }

        public static string Generate(Dataset dataset)
        {
            return JsonSerializer.Serialize(EncodedDataset.From(dataset), new() { IgnoreNullValues = true });
        }
    }
}