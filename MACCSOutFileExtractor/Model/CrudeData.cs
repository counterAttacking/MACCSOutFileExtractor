using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Model
{
    public class CrudeData
    {
        public string name
        {
            set;
            get;
        }

        public double[] interval
        {
            set;
            get;
        }

        public double[] intervalVal
        {
            set;
            get;
        }
    }
}
