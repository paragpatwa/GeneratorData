namespace GeneratorData.Model
{
    public class GenerationReport
    {

        private WindReportGenerator[] windField;

        private GasReportGenerator[] gasField;

        private CoalReportGenerator[] coalField;

        [System.Xml.Serialization.XmlArrayItemAttribute("WindGenerator", IsNullable = false)]
        public WindReportGenerator[] Wind
        {
            get => this.windField;
            set => this.windField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("GasGenerator", IsNullable = false)]
        public GasReportGenerator[] Gas
        {
            get => this.gasField;
            set => this.gasField = value;
        }

        [System.Xml.Serialization.XmlArrayItemAttribute("CoalGenerator", IsNullable = false)]
        public CoalReportGenerator[] Coal
        {
            get => this.coalField;
            set => this.coalField = value;
        }
    }
}

