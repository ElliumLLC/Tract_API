namespace Tract_API.Models
{
    public class Tract
    {
        public int ID { get; set; }
        public string TractNumber { get; set; } = string.Empty;
        public string TractAltNumber { get; set; } = string.Empty;
        public string TractMapId { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string County { get; set; } = string.Empty;
        public double GrossAcres { get; set; }
        public string ShortDesc { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int ProjectId { get; set; }
        public int CustomerId { get; set; }
        public string LegalDescription { get; set; } = string.Empty;
        public int TractTypeId { get; set; }
    }
}
