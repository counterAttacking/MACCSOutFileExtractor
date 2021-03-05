using MACCSOutFileExtractor.Model;
using MACCSOutFileExtractor.Service;
using MACCSOutFileExtractor.View;
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
        private MainForm frmMain;

        public ExtractManager(MainForm frmMain, OutFile[] inputFiles, bool isFrequency)
        {
            this.frmMain = frmMain;
            this.readService = new OutFileReadService(inputFiles);
            this.isFrequency = isFrequency;
        }

        public async Task Run()
        {
            await Task.Run(() =>
            {
                var str = new StringBuilder();
                str.Append(DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss]   "));
                str.AppendLine("Start reading loaded Out Files");
                this.frmMain.PrintStatus(str);

                this.readService.ReadOutFile();

                str.Clear();
                str.Append(DateTime.Now.ToString("[yyyy-MM-dd-HH:mm:ss]   "));
                str.AppendLine("Finished reading loaded Out Files");
                this.frmMain.PrintStatus(str);

                this.outlineDataWriteService = new OutlineDataWriteService(this.readService.GetExtractDatas(), this.readService.GetDistanceNames());
                this.outlineDataWriteService.FileWrite();
                this.mergeService = new IntervalMergeService(this.readService.GetExtractDatas(), this.readService.GetDistanceNames());
                this.mergeService.MergeInterval();
                this.refineService = new ExtractDataRefineService(this.readService.GetExtractDatas(), this.mergeService.GetMergedIntervals(),
                    this.readService.GetDistanceNames(), this.isFrequency);
                this.refineService.DataRefine();
            });
        }
    }
}
