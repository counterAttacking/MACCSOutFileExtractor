using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Service
{
    public class ExtractDataRefineService
    {
        private ExtractData[] extractDatas;
        private double[][] intervals;
        private string[] distances;
        private RefineData[] refineDatas;
        private bool isFrequency;

        public ExtractDataRefineService(object extractDatas, object intervals, object distances, bool isFrequency)
        {
            this.extractDatas = (ExtractData[])extractDatas;
            this.intervals = (double[][])intervals;
            this.distances = (string[])distances;
            this.isFrequency = isFrequency;
        }

        public void DataRefine()
        {
            this.MatchPreviousData();
        }

        private void MatchPreviousData()
        {
            for (var i = 0; i < this.distances.Length; i++)
            {
                var datas = new List<RefineData>();
                for (var j = 0; j < this.extractDatas.Length; j++)
                {
                    if (this.distances[i].Equals(this.extractDatas[j].healthCrudes[i].name))
                    {
                        var data = new RefineData
                        {
                            name = this.extractDatas[j].name,
                            interval = this.intervals[i],
                            intervalVal = new double[this.intervals[i].Length],
                            distance = this.extractDatas[j].healthCrudes[i].name
                        };
                        for (var k = 0; k < this.extractDatas[j].healthCrudes[i].interval.Length; k++)
                        {
                            var idx = Array.FindIndex(this.intervals[i], target => target == this.extractDatas[j].healthCrudes[i].interval[k]);
                            data.intervalVal[idx] = this.extractDatas[j].healthCrudes[i].intervalVal[k];
                        }
                        datas.Add(data);
                    }
                }
                this.refineDatas = datas.ToArray();
                var interpolationService = new InterpolationService(this.refineDatas.Clone());
                interpolationService.Interpolation();
                this.refineDatas = (RefineData[])interpolationService.GetRefineData();
                if (this.isFrequency == true)
                {
                    this.MultiplyFrequency();
                }
                var fileWriteService = new CsvFileWriteService(this.refineDatas.Clone(), this.distances[i]);
                fileWriteService.FileWrite();
            }
        }

        private void MultiplyFrequency()
        {
            var frequencies = (FrequencyData[])ExtractFrequencyDataService.GetExtractFrequencyService.GetFrequencies();

            for (var i = 0; i < this.refineDatas.Length; i++)
            {
                var idx = Array.FindIndex(frequencies, target => target.fileName == this.refineDatas[i].name);

                for (var j = 0; j < this.refineDatas[i].intervalVal.Length; j++)
                {
                    this.refineDatas[i].intervalVal[j] *= frequencies[idx].frequencyValue;
                }
            }
        }
    }
}
