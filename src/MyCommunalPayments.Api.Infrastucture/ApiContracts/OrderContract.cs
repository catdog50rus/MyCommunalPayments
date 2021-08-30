namespace MyCommunalPayments.Api.Infrastucture.ApiContracts
{
    public class OrderContract
    {
        public int IdOrder { get; set; }

        public string FileName { get; set; }

        public byte[] OrderScreen { get; set; }
    }
}
