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
        private ExtractDataRefineService refineService;

        public ExtractManager(OutFile[] inputFiles)
        {
            this.readService = new OutFileReadService(inputFiles);
        }

        public void Run()
        {
            this.readService.ReadOutFile();
            this.mergeService = new IntervalMergeService(this.readService.GetExtractDatas(), this.readService.GetDistances());
            this.mergeService.MergeInterval();
            this.refineService = new ExtractDataRefineService(this.readService.GetExtractDatas(), this.mergeService.GetMergedInterval());
        }
    }
}
