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
        private OutlineDataWriteService outlineDataWriteService;
        private bool isFrequency;

        public ExtractManager(OutFile[] inputFiles, bool isFrequency)
        {
            this.readService = new OutFileReadService(inputFiles);
            this.isFrequency = isFrequency;
        }

        public void Run()
        {
            this.readService.ReadOutFile();
            this.outlineDataWriteService = new OutlineDataWriteService(this.readService.GetExtractDatas(), this.readService.GetDistanceNames());
            this.outlineDataWriteService.FileWrite();
            this.mergeService = new IntervalMergeService(this.readService.GetExtractDatas(), this.readService.GetDistanceNames());
            this.mergeService.MergeInterval();
            this.refineService = new ExtractDataRefineService(this.readService.GetExtractDatas(), this.mergeService.GetMergedIntervals(),
                this.readService.GetDistanceNames(), this.isFrequency);
            this.refineService.DataRefine();
        }
    }
}
