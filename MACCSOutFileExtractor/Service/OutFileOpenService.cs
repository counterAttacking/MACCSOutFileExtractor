using MACCSOutFileExtractor.Model;
using MACCSOutFileExtractor.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MACCSOutFileExtractor.Service
{
    public class OutFileOpenService
    {
        private List<OutFile> files;
        private static string targetStr = ".out";

        private OutFileOpenService()
        {
            this.files = new List<OutFile>();
        }

        private static readonly Lazy<OutFileOpenService> outFileOpenService = new Lazy<OutFileOpenService>(() => new OutFileOpenService());

        public static OutFileOpenService GetOutFileOpenService
        {
            get
            {
                return outFileOpenService.Value;
            }
        }

        public List<OutFile> GetFiles() => this.files;

        /// <summary>
        /// Out 파일들을 직접 선택하는 경우
        /// </summary>
        /// <param name="fileNames">선택한 파일들</param>
        public void OpenFile(string[] fileNames)
        {
            try
            {
                foreach (var file in fileNames)
                {
                    var dividedFile = this.DivideFilePath(file);
                    this.files.Add(dividedFile);
                }
            }
            catch (Exception ex)
            {
                var frmErrorMsg = new ErrorMessageForm(ex.ToString());
                frmErrorMsg.Show();
                return;
            }
        }

        /// <summary>
        /// Out 파일이 존재하는 디텍토리를 선택하는 경우
        /// </summary>
        /// <param name="fileName">선택한 디렉토리</param>
        public void OpenFile(string fileName)
        {
            var directoryInfo = new DirectoryInfo(fileName);
            if (directoryInfo.GetDirectories().Length > 0)
            {
                foreach (var dir in directoryInfo.GetDirectories())
                {
                    this.DirFileSearch(dir.FullName);
                }
            }

            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Extension.Equals(targetStr))
                {
                    try
                    {
                        var dividedFile = this.DivideFilePath(file.FullName);
                        this.files.Add(dividedFile);
                    }
                    catch (Exception ex)
                    {
                        var frmErrorMsg = new ErrorMessageForm(ex.ToString());
                        frmErrorMsg.Show();
                        return;
                    }
                }
            }
        }

        public void ClearList()
        {
            if (MessageBox.Show("Are you sure you want to delete?", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            this.files.Clear();
        }

        /// <summary>
        /// 파일이 입력이 되면
        /// 순수한 파일 이름, 파일이 속한 디렉토리, 파일의 전체경로
        /// 로 구분하여 List에 저장할 수 있도록
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private OutFile DivideFilePath(string filePath)
        {
            var file = new OutFile
            {
                name = Path.GetFileName(filePath),
                path = Path.GetDirectoryName(filePath),
                fullPath = filePath
            };
            return file;
        }

        /// <summary>
        /// 디렉토리 안에 디렉토리가 존재할 가능성이 있으므로
        /// 재귀호출로 디렉토리를 탐색
        /// Out 파일이 존재하면 Out 파일을 List에 저장
        /// </summary>
        /// <param name="dirPath"></param>
        private void DirFileSearch(string dirPath)
        {
            foreach (var dir in Directory.GetDirectories(dirPath))
            {
                this.DirFileSearch(dir);
            }

            var directoryInfo = new DirectoryInfo(dirPath);
            foreach (var file in directoryInfo.GetFiles())
            {
                if (file.Extension.Equals(targetStr))
                {
                    try
                    {
                        var dividedFile = this.DivideFilePath(file.FullName);
                        this.files.Add(dividedFile);
                    }
                    catch (Exception ex)
                    {
                        var frmErrorMsg = new ErrorMessageForm(ex.ToString());
                        frmErrorMsg.Show();
                        return;
                    }
                }
            }
        }
    }
}
