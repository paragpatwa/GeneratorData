namespace GeneratorData.Model
{
    public class ReportGenerator 
    {
        public string Name { get; set; }
        [System.Xml.Serialization.XmlArrayItemAttribute("Day", IsNullable = false)]
        public GeneratorDay[] Generation { get; set; }
        public virtual decimal EmissionsRating { get; set; }
    }
}
