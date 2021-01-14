using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MACCSOutFileExtractor.Service
{
    public class ExtractFrequencyDataService
    {
        private FrequencyData[] frequencies;

        private ExtractFrequencyDataService()
        {

        }

        private static readonly Lazy<ExtractFrequencyDataService> extractFrequencyService = new Lazy<ExtractFrequencyDataService>(() => new ExtractFrequencyDataService());

        public static ExtractFrequencyDataService GetExtractFrequencyService
        {
            get
            {
                return extractFrequencyService.Value;
            }
        }

        public object GetFrequencies() => this.frequencies.Clone();

        public void ExtractFrequency(DataGridView dgvFrequency)
        {
            var frequencies = new List<FrequencyData>();
            var nameIdx = 0;
            var valueIdx = 0;

            for (var i = 0; i < dgvFrequency.ColumnCount; i++)
            {
                if (dgvFrequency.Columns[i].Name.Equals("File Name"))
                {
                    nameIdx = i;
                }
                else if (dgvFrequency.Columns[i].Name.Equals("Frequency Value"))
                {
                    valueIdx = i;
                }
            }

            for (var i = 0; i < dgvFrequency.RowCount; i++)
            {
                var frequency = new FrequencyData
                {
                    fileName = dgvFrequency[nameIdx, i].Value.ToString(),
                    frequencyValue = Convert.ToDouble(dgvFrequency[valueIdx, i].Value)
                };

                frequencies.Add(frequency);
            }

            this.frequencies = frequencies.ToArray();
        }
    }
}
