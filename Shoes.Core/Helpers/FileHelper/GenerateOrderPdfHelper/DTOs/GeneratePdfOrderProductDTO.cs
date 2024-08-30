namespace Shoes.Core.Helpers.FileHelper.GenerateOrderPdfHelper.DTOs
{
    public class GeneratePdfOrderProductDTO
    {
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public int size { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
