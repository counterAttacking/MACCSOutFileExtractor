using MACCSOutFileExtractor.Model;
using MACCSOutFileExtractor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Manager
{
    public class ExtractManager
    {
        private OutFileReadService readService;
        private IntervalMergeService mergeService;

        public ExtractManager(OutFile[] inputFiles)
        {
            this.readService = new OutFileReadService(inputFiles);
        }

        public void Run()
        {
            this.readService.ReadOutFile();
            this.mergeService = new IntervalMergeService(this.readService.GetExtractDatas(), this.readService.GetDistances());
            this.mergeService.MergeInterval();
        }
    }
}
