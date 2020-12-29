using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Service
{
    public class IntervalMergeService
    {
        private ExtractData[] extractDatas;
        private string[] distances;
        private double[][] intervals;

        public IntervalMergeService(object extractDatas, object distances)
        {
            this.extractDatas = (ExtractData[])extractDatas;
            this.distances = (string[])distances;
        }

        public void MergeInterval()
        {
            var mergedIntervals = new double[this.distances.Length][];
            var mergedList = new List<List<double>>();
            for (var i = 0; i < mergedIntervals.Length; i++)
            {
                mergedList.Add(new List<double>());
            }

            for (var i = 0; i < this.extractDatas.Length; i++)
            {
                for (var j = 0; j < this.extractDatas[i].crudes.Length; j++)
                {
                    if (this.extractDatas[i].crudes[j].name.Equals(distances[j]))
                    {
                        mergedList[j].AddRange(this.extractDatas[i].crudes[j].interval);
                    }
                }
            }

            for (var i = 0; i < mergedList.Count; i++)
            {
                mergedList[i].Add(0);
                var tmp = mergedList[i].Distinct().ToArray();
                Array.Sort(tmp);
                mergedIntervals[i] = tmp;
            }

            this.intervals = mergedIntervals;
        }

        public object GetMergedInterval() => this.intervals.Clone();
    }
}
