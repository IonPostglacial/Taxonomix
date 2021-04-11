using System.Threading.Tasks;
using Taxonomix.Data;
using System.IO;
using System.Collections.Generic;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Globalization;

namespace Taxonomix.DataExchange
{
    class TreeCsvRecord
    {
        [Name("IDBASE")]
        public string Id { get; set; }

        [Name("TYPE EMPLACEMENT")]
        public string PlaceType { get; set; }

        [Name("DOMANIALITE")]
        public string Domaniality { get; set; }

        [Name("ARRONDISSEMENT")]
        public string District { get; set; }

        [Name("COMPLEMENT ADRESSE")]
        public string AddressComplement { get; set; }

        [Name("NUMERO")]
        public string Number { get; set; }

        [Name("LIEU / ADRESSE")]
        public string Address { get; set; }

        [Name("IDEMPLACEMENT")]
        public string PlaceId { get; set; }

        [Name("LIBELLE FRANCAIS")]
        public string FrenchName { get; set; }

        [Name("俗名")]
        public string ChineseName { get; set; }

        [Name("Famille")]
        public string Family { get; set; }

        [Name("科")]
        public string ChineseFamily { get; set; }

        [Name("GENRE")]
        public string Genus { get; set; }

        [Name("ESPECE")]
        public string Specie { get; set; }

        [Name("VARIETE OUCULTIVAR")]
        public string Cultivar { get; set; }

        [Name("CIRCONFERENCE (cm)")]
        public int Circumference { get; set; }

        [Name("HAUTEUR (m)")]
        public int Hight { get; set; }

        [Name("STADE DE DEVELOPPEMENT")]
        public string DevelopmentStage { get; set; }

        [Name("REMARQUABLE")]
        public string Remarkable { get; set; }

        [Name("geo_point_2d")]
        public string GeoPoint2 { get; set; }
    }

    public class TreeCsv
    {
        static List<Hierarchy<Taxon>> ReadCsv(StreamReader reader)
        {
            var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            var records = csvReader.GetRecords<TreeCsvRecord>();
            // Family > Genus > Specie
            var result = new List<Hierarchy<Taxon>>();
            var genusByName = new Dictionary<string, Hierarchy<Taxon>>();
            var familiesByName = new Dictionary<string, Hierarchy<Taxon>>();
            var speciesByName = new Dictionary<string, Hierarchy<Taxon>>();
            var taxonId = 0;
            foreach (var record in records)
            {
                Hierarchy<Taxon> genus, family, specie;
                if (!familiesByName.TryGetValue(record.Family, out family))
                {
                    taxonId++;
                    family = new Hierarchy<Taxon>
                    {
                        Entry = new Taxon
                        {
                            Id = $"t{taxonId}",
                            Name = new ItemName
                            {
                                Scientific = record.Family,
                                Chinese = record.ChineseFamily,
                            }
                        }
                    };
                    familiesByName.Add(family.Entry.Name.Scientific, family);
                    result.Add(family);
                }
                if (!genusByName.TryGetValue(record.Genus, out genus))
                {
                    taxonId++;
                    genus = new Hierarchy<Taxon>
                    {
                        Entry = new Taxon
                        {
                            Id = $"t{taxonId}",
                            Name = new ItemName
                            {
                                Scientific = record.Genus,
                            }
                        }
                    };
                    genusByName.Add(genus.Entry.Name.Scientific, genus);
                    family.Children.Add(genus);
                }
                if (!speciesByName.TryGetValue(record.Specie, out specie))
                {
                    taxonId++;
                    specie = new Hierarchy<Taxon>
                    {
                        Entry = new Taxon
                        {
                            Id = $"t{taxonId}",
                            Name = new ItemName
                            {
                                Scientific = record.Specie,
                                Chinese = record.ChineseName,
                                Vernacular = record.FrenchName,
                            }
                        }
                    };
                    speciesByName.Add(specie.Entry.Name.Scientific, specie);
                    genus.Children.Add(specie);
                }
            }
            return result;
        }

        public async static Task<List<Hierarchy<Taxon>>> Parse(Stream stream)
        {
            List<Hierarchy<Taxon>> result;
            using (var stream2 = new MemoryStream())
            {
                await stream.CopyToAsync(stream2);
                stream2.Seek(0, SeekOrigin.Begin);
                var reader = new System.IO.StreamReader(stream2);
                result = ReadCsv(reader);
            }
            return result;
        }
    }
}