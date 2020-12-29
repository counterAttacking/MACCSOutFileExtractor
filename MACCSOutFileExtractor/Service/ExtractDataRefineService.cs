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

        public ExtractDataRefineService(object extractDatas, object intervals)
        {
            this.extractDatas = (ExtractData[])extractDatas;
            this.intervals = (double[][])intervals;
        }

        public void DataRefine()
        {

        }
    }
}
