using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Service
{
    public class CsvFileWriteService
    {
        private static string healthStr = "HEALTH EFFECTS CASES ";
        private string distanceStr;
        private static string extStr = ".csv";
        private RefineData[] refineDatas;

        public CsvFileWriteService(object refineDatas, string distanceStr)
        {
            this.refineDatas = (RefineData[])refineDatas;
            this.distanceStr = distanceStr;
        }

        public void FileWrite()
        {
            var fileName = new StringBuilder();
            fileName.Append(healthStr);
            this.distanceStr = this.distanceStr.Replace('/', ',');
            fileName.Append(this.distanceStr);
            fileName.Append(extStr);

            var str = new StringBuilder();
            str.Append("");
            str.Append(",");
            for (var i = 0; i < this.refineDatas.Length; i++)
            {
                str.Append(this.refineDatas[i].name);
                if (i == this.refineDatas.Length - 1)
                {
                    str.AppendLine();
                    break;
                }
                str.Append(",");
            }

            var len = this.refineDatas[0].interval.Length;
            for (var i = 0; i < len; i++)
            {
                str.Append(this.refineDatas[0].interval[i]);
                str.Append(",");
                for (var j = 0; j < this.refineDatas.Length; j++)
                {
                    str.Append(this.refineDatas[j].intervalVal[i]);
                    if (j < this.refineDatas.Length - 1)
                    {
                        str.Append(",");
                    }
                }
                if (i < len - 1)
                {
                    str.AppendLine();
                }
            }
            File.WriteAllText(fileName.ToString(), str.ToString());
        }
    }
}
