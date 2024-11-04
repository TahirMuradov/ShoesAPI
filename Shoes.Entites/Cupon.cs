namespace Shoes.Entites
{
    public class Cupon
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal DisCountPercent { get; set; }
        public List<CategoryCupon>? CategoryCupons { get; set; }
        public List<SubCategoryCupon>? SubCategoryCupons { get; set; }
        public List<UserCupon>?  UserCupons { get; set; }
        public List<ProductCupon>? ProductCupons { get; set; }

    }
}
