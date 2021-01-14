using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public object GetFrequencies => this.frequencies.Clone();
    }
}
