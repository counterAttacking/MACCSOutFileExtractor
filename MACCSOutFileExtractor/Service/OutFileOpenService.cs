using MACCSOutFileExtractor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MACCSOutFileExtractor.Service
{
    public class OutFileOpenService
    {
        private List<OutFile> files;

        public OutFileOpenService()
        {
            this.files = new List<OutFile>();
        }

        public List<OutFile> GetFiles() => this.files;
    }
}
