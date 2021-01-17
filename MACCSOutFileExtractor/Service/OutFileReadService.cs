using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Service
{
    public class OutFileReadService
    {
        private OutFile[] inputFiles;
        private ExtractData[] extractDatas;
        private Dictionary<string, string[]> distanceNames;

        private static string healthStr = "RESULT NAME = HEALTH EFFECTS CASES";
        private static string doseStr = "RESULT NAME = POPULATION DOSE (Sv)";
        private static string riskStr = "RESULT NAME = POPULATION WEIGHTED RISK";
        private static string peakStr = "RESULT NAME = PEAK DOSE FOUND ON SPATIAL GRID (Sv)";
        private static string fatStr = "FAT/TOTAL";
        private static string totLifStr = "L-ICRP60ED  TOT LIF";
        private static string overallStr = "OVERALL";
        private static string overallEndStr = "SOURCE TERM";

        public OutFileReadService(OutFile[] inputFiles)
        {
            this.inputFiles = inputFiles;
            this.distanceNames = new Dictionary<string, string[]>();
        }

        public object GetExtractDatas() => this.extractDatas.Clone();

        public Dictionary<string, string[]> GetDistanceNames()
        {
            var copied = new Dictionary<string, string[]>(this.distanceNames);
            return copied;
        }

        public void ReadOutFile()
        {
            var inputFileLen = this.inputFiles.Length;
            var extracts = new List<ExtractData>();

            for (var i = 0; i < inputFileLen; i++)
            {
                var healthCrudes = new List<OutData>();
                var doseCrudes = new List<OutData>();
                var riskCrudes = new List<OutData>();

                using (var fileStream = new FileStream(inputFiles[i].fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        healthCrudes = this.ReadHealthSection(streamReader);
                        doseCrudes = this.ReadDoseSection(streamReader);
                        riskCrudes = this.ReadRiskSection(streamReader);
                    }
                }

                var extract = new ExtractData()
                {
                    name = inputFiles[i].name,
                    healthCrudes = healthCrudes.ToArray(),
                    doseCrudes = doseCrudes.ToArray(),
                    riskCrudes = riskCrudes.ToArray()
                };
                extracts.Add(extract);
            }

            this.extractDatas = extracts.ToArray();
        }

        private List<OutData> ReadHealthSection(StreamReader streamReader)
        {
            var isHealthStrFound = false;
            var isOverallStrFound = false;
            var overallIdx = 0;
            string name = null;
            var intervals = new List<double>();
            var intervalValues = new List<double>();
            var healthCrudes = new List<OutData>();
            var distances = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var readLine = streamReader.ReadLine();

                if (readLine.Contains(healthStr))
                {
                    /* 
                     * 'RESULT NAME = HEALTH EFFECTS CASES'에 해당하는 부분에 찾고자 하는 값들이 존재
                     * 이 분기문 밑으로 넘어가면 안 되기 때문에 continue 설정
                     */
                    isHealthStrFound = true;
                    continue;
                }
                else if (readLine.Contains(doseStr))
                {
                    /* 
                     * 'RESULT NAME = POPULATION DOSE (Sv)' 검색된 줄 밑으로 찾고자 하는 값들은
                     * ReadDoseSection()에서 처리
                     */
                    break;
                }

                if (isHealthStrFound == true)
                {
                    /* 
                     * healthStr 바로 다음 줄에는
                     * EAL FAT/TOTAL, CAN FAT/TOTAL에 대한 내용 존재
                     * OVERALL이 존재하는 위치에 해당하는 값들만 추출
                     */

                    // EAL FAT/TOTAL, CAN FAT/TOTAL에 대한 내용 처리
                    if (readLine.Contains(fatStr))
                    {
                        name = readLine.TrimStart();
                        if (!distances.Contains(name))
                        {
                            distances.Add(name);
                        }
                        continue;
                    }

                    // OVERALL이 존재하는 위치 파악
                    if (readLine.Contains(overallStr))
                    {
                        var splitStr = readLine.TrimStart().Split(' ');
                        for (var j = 0; j < splitStr.Length; j++)
                        {
                            if (splitStr[j].Equals(overallStr))
                            {
                                overallIdx = j;
                                isOverallStrFound = true;
                                break;
                            }
                        }
                        continue;
                    }

                    // X가 0이 아닌 부분부터 X와 PROB>=X 값을 가지고 오기
                    if (isOverallStrFound == true)
                    {
                        // 'SOURCE TERM  1 OF  1:'가 존재하면 다른 데이터가 존재하므로
                        if (readLine.Contains(overallEndStr))
                        {
                            var healthCrude = new OutData()
                            {
                                name = name,
                                interval = intervals.ToArray(),
                                intervalVal = intervalValues.ToArray()
                            };
                            healthCrudes.Add(healthCrude);
                            intervals.Clear();
                            intervalValues.Clear();
                            isHealthStrFound = false;
                            isOverallStrFound = false;
                            continue;
                        }
                        var splitStr = readLine.TrimStart().Split(' ');
                        if (!splitStr[overallIdx].Equals("------------------") && !splitStr[overallIdx].Equals("X") && !String.IsNullOrEmpty(splitStr[overallIdx]))
                        {
                            var interval = Convert.ToDouble(splitStr[overallIdx]);
                            if (interval != 0)
                            {
                                var intervalValue = Convert.ToDouble(splitStr[overallIdx + 2]);
                                intervals.Add(interval);
                                intervalValues.Add(intervalValue);
                            }
                        }
                    }
                }
            }

            var distanceName = this.MakeDistanceName(healthStr);
            if (!this.distanceNames.ContainsKey(distanceName))
            {
                this.distanceNames.Add(distanceName, distances.ToArray());
            }
            return healthCrudes;
        }

        private List<OutData> ReadDoseSection(StreamReader streamReader)
        {
            var isDoseStrFound = true;
            var isOverallStrFound = false;
            var overallIdx = 0;
            string name = null;
            var intervals = new List<double>();
            var intervalValues = new List<double>();
            var doseCrudes = new List<OutData>();
            var distances = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var readLine = streamReader.ReadLine();

                if (readLine.Contains(doseStr))
                {
                    /*
                     * 'RESULT NAME = POPULATION DOSE (Sv)'이 나타나는 지점부터 찾고자 하는 값들이 존재
                     * 이 분기문 밑으로 넘어가면 안되기 때문에 continnue 설정
                     */
                    isDoseStrFound = true;
                    continue;
                }
                else if (readLine.Contains(riskStr))
                {
                    /*
                     * 'RESULT NAME = POPULATION WEIGHTED RISK' 검색된 줄 밑으로 찾고자 하는 값들은
                     * ReadRiskSection()에서 처리
                     */
                    break;
                }

                if (isDoseStrFound == true)
                {
                    /*
                     * doseStr 바로 다음 줄에는 'L-ICRP60ED  TOT LIF'이 존재하며
                     * OVERALL이 존재하는 위치하는 곳의 값들을 추출
                     */

                    // L-ICRP60ED  TOT LIF 에 대한 내용 처리
                    if (readLine.Contains(totLifStr))
                    {
                        name = readLine.TrimStart();
                        if (!distances.Contains(name))
                        {
                            distances.Add(name);
                        }
                        continue;
                    }

                    // OVERALL이 존재하는 위치 파악
                    if (readLine.Contains(overallStr))
                    {
                        var splitStr = readLine.TrimStart().Split(' ');
                        for (var j = 0; j < splitStr.Length; j++)
                        {
                            if (splitStr[j].Equals(overallStr))
                            {
                                overallIdx = j;
                                isOverallStrFound = true;
                                break;
                            }
                        }
                        continue;
                    }

                    // X가 0이 아닌 부분부터 X와 PROB>=X 값을 가지고 오기
                    if (isOverallStrFound == true)
                    {
                        // 'SOURCE TERM  1 OF  1:'가 존재하면 다른 데이터가 존재하므로
                        if (readLine.Contains(overallEndStr))
                        {
                            var doseCrude = new OutData()
                            {
                                name = name,
                                interval = intervals.ToArray(),
                                intervalVal = intervalValues.ToArray()
                            };
                            doseCrudes.Add(doseCrude);
                            intervals.Clear();
                            intervalValues.Clear();
                            isDoseStrFound = false;
                            isOverallStrFound = false;
                            continue;
                        }
                        var splitStr = readLine.TrimStart().Split(' ');
                        if (!splitStr[overallIdx].Equals("------------------") && !splitStr[overallIdx].Equals("X") && !String.IsNullOrEmpty(splitStr[overallIdx]))
                        {
                            var interval = Convert.ToDouble(splitStr[overallIdx]);
                            if (interval != 0)
                            {
                                var intervalValue = Convert.ToDouble(splitStr[overallIdx + 2]);
                                intervals.Add(interval);
                                intervalValues.Add(intervalValue);
                            }
                        }
                    }
                }
            }

            var distanceName = this.MakeDistanceName(doseStr);
            if (!this.distanceNames.ContainsKey(distanceName))
            {
                this.distanceNames.Add(distanceName, distances.ToArray());
            }
            return doseCrudes;
        }

        private List<OutData> ReadRiskSection(StreamReader streamReader)
        {
            var isRiskStrFound = true;
            var isOverallStrFound = false;
            var overallIdx = 0;
            string name = null;
            var intervals = new List<double>();
            var intervalValues = new List<double>();
            var riskCrudes = new List<OutData>();
            var distances = new List<string>();

            while (!streamReader.EndOfStream)
            {
                var readLine = streamReader.ReadLine();

                if (readLine.Contains(riskStr))
                {
                    /*
                     * 'RESULT NAME = POPULATION WEIGHTED RISK'이 나타나는 지점부터 찾고자 하는 값들이 존재
                     * 이 분기문 밑으로 넘어가면 안되기 때문에 continnue 설정
                     */
                    isRiskStrFound = true;
                    continue;
                }
                else if (readLine.Contains(peakStr))
                {
                    // 추출할 값 모두 추출
                    break;
                }

                if (isRiskStrFound == true)
                {
                    /*
                     * riskStr 바로 다음 줄에는
                     * EAL FAT/TOTAL, CAN FAT/TOTAL에 대한 내용 존재
                     * OVERALL이 존재하는 위치하는 곳의 값들을 추출
                     */

                    // EAL FAT/TOTAL, CAN FAT/TOTAL에 대한 내용 처리
                    if (readLine.Contains(fatStr))
                    {
                        name = readLine.TrimStart();
                        if (!distances.Contains(name))
                        {
                            distances.Add(name);
                        }
                        continue;
                    }

                    // OVERALL이 존재하는 위치 파악
                    if (readLine.Contains(overallStr))
                    {
                        var splitStr = readLine.TrimStart().Split(' ');
                        for (var j = 0; j < splitStr.Length; j++)
                        {
                            if (splitStr[j].Equals(overallStr))
                            {
                                overallIdx = j;
                                isOverallStrFound = true;
                                break;
                            }
                        }
                        continue;
                    }

                    // X가 0이 아닌 부분부터 X와 PROB>=X 값을 가지고 오기
                    if (isOverallStrFound == true)
                    {
                        // 'SOURCE TERM  1 OF  1:'가 존재하면 다른 데이터가 존재하므로
                        if (readLine.Contains(overallEndStr))
                        {
                            var riskCrude = new OutData()
                            {
                                name = name,
                                interval = intervals.ToArray(),
                                intervalVal = intervalValues.ToArray()
                            };
                            riskCrudes.Add(riskCrude);
                            intervals.Clear();
                            intervalValues.Clear();
                            isRiskStrFound = false;
                            isOverallStrFound = false;
                            continue;
                        }
                        var splitStr = readLine.TrimStart().Split(' ');
                        if (!splitStr[overallIdx].Equals("------------------") && !splitStr[overallIdx].Equals("X") && !String.IsNullOrEmpty(splitStr[overallIdx]))
                        {
                            var interval = Convert.ToDouble(splitStr[overallIdx]);
                            if (interval != 0)
                            {
                                var intervalValue = Convert.ToDouble(splitStr[overallIdx + 2]);
                                intervals.Add(interval);
                                intervalValues.Add(intervalValue);
                            }
                        }
                    }
                }
            }

            var distanceName = this.MakeDistanceName(riskStr);
            if (!this.distanceNames.ContainsKey(distanceName))
            {
                this.distanceNames.Add(distanceName, distances.ToArray());
            }
            return riskCrudes;
        }

        private string MakeDistanceName(string originName)
        {
            var name = originName.Replace("RESULT NAME = ", "");
            return name;
        }
    }
}
