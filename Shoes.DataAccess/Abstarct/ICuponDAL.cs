using Shoes.Core.Utilites.Results.Abstract;

namespace Shoes.DataAccess.Abstarct
{
    public interface ICuponDAL
    {
        public IResult AddedSpecificCuponForUser();
        public IResult AddedSpecificCuponForCategory();
        public IResult AddedSpecificCuponForSubCategory();
        public IResult AddedSpecificCuponForProduct();

    }
}
