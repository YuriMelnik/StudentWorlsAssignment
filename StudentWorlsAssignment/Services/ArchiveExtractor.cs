using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using System.IO.Compression;

namespace StudentWorlsAssignment.Services
{
    public sealed class ArchiveExtractor
    {
        public void ExtractPerStudent(string archivePath, string baseOutputDir)
        {
            if (string.IsNullOrWhiteSpace(archivePath))
                throw new ArgumentException("Archive path is required.", nameof(archivePath));
            if (string.IsNullOrWhiteSpace(baseOutputDir))
                throw new ArgumentException("Output directory is required.", nameof(baseOutputDir));

            Directory.CreateDirectory(baseOutputDir);

            string ext = Path.GetExtension(archivePath).ToLowerInvariant();

            if (ext == ".zip")
            {
                ExtractZipPerStudent(archivePath, baseOutputDir);
            }
            else if (ext == ".rar")
            {
                ExtractRarPerStudent(archivePath, baseOutputDir);
            }
            else
            {
                throw new NotSupportedException("Поддерживаются только ZIP и RAR архивы.");
            }
        }

        private void ExtractRarPerStudent(string archivePath, string baseOutputDir)
        {
            string tempRarPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".rar");
            File.Copy(archivePath, tempRarPath, overwrite: true);

            using var archive = RarArchive.Open(tempRarPath);
            //using var archive = RarArchive.Open(archivePath);

            foreach (var entry in archive.Entries.Where(e => !e.IsDirectory))
            {
                string innerFileName = Path.GetFileName(entry.Key)!;
                string studentName = Path.GetFileNameWithoutExtension(innerFileName);

                string studentFolder = Path.Combine(baseOutputDir, studentName);
                Directory.CreateDirectory(studentFolder);

                string destPath = Path.Combine(studentFolder, innerFileName);
                string? destDir = Path.GetDirectoryName(destPath);
                if (!string.IsNullOrEmpty(destDir))
                    Directory.CreateDirectory(destDir);

                entry.WriteToFile(destPath, new ExtractionOptions
                {
                    ExtractFullPath = true,
                    Overwrite = true
                });
            }
            // delete temp copy
            try
            {
                File.Delete(tempRarPath);
            }
            catch
            {
                // если не удалился — не критично
            }
        }

        private void ExtractZipPerStudent(string archivePath, string baseOutputDir)
        {
            using var archive = ZipFile.OpenRead(archivePath);

            foreach (var entry in archive.Entries)
            {
                if (string.IsNullOrEmpty(entry.Name))
                    continue;

                string innerFileName = entry.Name;
                string studentName = Path.GetFileNameWithoutExtension(innerFileName);
                studentName = studentName.Split(['-', '_'])[0];

                string studentFolder = Path.Combine(baseOutputDir, studentName);
                Directory.CreateDirectory(studentFolder);

                string tempPath = Path.Combine(baseOutputDir, innerFileName);
                entry.ExtractToFile(tempPath, overwrite: true);

                string innerExt = Path.GetExtension(tempPath).ToLowerInvariant();

                if (innerExt == ".zip")
                {
                    ZipFile.ExtractToDirectory(tempPath, studentFolder, overwriteFiles: true);
                    TryDeleteFile(tempPath);
                }
                else if (innerExt == ".rar")
                {
                    using var rar = RarArchive.Open(tempPath);
                    foreach (var rarEntry in rar.Entries.Where(e => !e.IsDirectory))
                    {
                        rarEntry.WriteToDirectory(studentFolder, new ExtractionOptions
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                    TryDeleteFile(tempPath);
                }
                else
                {
                    string destPath = Path.Combine(studentFolder, innerFileName);
                    if (File.Exists(destPath))
                        File.Delete(destPath);
                    File.Move(tempPath, destPath);
                }
            }
        }
        private void TryDeleteFile(string path, int retries = 3, int delayMs = 100)
        {
            for (int i = 0; i < retries; i++)
            {
                try
                {
                    if (File.Exists(path))
                        File.Delete(path);
                    return;
                }
                catch (IOException)
                {
                    if (i == retries - 1)
                        return; // сдаёмся тихо
                    Thread.Sleep(delayMs);
                }
                catch (UnauthorizedAccessException)
                {
                    if (i == retries - 1)
                        return;
                    Thread.Sleep(delayMs);
                }
            }
        }

    }
}
