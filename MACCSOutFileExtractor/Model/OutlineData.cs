using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Model
{
    public class OutlineData
    {
        public string section
        {
            set;
            get;
        }

        public double[] sectionValue
        {
            set;
            get;
        }

        public string[] menuName
        {
            set;
            get;
        }
    }
}
