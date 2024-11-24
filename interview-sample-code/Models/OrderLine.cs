namespace interview_sample_code.Models
{
    public class OrderLine

    {

        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }

    }


}
