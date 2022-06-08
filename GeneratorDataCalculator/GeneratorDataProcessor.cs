using GeneratorData.Model;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace GeneratorData
{
    public class GeneratorDataProcessor
    {
        private ReferenceData _referenceData;
        private GenerationReport _generationReport;
        private GenerationOutput _generationOutput = new GenerationOutput();
        private string _refDataFile;

        public GeneratorDataProcessor(string refData)
        {
            _refDataFile = refData;
            LoadReferenceData();
        }



        public bool LoadInputFile(string fileName)
        {
            Console.WriteLine($"Processing file {fileName}");
            bool success = false;
            XmlSerializer genReportSerializer = new XmlSerializer(typeof(GenerationReport));
            try
            {
                using (var reader = new XmlTextReader(fileName))
                {
                    _generationReport = (GenerationReport)genReportSerializer.Deserialize(reader);
                    success = true;
                }
            }
            catch (InvalidOperationException ioe)
            {
                Console.WriteLine(ioe.Message);
            }

            Console.WriteLine("Input file read");
            return success;
        }

        public void CalculateData()
        {
            ProcessWind();
            ProcessGas();
            ProcessCoal();
        }

        public void WriteOutput(string fileName)
        {
            Console.WriteLine($"Writing Outpue file {fileName}");
            using (var writer = new System.IO.StreamWriter(fileName))
            {
                var serializer = new XmlSerializer(_generationOutput.GetType());
                serializer.Serialize(writer, _generationOutput);
                writer.Flush();
            }
        }

        private void LoadReferenceData()
        {
            Console.WriteLine($"Loading reference data {_refDataFile}");
            XmlSerializer refDataSerializer = new XmlSerializer(typeof(ReferenceData));
            try
            {
                using (var reader = new XmlTextReader(_refDataFile))
                {
                    _referenceData = (ReferenceData)refDataSerializer.Deserialize(reader);
                }
            }
            catch (InvalidOperationException ioe)
            {
                Console.WriteLine(ioe.Message);
            }

        }
        private void ProcessCoal()
        {
            CalculateTotalGeneration(_generationReport.Coal);
            CalculateEmmissions(_generationReport.Coal);
            CalculateHeatRate(_generationReport.Coal);
        }

        private void ProcessGas()
        {
            CalculateTotalGeneration(_generationReport.Gas);
            CalculateEmmissions(_generationReport.Gas);
        }

        private void ProcessWind()
        {
            CalculateTotalGeneration(_generationReport.Wind);
        }

        private void CalculateHeatRate(CoalReportGenerator[] reportGenerator)
        {
            foreach (CoalReportGenerator wgr in reportGenerator)
            {
                _generationOutput.ActualHeatRates.Add(new GenerationOutputActualHeatRates
                {
                    Name = wgr.Name,
                    HeatRates = wgr.TotalHeatInput / wgr.ActualNetGeneration
                });
            }
        }

        private void CalculateTotalGeneration(ReportGenerator[] reportGenerator)
        {
            foreach (ReportGenerator wgr in reportGenerator)
            {
                decimal total = 0;
                foreach (GeneratorDay gd in wgr.Generation)
                {
                    total += gd.Energy * gd.Price * GetValueFactor(wgr.Name);
                }
                _generationOutput.Totals.Add(new GenerationOutputGenerator
                {
                    Name = wgr.Name,
                    Total = total
                });
            }
        }
        private void CalculateEmmissions(ReportGenerator[] reportGenerator)
        {
            foreach (ReportGenerator wgr in reportGenerator)
            {
                foreach (GeneratorDay gd in wgr.Generation)
                {
                    var emmision = gd.Energy * wgr.EmissionsRating * GetEmmissionFactor(wgr.Name);
                    if (emmision <= 0) return;
                    _generationOutput.MaxEmissionGenerators.Add(new GenerationOutputDay
                    {
                        Date = gd.Date,
                        Name = wgr.Name,
                        Emission = emmision
                    });
                }
            }
        }

        private decimal GetEmmissionFactor(string name)
        {
            switch (name.Substring(0, name.IndexOf('[')))
            {
                case "Gas":
                    return _referenceData.Factors.EmissionsFactor.Medium;
                case "Coal":
                    return _referenceData.Factors.EmissionsFactor.High;
                default:
                    break;
            }
            return 0;
        }
        private decimal GetValueFactor(string name)
        {
            //var _name = name.Substring(name.IndexOf('['));
            switch (name)
            {
                case "Wind[Offshore]":
                    return _referenceData.Factors.ValueFactor.Low;
                case "Wind[Onshore]":
                    return _referenceData.Factors.ValueFactor.High;
                default:
                    return _referenceData.Factors.ValueFactor.Medium;
            }
        }
    }
}
