using GeneratorData;
using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace GeneratorDataConsole
{
    class Program
    {
        private static string _inputDir;
        private static string _outputDir;
        private static string _archiveDir;
        private static string _referenceDataDir;
        private static string _referenceDataFile;
        private static GeneratorDataProcessor _gdc;

        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            _inputDir = configuration.GetSection("inputDir").Value;
            _outputDir = configuration.GetSection("outputDir").Value;
            _archiveDir = configuration.GetSection("archiveDir").Value;
            _referenceDataDir = configuration.GetSection("referenceDataDir").Value;
            _referenceDataFile = configuration.GetSection("referenceDataFile").Value;

            using var fileSystemWatcher = new FileSystemWatcher(_inputDir);
            fileSystemWatcher.NotifyFilter = NotifyFilters.LastWrite
                                             | NotifyFilters.CreationTime
                                             | NotifyFilters.FileName;
            fileSystemWatcher.Filter = configuration.GetSection("inputFileFilter").Value;
            fileSystemWatcher.Created += FileSystemWatcher_Created;
            fileSystemWatcher.EnableRaisingEvents = true;

            Console.ReadLine();
        }

        private static void FileSystemWatcher_Created(object sender, FileSystemEventArgs e)
        {
            var filetoProcess = e.FullPath;
            var archiveFile = Path.Combine(_archiveDir, Path.GetFileName(filetoProcess));
            var outputFile = Path.Combine(_outputDir, Path.GetFileNameWithoutExtension(filetoProcess) + "-Result.xml");
            var refData = Path.Combine(_referenceDataDir, _referenceDataFile);

            _gdc = _gdc == null ? new GeneratorDataProcessor(refData) : _gdc;
            if (_gdc.LoadInputFile(filetoProcess))
            {
                _gdc.CalculateData();
                _gdc.WriteOutput(outputFile);
            }
            try
            {
                File.Move(filetoProcess, archiveFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
