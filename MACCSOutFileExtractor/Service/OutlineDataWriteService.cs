using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Service
{
    public class OutlineDataWriteService
    {
        private ExtractData[] extractDatas;
        private Dictionary<string, string[]> distanceNames;
        private string[] fileName;
        private string[] resultNames;
        private static string extStr = ".csv";

        public OutlineDataWriteService(object extractDatas, Dictionary<string, string[]> distanceNames)
        {
            this.extractDatas = (ExtractData[])extractDatas;
            this.distanceNames = distanceNames;
        }

        public void FileWrite()
        {
            this.SetFileName();
            this.SetResultName();

            for (var i = 0; i < this.fileName.Length; i++)
            {
                var str = new StringBuilder();
                for (var j = 0; j < this.resultNames.Length; j++)
                {
                    str.Append(this.resultNames[j]);
                    str.Append(",");

                    for (var k = 0; k < this.extractDatas.Length; k++)
                    {
                        str.Append(this.extractDatas[k].name);
                        if (k == this.extractDatas.Length - 1)
                        {
                            str.AppendLine();
                        }
                        else
                        {
                            str.Append(",");
                        }
                    }

                    for (var k = 0; k < this.distanceNames[this.resultNames[j]].Length; k++)
                    {
                        if (this.resultNames[j].Equals("HEALTH EFFECTS CASES"))
                        {
                            str.Append(this.extractDatas[0].healthOutlines[k].section);
                            str.Append(",");

                            for (var l = 0; l < this.extractDatas.Length; l++)
                            {
                                str.Append(this.extractDatas[l].healthOutlines[k].sectionValue[i]);
                                if (l == this.extractDatas.Length - 1)
                                {
                                    str.AppendLine();
                                }
                                else
                                {
                                    str.Append(",");
                                }
                            }
                        }
                        else if (this.resultNames[j].Equals("POPULATION DOSE (Sv)"))
                        {
                            str.Append(this.extractDatas[0].doseOutlines[k].section);
                            str.Append(",");

                            for (var l = 0; l < this.extractDatas.Length; l++)
                            {
                                str.Append(this.extractDatas[l].doseOutlines[k].sectionValue[i]);
                                if (l == this.extractDatas.Length - 1)
                                {
                                    str.AppendLine();
                                }
                                else
                                {
                                    str.Append(",");
                                }
                            }
                        }
                        else if (this.resultNames[j].Equals("POPULATION WEIGHTED RISK"))
                        {
                            str.Append(this.extractDatas[0].riskOutlines[k].section);
                            str.Append(",");

                            for (var l = 0; l < this.extractDatas.Length; l++)
                            {
                                str.Append(this.extractDatas[l].riskOutlines[k].sectionValue[i]);
                                if (l == this.extractDatas.Length - 1)
                                {
                                    str.AppendLine();
                                }
                                else
                                {
                                    str.Append(",");
                                }
                            }
                        }
                    }

                    if (j < this.resultNames.Length - 1)
                    {
                        str.AppendLine();
                    }
                }

                var name = new StringBuilder();
                name.Append(this.fileName[i]);
                name.Append(extStr);
                File.WriteAllText(name.ToString(), str.ToString());
            }
        }

        private void SetFileName()
        {
            var names = new List<string>();
            for (var i = 0; i < this.extractDatas[0].healthOutlines[0].menuName.Length; i++)
            {
                var name = this.extractDatas[0].healthOutlines[0].menuName[i];
                names.Add(name);
            }
            this.fileName = names.ToArray();
        }

        private void SetResultName()
        {
            var names = new List<string>
            {
                "HEALTH EFFECTS CASES",
                "POPULATION DOSE (Sv)",
                "POPULATION WEIGHTED RISK"
            };

            this.resultNames = names.ToArray();
        }
    }
}
