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

        private static string probStr = "PROB                           QUANTILES                             PEAK      PEAK  PEAK";
        private static string healthStr = "HEALTH EFFECTS CASES";
        private static string doseStr = "POPULATION DOSE (Sv)";
        private static string riskStr = "POPULATION WEIGHTED RISK";
        private static string resultHealthStr = "RESULT NAME = HEALTH EFFECTS CASES";
        private static string resultDoseStr = "RESULT NAME = POPULATION DOSE (Sv)";
        private static string resultRiskStr = "RESULT NAME = POPULATION WEIGHTED RISK";
        private static string resultPeakStr = "RESULT NAME = PEAK DOSE FOUND ON SPATIAL GRID (Sv)";
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
                var healthOutlines = new List<OutlineData>();
                var doseOutlines = new List<OutlineData>();
                var riskOutlines = new List<OutlineData>();

                using (var fileStream = new FileStream(inputFiles[i].fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        healthOutlines = this.ReadHealthOutline(streamReader);
                        doseOutlines = this.ReadDoseOutline(streamReader);
                        riskOutlines = this.ReadRiskOutline(streamReader);
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
                    riskCrudes = riskCrudes.ToArray(),
                    healthOutlines = healthOutlines.ToArray(),
                    doseOutlines = doseOutlines.ToArray(),
                    riskOutlines = riskOutlines.ToArray(),
                };
                extracts.Add(extract);
            }

            this.extractDatas = extracts.ToArray();
        }

        private List<OutlineData> ReadHealthOutline(StreamReader streamReader)
        {
            var isProbStrFound = false;
            var isHealthStrFound = false;

            var menus = new List<string>();
            var outlines = new List<OutlineData>();

            while (!streamReader.EndOfStream)
            {
                var readLine = streamReader.ReadLine();

                if (readLine.Contains(probStr))
                {
                    isProbStrFound = true;
                    continue;
                }

                if (isProbStrFound == true)
                {
                    readLine = readLine.Trim();
                    if (String.IsNullOrWhiteSpace(readLine))
                    {
                        break;
                    }
                    else if (readLine.Contains("NON-ZERO"))
                    {
                        var tmp = readLine.Split(' ');
                        for (var i = 0; i < tmp.Length; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(tmp[i]))
                            {
                                menus.Add(tmp[i]);
                            }
                        }
                        continue;
                    }
                    else if (readLine.Contains(healthStr))
                    {
                        isHealthStrFound = true;
                        continue;
                    }

                    if (isHealthStrFound == true)
                    {
                        var tmp = readLine.Split(new string[] { "km" }, StringSplitOptions.None);
                        var value = tmp[1].Trim().Split(' ');
                        var values = new List<double>();
                        for (var i = 0; i < value.Length; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(value[i]))
                            {
                                values.Add(Convert.ToDouble(value[i]));
                            }
                        }

                        var data = new OutlineData
                        {
                            section = tmp[0] + "km",
                            sectionValue = values.ToArray(),
                            menuName = menus.ToArray()
                        };

                        outlines.Add(data);
                    }
                }
            }

            return outlines;
        }

        private List<OutlineData> ReadDoseOutline(StreamReader streamReader)
        {
            var isProbStrFound = false;
            var isDoseStrFound = false;

            var menus = new List<string>();
            var outlines = new List<OutlineData>();

            while (!streamReader.EndOfStream)
            {
                var readLine = streamReader.ReadLine();

                if (readLine.Contains(probStr))
                {
                    isProbStrFound = true;
                    continue;
                }

                if (isProbStrFound == true)
                {
                    readLine = readLine.Trim();
                    if (String.IsNullOrWhiteSpace(readLine))
                    {
                        if (isDoseStrFound == true)
                        {
                            break;
                        }
                        else
                        {
                            isProbStrFound = false;
                            menus.Clear();
                            continue;
                        }
                    }
                    else if (readLine.Contains("NON-ZERO"))
                    {
                        var tmp = readLine.Split(' ');
                        for (var i = 0; i < tmp.Length; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(tmp[i]))
                            {
                                menus.Add(tmp[i]);
                            }
                        }
                        continue;
                    }
                    else if (readLine.Contains(doseStr))
                    {
                        isDoseStrFound = true;
                        continue;
                    }

                    if (isDoseStrFound == true)
                    {
                        var tmp = readLine.Split(new string[] { "km" }, StringSplitOptions.None);
                        var value = tmp[1].Trim().Split(' ');
                        var values = new List<double>();
                        for (var i = 0; i < value.Length; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(value[i]))
                            {
                                values.Add(Convert.ToDouble(value[i]));
                            }
                        }

                        var data = new OutlineData
                        {
                            section = tmp[0] + "km",
                            sectionValue = values.ToArray(),
                            menuName = menus.ToArray()
                        };

                        outlines.Add(data);
                    }
                }
            }

            return outlines;
        }

        private List<OutlineData> ReadRiskOutline(StreamReader streamReader)
        {
            var isProbStrFound = false;
            var isRiskStrFound = false;

            var menus = new List<string>();
            var outlines = new List<OutlineData>();

            while (!streamReader.EndOfStream)
            {
                var readLine = streamReader.ReadLine();

                if (readLine.Contains(probStr))
                {
                    isProbStrFound = true;
                    continue;
                }

                if (isProbStrFound == true)
                {
                    readLine = readLine.Trim();
                    if (String.IsNullOrWhiteSpace(readLine))
                    {
                        if (isRiskStrFound == true)
                        {
                            break;
                        }
                        else
                        {
                            isProbStrFound = false;
                            menus.Clear();
                            continue;
                        }
                    }
                    else if (readLine.Contains("NON-ZERO"))
                    {
                        var tmp = readLine.Split(' ');
                        for (var i = 0; i < tmp.Length; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(tmp[i]))
                            {
                                menus.Add(tmp[i]);
                            }
                        }
                        continue;
                    }
                    else if (readLine.Contains(riskStr))
                    {
                        isRiskStrFound = true;
                        continue;
                    }

                    if (isRiskStrFound == true)
                    {
                        var tmp = readLine.Split(new string[] { "km" }, StringSplitOptions.None);
                        var value = tmp[1].Trim().Split(' ');
                        var values = new List<double>();
                        for (var i = 0; i < value.Length; i++)
                        {
                            if (!String.IsNullOrWhiteSpace(value[i]))
                            {
                                values.Add(Convert.ToDouble(value[i]));
                            }
                        }

                        var data = new OutlineData
                        {
                            section = tmp[0] + "km",
                            sectionValue = values.ToArray(),
                            menuName = menus.ToArray()
                        };

                        outlines.Add(data);
                    }
                }
            }

            return outlines;
        }

        private List<OutData> ReadHealthSection(StreamReader streamReader)
        {
            var isResultHealthStrFound = false;
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

                if (readLine.Contains(resultHealthStr))
                {
                    /* 
                     * 'RESULT NAME = HEALTH EFFECTS CASES'에 해당하는 부분에 찾고자 하는 값들이 존재
                     * 이 분기문 밑으로 넘어가면 안 되기 때문에 continue 설정
                     */
                    isResultHealthStrFound = true;
                    continue;
                }
                else if (readLine.Contains(resultDoseStr))
                {
                    /* 
                     * 'RESULT NAME = POPULATION DOSE (Sv)' 검색된 줄 밑으로 찾고자 하는 값들은
                     * ReadDoseSection()에서 처리
                     */
                    break;
                }

                if (isResultHealthStrFound == true)
                {
                    /* 
                     * resultHealthStr 바로 다음 줄에는
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
                            isResultHealthStrFound = false;
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

            var distanceName = this.MakeDistanceName(resultHealthStr);
            if (!this.distanceNames.ContainsKey(distanceName))
            {
                this.distanceNames.Add(distanceName, distances.ToArray());
            }
            return healthCrudes;
        }

        private List<OutData> ReadDoseSection(StreamReader streamReader)
        {
            var isResultDoseStrFound = true;
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

                if (readLine.Contains(resultDoseStr))
                {
                    /*
                     * 'RESULT NAME = POPULATION DOSE (Sv)'이 나타나는 지점부터 찾고자 하는 값들이 존재
                     * 이 분기문 밑으로 넘어가면 안되기 때문에 continnue 설정
                     */
                    isResultDoseStrFound = true;
                    continue;
                }
                else if (readLine.Contains(resultRiskStr))
                {
                    /*
                     * 'RESULT NAME = POPULATION WEIGHTED RISK' 검색된 줄 밑으로 찾고자 하는 값들은
                     * ReadRiskSection()에서 처리
                     */
                    break;
                }

                if (isResultDoseStrFound == true)
                {
                    /*
                     * resultDoseStr 바로 다음 줄에는 'L-ICRP60ED  TOT LIF'이 존재하며
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
                            isResultDoseStrFound = false;
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

            var distanceName = this.MakeDistanceName(resultDoseStr);
            if (!this.distanceNames.ContainsKey(distanceName))
            {
                this.distanceNames.Add(distanceName, distances.ToArray());
            }
            return doseCrudes;
        }

        private List<OutData> ReadRiskSection(StreamReader streamReader)
        {
            var isResultRiskStrFound = true;
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

                if (readLine.Contains(resultRiskStr))
                {
                    /*
                     * 'RESULT NAME = POPULATION WEIGHTED RISK'이 나타나는 지점부터 찾고자 하는 값들이 존재
                     * 이 분기문 밑으로 넘어가면 안되기 때문에 continnue 설정
                     */
                    isResultRiskStrFound = true;
                    continue;
                }
                else if (readLine.Contains(resultPeakStr))
                {
                    // 추출할 값 모두 추출
                    break;
                }

                if (isResultRiskStrFound == true)
                {
                    /*
                     * resultRiskStr 바로 다음 줄에는
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
                            isResultRiskStrFound = false;
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

            var distanceName = this.MakeDistanceName(resultRiskStr);
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
