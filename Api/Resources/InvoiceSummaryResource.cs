namespace Api.Resources
{
    public class InvoiceSummaryResource
    {
        public int Id { get; set; }
        public int Correlative { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public string BillingNo { get; set; }
        public string Customer { get; set; }
        public float Total { get; set; }
    }
}