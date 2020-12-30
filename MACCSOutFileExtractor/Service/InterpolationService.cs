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

                // 덮어쓰기를 진행하기 전에 0이 아닌 값이 존재하는 곳의 Interval 탐색
                var intervals = this.FindInterval(i, values);

                // 0이 아닌 값이 처음으로 발견되면 처음부터 발견된 곳까지 값 덮어쓰기
                var idx = Array.FindIndex(this.refineDatas[i].intervalVal, target => target != 0);
                this.OverWrite(i, this.refineDatas[i].intervalVal[idx], 0, idx);

                // 0이 아닌 값이 1개인 경우는 덮어쓰기만 진행
                if (values.Length == 1)
                {
                    continue;
                }

                // Linear Interpolation
                this.LinearInterpolation(i, intervals, values);
            }
        }

        private double[] FindInterval(int nth, double[] values)
        {
            var intervals = new double[values.Length];
            for (var i = 0; i < values.Length; i++)
            {
                var idx = Array.FindIndex(this.refineDatas[nth].intervalVal, target => target == values[i]);
                intervals[i] = this.refineDatas[nth].interval[idx];
            }
            return intervals;
        }

        private void OverWrite(int nth, double val, int start, int end)
        {
            for (var i = start; i < end; i++)
            {
                this.refineDatas[nth].intervalVal[i] = val;
            }
        }

        private void LinearInterpolation(int nth, double[] intervals, double[] values)
        {
            double x = 0.0;
            double xPrime = 0.0;
            double y = 0.0;
            double yPrime = 0.0;

            for (var i = 0; i < intervals.Length; i++)
            {
                // xPrime, yPrime 가지고는 계산을 진행할 수 없기 때문에
                if (i == 0)
                {
                    xPrime = intervals[i];
                    yPrime = values[i];
                    continue;
                }
                x = xPrime;
                xPrime = intervals[i];
                y = yPrime;
                yPrime = values[i];

                var start = Array.FindIndex(this.refineDatas[nth].interval, target => target == x);
                var end = Array.FindIndex(this.refineDatas[nth].interval, target => target == xPrime);
                for (var j = start; j < end; j++)
                {
                    var p = this.refineDatas[nth].interval[j];
                    this.refineDatas[nth].intervalVal[j] = this.Calculation(x, y, xPrime, yPrime, p);
                }
            }
        }

        private double Calculation(double x, double y, double xPrime, double yPrime, double p)
        {
            return (yPrime - y) * (p - x) / (xPrime - x) + y;
        }
    }
}
