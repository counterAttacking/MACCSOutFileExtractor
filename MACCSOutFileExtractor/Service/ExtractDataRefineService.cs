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
        private Dictionary<string, string[]> distanceNames;
        private Dictionary<string, double[][]> mergedIntervals;
        private RefineData[] refineDatas;
        private bool isFrequency;

        private static string healthStr = "HEALTH EFFECTS CASES";
        private static string doseStr = "POPULATION DOSE (Sv)";
        private static string riskStr = "POPULATION WEIGHTED RISK";

        public ExtractDataRefineService(object extractDatas, object intervals, Dictionary<string, double[][]> mergedIntervals,
            object distances, Dictionary<string, string[]> distanceNames, bool isFrequency)
        {
            this.extractDatas = (ExtractData[])extractDatas;
            this.intervals = (double[][])intervals;
            this.distances = (string[])distances;
            this.distanceNames = distanceNames;
            this.mergedIntervals = mergedIntervals;
            this.isFrequency = isFrequency;
        }

        public void DataRefine()
        {
            foreach (var section in this.distanceNames)
            {
                if (section.Key.Equals(healthStr))
                {
                    this.RefineHealth(section.Key, section.Value, this.mergedIntervals[healthStr]);
                }
                else if (section.Key.Equals(doseStr))
                {
                    this.RefineDose(section.Key, section.Value, this.mergedIntervals[doseStr]);
                }
                else if (section.Key.Equals(riskStr))
                {
                    this.RefineRisk(section.Key, section.Value, this.mergedIntervals[riskStr]);
                }
            }
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

        private void RefineHealth(string result, string[] section, double[][] intervals)
        {
            for (var i = 0; i < section.Length; i++)
            {
                var datas = new List<RefineData>();
                for (var j = 0; j < this.extractDatas.Length; j++)
                {
                    if (section[i].Equals(this.extractDatas[j].healthCrudes[i].name))
                    {
                        var data = new RefineData
                        {
                            name = this.extractDatas[j].name,
                            interval = intervals[i],
                            intervalVal = new double[intervals[i].Length],
                            distance = this.extractDatas[j].healthCrudes[i].name
                        };
                        for (var k = 0; k < this.extractDatas[j].healthCrudes[i].interval.Length; k++)
                        {
                            var idx = Array.FindIndex(intervals[i], target => target == this.extractDatas[j].healthCrudes[i].interval[k]);
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
                var fileWriteService = new CsvFileWriteService(this.refineDatas.Clone(), section[i]);
                fileWriteService.FileWrite();
            }
        }

        private void RefineDose(string result, string[] section, double[][] intervals)
        {
            for (var i = 0; i < section.Length; i++)
            {
                var datas = new List<RefineData>();
                for (var j = 0; j < this.extractDatas.Length; j++)
                {
                    if (section[i].Equals(this.extractDatas[j].doseCrudes[i].name))
                    {
                        var data = new RefineData
                        {
                            name = this.extractDatas[j].name,
                            interval = intervals[i],
                            intervalVal = new double[intervals[i].Length],
                            distance = this.extractDatas[j].doseCrudes[i].name
                        };
                        for (var k = 0; k < this.extractDatas[j].doseCrudes[i].interval.Length; k++)
                        {
                            var idx = Array.FindIndex(intervals[i], target => target == this.extractDatas[j].doseCrudes[i].interval[k]);
                            data.intervalVal[idx] = this.extractDatas[j].doseCrudes[i].intervalVal[k];
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
                var fileWriteService = new CsvFileWriteService(this.refineDatas.Clone(), section[i]);
                fileWriteService.FileWrite();
            }
        }

        private void RefineRisk(string result, string[] section, double[][] intervals)
        {
            for (var i = 0; i < section.Length; i++)
            {
                var datas = new List<RefineData>();
                for (var j = 0; j < this.extractDatas.Length; j++)
                {
                    if (section[i].Equals(this.extractDatas[j].riskCrudes[i].name))
                    {
                        var data = new RefineData
                        {
                            name = this.extractDatas[j].name,
                            interval = intervals[i],
                            intervalVal = new double[intervals[i].Length],
                            distance = this.extractDatas[j].riskCrudes[i].name
                        };
                        for (var k = 0; k < this.extractDatas[j].riskCrudes[i].interval.Length; k++)
                        {
                            var idx = Array.FindIndex(intervals[i], target => target == this.extractDatas[j].riskCrudes[i].interval[k]);
                            data.intervalVal[idx] = this.extractDatas[j].riskCrudes[i].intervalVal[k];
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
                var fileWriteService = new CsvFileWriteService(this.refineDatas.Clone(), section[i]);
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
