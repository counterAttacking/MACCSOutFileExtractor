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
        private Dictionary<string, string[]> distanceNames;
        private Dictionary<string, double[][]> mergedIntervals;

        private static string healthStr = "HEALTH EFFECTS CASES";
        private static string doseStr = "POPULATION DOSE (Sv)";
        private static string riskStr = "POPULATION WEIGHTED RISK";

        public IntervalMergeService(object extractDatas, object distances, Dictionary<string, string[]> distanceNames)
        {
            this.extractDatas = (ExtractData[])extractDatas;
            this.distances = (string[])distances;
            this.distanceNames = distanceNames;
            this.mergedIntervals = new Dictionary<string, double[][]>();
        }

        public object GetMergedInterval() => this.intervals.Clone();

        public Dictionary<string, double[][]> GetMergedIntervals()
        {
            var copied = new Dictionary<string, double[][]>(this.mergedIntervals);
            return copied;
        }

        public void MergeInterval()
        {
            foreach (var section in this.distanceNames)
            {
                if (section.Key.Equals(healthStr))
                {
                    var merged = this.MergeHealthInterval(section.Value);
                    this.mergedIntervals.Add(section.Key, merged);
                }
                else if (section.Key.Equals(doseStr))
                {
                    var merged = this.MergeDoseInterval(section.Value);
                    this.mergedIntervals.Add(section.Key, merged);
                }
                else if (section.Key.Equals(riskStr))
                {
                    var merged = this.MergeRiskInterval(section.Value);
                    this.mergedIntervals.Add(section.Key, merged);
                }
            }

            var mergedIntervals = new double[this.distances.Length][];
            var mergedList = new List<List<double>>();
            for (var i = 0; i < mergedIntervals.Length; i++)
            {
                mergedList.Add(new List<double>());
            }

            for (var i = 0; i < this.extractDatas.Length; i++)
            {
                for (var j = 0; j < this.extractDatas[i].healthCrudes.Length; j++)
                {
                    if (this.extractDatas[i].healthCrudes[j].name.Equals(distances[j]))
                    {
                        mergedList[j].AddRange(this.extractDatas[i].healthCrudes[j].interval);
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

        private double[][] MergeHealthInterval(string[] section)
        {
            var mergedIntervals = new double[section.Length][];
            var mergedList = new List<List<double>>();
            for (var i = 0; i < mergedIntervals.Length; i++)
            {
                mergedList.Add(new List<double>());
            }

            for (var i = 0; i < this.extractDatas.Length; i++)
            {
                for (var j = 0; j < this.extractDatas[i].healthCrudes.Length; j++)
                {
                    if (this.extractDatas[i].healthCrudes[j].name.Equals(section[j]))
                    {
                        mergedList[j].AddRange(this.extractDatas[i].healthCrudes[j].interval);
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

            return mergedIntervals;
        }

        private double[][] MergeDoseInterval(string[] section)
        {
            var mergedIntervals = new double[section.Length][];
            var mergedList = new List<List<double>>();
            for (var i = 0; i < mergedIntervals.Length; i++)
            {
                mergedList.Add(new List<double>());
            }

            for (var i = 0; i < this.extractDatas.Length; i++)
            {
                for (var j = 0; j < this.extractDatas[i].doseCrudes.Length; j++)
                {
                    if (this.extractDatas[i].doseCrudes[j].name.Equals(section[j]))
                    {
                        mergedList[j].AddRange(this.extractDatas[i].doseCrudes[j].interval);
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

            return mergedIntervals;
        }

        private double[][] MergeRiskInterval(string[] section)
        {
            var mergedIntervals = new double[section.Length][];
            var mergedList = new List<List<double>>();
            for (var i = 0; i < mergedIntervals.Length; i++)
            {
                mergedList.Add(new List<double>());
            }

            for (var i = 0; i < this.extractDatas.Length; i++)
            {
                for (var j = 0; j < this.extractDatas[i].riskCrudes.Length; j++)
                {
                    if (this.extractDatas[i].riskCrudes[j].name.Equals(section[j]))
                    {
                        mergedList[j].AddRange(this.extractDatas[i].riskCrudes[j].interval);
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

            return mergedIntervals;
        }
    }
}
