namespace StudentWorlsAssignment.Models
{
    public class FileItem
    {
        public string FullPath { get; }

        public FileItem(string fullPath)
        {
            FullPath = fullPath;
        }

        public override string ToString()
        {
            return Path.GetFileName(FullPath); // в ListBox будет только имя файла.[web:181]
        }
    }

}




