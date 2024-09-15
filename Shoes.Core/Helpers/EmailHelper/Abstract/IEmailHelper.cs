using Shoes.Core.Utilites.Results.Abstract;

namespace Shoes.Core.Helpers.EmailHelper.Abstract
{
    public interface IEmailHelper
    {
        public Task<IResult> SendEmailAsync(string userEmail, string confirmationLink, string UserName);
        public Task<IResult> SendEmailPdfAsync(string userEmail, string UserName, string pdfLink);
    }
}
