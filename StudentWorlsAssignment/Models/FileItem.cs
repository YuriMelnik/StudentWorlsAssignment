namespace StudentWorlsAssignment.Models
{
    public class FileItem
    {
        private readonly string _path;
        private readonly ISet<string> _previewExtensions;

        public FileItem(string fullPath, ISet<string> previewExtentions)
        {
            _path = fullPath;
            _previewExtensions = previewExtentions;
        }

        public override string ToString()
        {
            return System.IO.Path.GetFileName(_path); // в ListBox будет только имя файла.[web:181]
        }

        public string FullPath => _path;
        public bool CanPreview => _previewExtensions.Contains(Path.GetExtension(_path).ToLowerInvariant());
    }

}




