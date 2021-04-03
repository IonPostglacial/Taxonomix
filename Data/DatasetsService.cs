using System.Collections.Generic;

namespace Taxonomix.Data
{
    public class DatasetsService
    {
        private readonly Dictionary<string, Dataset> Datasets = new();
        public Dataset SelectedDataset { get; private set; }

        public void AddDataset(Dataset dataset)
        {
            Datasets.Add(dataset.Id, dataset);
        }

        public void SelectDataset(string datasetKey)
        {
            Datasets.TryGetValue(datasetKey, out var dataset);
            if (dataset != null)
            {
                SelectedDataset = dataset;
            }
        }

        public IEnumerable<string> GetDatasetIds()
        {
            foreach (var dataset in Datasets)
            {
                yield return dataset.Key;
            }
        }

        public Dataset GetDataset(string id)
        {
            Datasets.TryGetValue(id, out var dataset);
            return dataset;
        }
    }
}
