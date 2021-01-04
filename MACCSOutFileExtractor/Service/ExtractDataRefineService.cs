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

        public ExtractDataRefineService(object extractDatas, object intervals, object distances)
        {
            this.extractDatas = (ExtractData[])extractDatas;
            this.intervals = (double[][])intervals;
            this.distances = (string[])distances;
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
                    if (this.distances[i].Equals(this.extractDatas[j].crudes[i].name))
                    {
                        var data = new RefineData
                        {
                            name = this.extractDatas[j].name,
                            interval = this.intervals[i],
                            intervalVal = new double[this.intervals[i].Length],
                            distance = this.extractDatas[j].crudes[i].name
                        };
                        for (var k = 0; k < this.extractDatas[j].crudes[i].interval.Length; k++)
                        {
                            var idx = Array.FindIndex(this.intervals[i], target => target == this.extractDatas[j].crudes[i].interval[k]);
                            data.intervalVal[idx] = this.extractDatas[j].crudes[i].intervalVal[k];
                        }
                        datas.Add(data);
                    }
                }
                this.refineDatas = datas.ToArray();
                var interpolationService = new InterpolationService(this.refineDatas.Clone());
                interpolationService.Interpolation();
                this.refineDatas = (RefineData[])interpolationService.GetRefineData();
                var fileWriteService = new CsvFileWriteService(this.refineDatas.Clone(), this.distances[i]);
                fileWriteService.FileWrite();
            }
        }
    }
}
