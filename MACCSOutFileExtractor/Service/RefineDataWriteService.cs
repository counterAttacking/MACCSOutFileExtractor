using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Service
{
    public class RefineDataWriteService
    {
        private static string ccdfStr = "CCDF_";
        private string resultStr;
        private string distanceStr;
        private static string extStr = ".csv";
        private RefineData[] refineDatas;

        public RefineDataWriteService(object refineDatas, string result, string distanceStr)
        {
            this.refineDatas = (RefineData[])refineDatas;
            this.resultStr = result;
            this.distanceStr = distanceStr;
        }

        public void FileWrite()
        {
            var fileName = new StringBuilder();
            fileName.Append(ccdfStr);
            fileName.Append(resultStr);
            fileName.Append(" ");
            if (this.distanceStr.Contains('/'))
            {
                this.distanceStr = this.distanceStr.Replace('/', ',');
            }
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
