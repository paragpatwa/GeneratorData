namespace GeneratorData.Model
{
    public class ReferenceData
    {
        private ReferenceDataFactors factorsField;
        public ReferenceDataFactors Factors
        {
            get => this.factorsField;
            set => this.factorsField = value;
        }
    }

    public class ReferenceDataFactors
    {
        private ReferenceDataFactor valueFactorField;
        private ReferenceDataFactor emissionsFactorField;
        public ReferenceDataFactor ValueFactor
        {
            get => this.valueFactorField;
            set => this.valueFactorField = value;
        }
        public ReferenceDataFactor EmissionsFactor
        {
            get => this.emissionsFactorField;
            set => this.emissionsFactorField = value;
        }
    }

   
}
