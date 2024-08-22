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


            // Add translation for the rule ensuring LangContent is not null and contains at least three entries
            AddTranslation("az", "LangContentTooShort", "LangContent siyahısının uzunluğu 3-dən az ola bilməz!");
            AddTranslation("ru", "LangContentTooShort", "Длина списка LangContent не может быть меньше 3!");
            AddTranslation("en", "LangContentTooShort", "The length of the LangContent list cannot be less than 3!");

            // Add translation for the rule validating each key in LangContent is a valid language code
            AddTranslation("az", "InvalidLangCode", "Dil Kodları yalnız 'az', 'ru', 'en' ola bilər!");
            AddTranslation("ru", "InvalidLangCode", "LangCode может быть только 'az', 'ru', 'en'!");
            AddTranslation("en", "InvalidLangCode", "LangCode can only be 'az', 'ru', 'en'!");

            // Add translation for the rule ensuring each value in LangContent is not null or empty
            AddTranslation("az", "ContentEmpty", "Məzmun boş ola bilməz!");
            AddTranslation("ru", "ContentEmpty", "Содержимое не может быть пустым!");
            AddTranslation("en", "ContentEmpty", "Content cannot be empty!");

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

            #region SubCategoryValidationMessages
            #region AddSubCategoryValidationMessages
            // Azerbaijani
            AddTranslation("az", "CategoryIdInvalid", "Kateqoriya  boş  ola bilməz!");
            AddTranslation("az", "LangContentTooShort", "Dil Məzmun siyahısının uzunluğu 3 olmalıdır!");
            AddTranslation("az", "InvalidLangCode", "Dil Kodları yalnız 'az', 'ru', 'en' ola bilər!");
            AddTranslation("az", "ContentEmpty", "Məzmun boş ola bilməz!");

            // Russian
            AddTranslation("ru", "CategoryIdInvalid", "Категория не может быть пустой!");
            AddTranslation("ru", "LangContentTooShort", "Длина списка языкового контента должна быть 3!");
            AddTranslation("ru", "InvalidLangCode", "Коды языков могут быть только 'az', 'ru', 'en'!");
            AddTranslation("ru", "ContentEmpty", "Контент не может быть пустым!");


            // English
            AddTranslation("en", "CategoryIdInvalid", "Category cannot be empty!");
            AddTranslation("en", "LangContentTooShort", "The length of the language content list must be 3!");
            AddTranslation("en", "InvalidLangCode", "Language codes can only be 'az', 'ru', 'en'!");
            AddTranslation("en", "ContentEmpty", "Content cannot be empty!");

            #endregion
            #region UpdateSubCategoryValidationMessages
            AddTranslation("az", "SubCategoryIdInvalid", "Alt Kateqoriya məzmunu boş  ola bilməz!");
            AddTranslation("az", "LangSubCategoryContentTooShort", "Alt Kateqoriya id-si boş  ola bilməz!");
            AddTranslation("ru", "SubCategoryIdInvalid", "Содержимое подкатегории не может быть пустым!");
            AddTranslation("ru", "LangSubCategoryContentTooShort", "ID подкатегории не может быть пустым!");
            AddTranslation("en", "SubCategoryIdInvalid", "Subcategory content cannot be empty!");
            AddTranslation("en", "LangSubCategoryContentTooShort", "Subcategory ID cannot be empty!");

            #endregion
            #endregion
        }
    }
}
