namespace Shoes.Core.Helpers
{
    public static  class GenerateCouponCode
    {
        
        public static string GenerateCouponCodeFromGuid()
        {
            Guid guid = Guid.NewGuid();
            string code = guid.ToString("N").Substring(0, 10);
            return code.ToUpper();
        }


    }
}
