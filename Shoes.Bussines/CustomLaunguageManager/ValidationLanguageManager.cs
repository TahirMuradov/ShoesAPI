using FluentValidation;
using FluentValidation.Resources;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Shoes.Bussines.CustomLaunguageManager
{
    public class ValidationLanguageManager: LanguageManager
    {
        public ValidationLanguageManager()
        {

            #region CategoryValidationMessages
            #region CategoryAddValidationMessages
 


            AddTranslation("az", "LangCodeContentLengthMismatch", "Dil kodu və Məzmun uzunluqları bərabər olmalıdır!");
            AddTranslation("ru", "LangCodeContentLengthMismatch", "Длины LangCode и Content должны быть равны!");
            AddTranslation("en", "LangCodeContentLengthMismatch", "The length of LangCode and Content must be equal!");

            
            AddTranslation("az", "LangCodeLengthTooShort", "Dil kodu siyahısının uzunluğu 3-dən az ola bilməz!");
            AddTranslation("ru", "LangCodeLengthTooShort", "Длина списка LangCode не может быть меньше 3!");
            AddTranslation("en", "LangCodeLengthTooShort", "The length of the LangCode list cannot be less than 3!");

        
            AddTranslation("az", "ContentLengthTooShort", "Məzmun siyahısının uzunluğu 3-dən az ola bilməz!");
            AddTranslation("ru", "ContentLengthTooShort", "Длина списка Content не может быть меньше 3!");
            AddTranslation("en", "ContentLengthTooShort", "The length of the Content list cannot be less than 3!");

            
            AddTranslation("az", "InvalidLangCode", "Dil Kodları yalnız 'az', 'ru', 'en' ola bilər!");
            AddTranslation("ru", "InvalidLangCode", "LangCode может быть только 'az', 'ru', 'en'!");
            AddTranslation("en", "InvalidLangCode", "LangCode can only be 'az', 'ru', 'en'!");
            #endregion
            #region CategoryUpdateValidationMessages

            AddTranslation("az", "IdIsRequired", "ID boş ola bilməz!");
            AddTranslation("ru", "IdIsRequired", "ID не может быть пустым!");
            AddTranslation("en", "IdIsRequired", "ID is required!");

            AddTranslation("az", "InvalidGuid", "ID etibarlı bir GUID olmalıdır!");
            AddTranslation("ru", "InvalidGuid", "ID должен быть действительным GUID!");
            AddTranslation("en", "InvalidGuid", "ID must be a valid GUID!");

      
            AddTranslation("az", "LangDictionaryIsRequired", "Lang sözlüyü boş ola bilməz!");
            AddTranslation("ru", "LangDictionaryIsRequired", "Словарь Lang не может быть пустым!");
            AddTranslation("en", "LangDictionaryIsRequired", "Lang dictionary cannot be empty!");

            AddTranslation("az", "LangKeyAndValueRequired", "Lang sözlüyündə hər bir açar və dəyər dolu olmalıdır!");
            AddTranslation("ru", "LangKeyAndValueRequired", "Каждый ключ и значение в словаре Lang должны быть заполнены!");
            AddTranslation("en", "LangKeyAndValueRequired", "Each key and value in the Lang dictionary must be filled!");

            AddTranslation("az", "InvalidLangKey", "Lang açarı yalnız 'az', 'ru', 'en' ola bilər!");
            AddTranslation("ru", "InvalidLangKey", "Ключ Lang может быть только 'az', 'ru', 'en'!");
            AddTranslation("en", "InvalidLangKey", "Lang key can only be 'az', 'ru', 'en'!");


            #endregion
            #endregion
            #region SizeValidationMessages
            AddTranslation("az", "NewSizeNumberIsRequiredd", "Yeni ölçü boş ola bilməz!!");
            AddTranslation("az", "SizeNumberIsRequiredd", "Ölçü boş ola bilməz!!");
            AddTranslation("ru", "NewSizeNumberIsRequiredd", "Новый размер не может быть пустым!!");
            AddTranslation("en", "NewSizeNumberIsRequiredd", "New size cannot be empty!!");

            AddTranslation("ru", "SizeNumberIsRequiredd", "Размер не может быть пустым!!");
            AddTranslation("en", "SizeNumberIsRequiredd", "Size cannot be empty!!");

            #endregion
        }
    }
}
