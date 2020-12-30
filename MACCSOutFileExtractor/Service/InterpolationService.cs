using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Service
{
    public class InterpolationService
    {
        private RefineData[] refineDatas;

        public InterpolationService(object refineDatas)
        {
            this.refineDatas = (RefineData[])refineDatas;
        }

        public object GetRefineData() => this.refineDatas.Clone();

        public void Interpolation()
        {
            for (var i = 0; i < this.refineDatas.Length; i++)
            {
                // 0이 아닌 값이 존재하는 곳까지 값들을 덮어쓰기 전에 전체 배열 중 0이 아닌 값들 탐색
                var values = Array.FindAll(this.refineDatas[i].intervalVal, target => target != 0);
                if (values == null || values.Length == 0)
                {
                    continue;
                }

                // 0이 아닌 값이 처음으로 발견되면 처음부터 발견된 곳까지 값 덮어쓰기
                var idx = Array.FindIndex(this.refineDatas[i].intervalVal, target => target != 0);
                this.OverWrite(i, this.refineDatas[i].intervalVal[idx], 0, idx);

                // Linear Interpolation
            }
        }

        private void OverWrite(int nth, double val, int start, int end)
        {
            for (var i = start; i < end; i++)
            {
                this.refineDatas[nth].intervalVal[i] = val;
            }
        }
    }
}
