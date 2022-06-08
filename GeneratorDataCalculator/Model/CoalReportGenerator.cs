namespace GeneratorData.Model
{
    public class CoalReportGenerator :ReportGenerator
    {
        public decimal TotalHeatInput { get; set; }

        public decimal ActualNetGeneration { get; set; }

        public override decimal EmissionsRating { get; set; }
    }
}



