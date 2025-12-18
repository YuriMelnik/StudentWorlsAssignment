using StudentWorlsAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentWorlsAssignment.Services
{
    internal sealed class StudentFileService
    {
        private readonly string _baseOutputDir;
        private readonly HashSet<string> _allowedExtentions;

        public StudentFileService(string baseOutputDir, IEnumerable<string> allowedExtentions)
        {
            _baseOutputDir = baseOutputDir ?? throw new ArgumentNullException(nameof(baseOutputDir));
            _allowedExtentions = new HashSet<string>(allowedExtentions.Select(e => e.ToLowerInvariant()));

        }
        public bool TryGetStudentfiles(string studentName, out List<FileItem> files)
        {
            files = new List<FileItem>();

            if (string.IsNullOrWhiteSpace(studentName) || string.IsNullOrEmpty(_baseOutputDir))
            {
                return false;
            }
            string studentFolder = Path.Combine(_baseOutputDir, studentName);
            if (!Directory.Exists(studentFolder))
            {
                return false;
            }

            var allFiles = Directory.GetFiles(studentFolder, "*", SearchOption.AllDirectories);

            files = [.. allFiles.Where(f => _allowedExtentions.Contains(Path.GetExtension(f).ToLowerInvariant())).Select(f => new FileItem(f, _allowedExtentions))];

            return files.Count > 0;
        }
    }

}
