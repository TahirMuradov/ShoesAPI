using Microsoft.AspNetCore.Identity;
using Shoes.WebAPI.Services;

namespace Shoes.Bussines
{
    public class MultilanguageIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly ErrorMessageService _errorMessageService;

        public MultilanguageIdentityErrorDescriber(ErrorMessageService errorMessageService)
        {
            _errorMessageService = errorMessageService;
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = _errorMessageService.GetKey("DuplicateEmail")
            };
        }

        public override IdentityError RecoveryCodeRedemptionFailed()
        {
            return new IdentityError
            {
                Code = nameof(RecoveryCodeRedemptionFailed),
                Description = _errorMessageService.GetKey("RecoveryCodeRedemptionFailed")
            };
        }

        public override IdentityError DuplicateRoleName(string name)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateRoleName),
                Description = _errorMessageService.GetKey("DuplicateRoleName")
            };
        }

        public override IdentityError DuplicateUserName(string name)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateUserName),
                Description = _errorMessageService.GetKey("DuplicateUserName")
            };
        }

        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(InvalidEmail),
                Description = _errorMessageService.GetKey("InvalidEmail")
            };
        }

        public override IdentityError InvalidRoleName(string name)
        {
            return new IdentityError
            {
                Code = nameof(InvalidRoleName),
                Description = _errorMessageService.GetKey("InvalidRoleName")
            };
        }

        public override IdentityError InvalidToken()
        {
            return new IdentityError
            {
                Code = nameof(InvalidToken),
                Description = _errorMessageService.GetKey("InvalidToken")
            };
        }

        public override IdentityError InvalidUserName(string name)
        {
            return new IdentityError
            {
                Code = nameof(InvalidUserName),
                Description = _errorMessageService.GetKey("InvalidUserName")
            };
        }

        public override IdentityError LoginAlreadyAssociated()
        {
            return new IdentityError
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = _errorMessageService.GetKey("LoginAlreadyAssociated")
            };
        }

        public override IdentityError PasswordMismatch()
        {
            return new IdentityError
            {
                Code = nameof(PasswordMismatch),
                Description = _errorMessageService.GetKey("PasswordMismatch")
            };
        }

        public override IdentityError PasswordRequiresDigit()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresDigit),
                Description = _errorMessageService.GetKey("PasswordRequiresDigit")
            };
        }

        public override IdentityError PasswordRequiresLower()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresLower),
                Description = _errorMessageService.GetKey("PasswordRequiresLower")
            };
        }

        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = _errorMessageService.GetKey("PasswordRequiresUpper")
            };
        }

        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = _errorMessageService.GetKey("PasswordTooShort").Value.Replace("{0}", length.ToString())
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = _errorMessageService.GetKey("PasswordRequiresNonAlphanumeric")
            };
        }

        public override IdentityError UserAlreadyInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserAlreadyInRole),
                Description = _errorMessageService.GetKey("UserAlreadyInRole").Value.Replace("{0}", role)
            };
        }

        public override IdentityError UserLockoutNotEnabled()
        {
            return new IdentityError
            {
                Code = nameof(UserLockoutNotEnabled),
                Description = _errorMessageService.GetKey("UserLockoutNotEnabled")
            };
        }

        public override IdentityError UserNotInRole(string role)
        {
            return new IdentityError
            {
                Code = nameof(UserNotInRole),
                Description = _errorMessageService.GetKey("UserNotInRole").Value.Replace("{0}", role)
            };
        }

    }
}
