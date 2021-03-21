using System.Collections.Generic;
using System.Threading.Tasks;

namespace Taxonomix.Data
{
    public class DatasetsService
    {
        private static readonly Dictionary<string, Dataset> Datasets = new Dictionary<string, Dataset>
        {
            { "1", new Dataset() }
        };

        public Task<Dataset> GetDatasetAsync(string id)
        {
            Datasets.TryGetValue(id, out var dataset);
            return Task.FromResult<Dataset>(dataset);
        }
    }
}
