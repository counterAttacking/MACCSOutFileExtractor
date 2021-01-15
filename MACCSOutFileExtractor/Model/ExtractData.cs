using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Model
{
    public class ExtractData
    {
        public string name
        {
            set;
            get;
        }

        public OutData[] healthCrudes
        {
            set;
            get;
        }

        public OutData[] doseCrudes
        {
            set;
            get;
        }

        public OutData[] riskCrudes
        {
            set;
            get;
        }
    }
}
