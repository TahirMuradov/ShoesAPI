

namespace Shoes.Bussines.AuthStatusMessages
{
    public static class AuthStatusMessage
    {
    

        public static Dictionary<string,string> UserNotFound
        {
            get
            {
                return new Dictionary<string, string>()
                {
 { "az", "İstifadəçi tapılmadı!" },
  { "ru", "Пользователь не найден!" },
      { "en", "User not found!" },
                };
            }
        }
        public static Dictionary<string, string> RoleNotFound
        {
            get
            {
                return new Dictionary<string, string>()
                {
 { "az", "Rol tapılmadı!" },
  { "ru", "Рол не найден!" },
      { "en", "Rol not found!" },
                };
            }
        }
        public static Dictionary<string, string> EmailAlreadyExists
        {
            get
            {
                return new Dictionary<string, string>()
                {
 { "az", "E-poçt artıq mövcuddur!" },
  { "ru","Email уже используется!" },
  { "en", "Email is already in use!" }
                };
            }
        }
        public static Dictionary<string, string> UserNameAlreadyExists
        {
            get
            {
                return new Dictionary<string, string>()
                {
 { "az", "İstifadəçi adı artıq mövcuddur!" },
  { "ru", "Имя пользователя уже существует!"},
  {"en", "Username already exists!" }
                };
            }
        }
        public static Dictionary<string, string> RegistrationSuccess
        {
            get
            {
                return new Dictionary<string, string>()
                {
{ "az", "Uğurla qeydiyyatdan keçdiniz!" },
      { "ru", "Регистрация прошла успешно!" },
        { "en", "Registration successful!" },
                };
            }
        }
        public static Dictionary<string, string> ConfirmationLinkNotSend
        {
            get
            {
                return new Dictionary<string, string>()
                {
 { "az", "Təsdiq Linki göndərilə bilmədi!Yenidən qeyydiyatdan keçməyə çalışın." },
  { "ru", "Не удалось отправить ссылку для подтверждения. Попробуйте зарегистрироваться еще раз." },
      { "en", "Verification Link could not be sent! Try registering again." },
                };
            }
        }


    }
}
