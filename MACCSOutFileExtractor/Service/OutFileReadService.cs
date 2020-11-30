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
        private static string healthStr = "RESULT NAME = HEALTH EFFECTS CASES";
        private static string populationStr = "RESULT NAME = POPULATION DOSE (Sv)";
        private static string fatStr = "FAT/TOTAL";
        private static string overallStr = "OVERALL";
        private static string overallEndStr = "SOURCE TERM";

        public OutFileReadService(OutFile[] inputFiles)
        {
            this.inputFiles = inputFiles;
        }

        public ExtractData[] GetExtractDatas() => this.extractDatas;

        public void ReadOutFile()
        {
            var inputFileLen = this.inputFiles.Length;
            var extracts = new List<ExtractData>();
            for (var i = 0; i < inputFileLen; i++)
            {
                var ishealthStrFound = false;
                var isOverallStrFound = false;
                var overallIdx = 0;
                string name = null;
                var intervals = new List<double>();
                var intervalValues = new List<double>();
                var crudes = new List<CrudeData>();
                using (FileStream fileStream = new FileStream(inputFiles[i].fullPath, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8, false))
                    {
                        while (!streamReader.EndOfStream)
                        {
                            var readLine = streamReader.ReadLine();
                            /* 'RESULT NAME = HEALTH EFFECTS CASES'에 해당하는 부분에 찾고자 하는 값들이 존재
                             * 이 분기문 밑으로 넘어가면 안 되기 때문에 continue 설정
                             */
                            if (readLine.Contains(healthStr))
                            {
                                ishealthStrFound = true;
                                continue;
                            }

                            /* RESULT NAME = POPULATION DOSE (Sv)이 나오면
                             * RESULT NAME = HEALTH EFFECTS CASES에 해당하는 값들이 없기 때문에
                             */
                            if (readLine.Contains(populationStr))
                            {
                                break;
                            }

                            /* healthStr 바로 다음 줄에는
                             * EAL FAT/TOTAL, CAN FAT/TOTAL에 대한 내용 존재
                             * OVERALL이 존재하는 위치에 해당하는 값들만 추출
                             */
                            if (ishealthStrFound == true)
                            {
                                // EAL FAT/TOTAL, CAN FAT/TOTAL에 대한 내용 처리
                                if (readLine.Contains(fatStr))
                                {
                                    name = readLine.TrimStart();
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
                                        var crude = new CrudeData()
                                        {
                                            name = name,
                                            interval = intervals.ToArray(),
                                            intervalVal = intervalValues.ToArray()
                                        };
                                        crudes.Add(crude);
                                        intervals.Clear();
                                        intervalValues.Clear();
                                        ishealthStrFound = false;
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
                    }
                }
                var extract = new ExtractData()
                {
                    name = inputFiles[i].name,
                    crudes = crudes.ToArray()
                };
                extracts.Add(extract);
            }
            this.extractDatas = extracts.ToArray();
        }
    }
}
