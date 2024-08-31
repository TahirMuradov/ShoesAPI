﻿using FluentValidation.Resources;
using Microsoft.Extensions.Configuration;
using Shoes.Core.Helpers;

namespace Shoes.Bussines.CustomLaunguageManager
{
    public class ValidationLanguageManager: LanguageManager
    {
        public ValidationLanguageManager()
        {
            string[] supportedLaunguages = ConfigurationHelper.config.GetSection("SupportedLanguage:Launguages").Get<string[]>();
            string languagesList = string.Join(", ", supportedLaunguages);
            #region CategoryValidationMessages
            #region CategoryAddValidationMessages


            AddTranslation("az", "ContentEmpty", "Məzmun boş ola bilməz!");
            AddTranslation("az", "InvalidLangCode", $"Dil Kodları yalnız {languagesList} ola bilər!");
            AddTranslation("az", "LangContentTooShort", $"Məzmun siyahısının uzunluğu {supportedLaunguages.Length}-dən az ola bilməz!");

            AddTranslation("ru", "LangContentTooShort", $"Длина списка содержимого не может быть меньше {supportedLaunguages.Length}!");
            AddTranslation("ru", "InvalidLangCode", $"Коды языков могут быть только {languagesList}!");
            AddTranslation("ru", "ContentEmpty", "Содержимое не может быть пустым!");

            AddTranslation("en", "LangContentTooShort", $"The length of the content list cannot be less than {supportedLaunguages.Length}!");
            AddTranslation("en", "InvalidLangCode", $"Language codes can only be {languagesList}!");
            AddTranslation("en", "ContentEmpty", "Content cannot be empty!");

            #endregion
            #region CategoryUpdateValidationMessages

            AddTranslation("az", "IdIsRequired", "ID boş ola bilməz!");
            AddTranslation("ru", "IdIsRequired", "ID не может быть пустым!");
            AddTranslation("en", "IdIsRequired", "ID is required!");

            AddTranslation("az", "InvalidGuid", "ID etibarlı bir GUID olmalıdır!");
            AddTranslation("ru", "InvalidGuid", "ID должен быть действительным GUID!");
            AddTranslation("en", "InvalidGuid", "ID must be a valid GUID!");


            AddTranslation("az", "LangDictionaryIsRequired", "Məzmun boş ola bilməz!");
            AddTranslation("az", "LangKeyAndValueRequired", "Hər bir Dil Kodu və Məzmunu dolu olmalıdır!");
            AddTranslation("az", "InvalidLangKey", $"Dil Kodlari yalnız {languagesList} ola bilər!");

            AddTranslation("ru", "LangDictionaryIsRequired", "Содержимое не может быть пустым!");
            AddTranslation("ru", "LangKeyAndValueRequired", "Каждый код языка и содержимое должны быть заполнены!");
            AddTranslation("ru", "InvalidLangKey", $"Коды языков могут быть только {languagesList}!");

            AddTranslation("en", "LangDictionaryIsRequired", "Content cannot be empty!");
            AddTranslation("en", "LangKeyAndValueRequired", "Each language code and content must be filled!");
            AddTranslation("en", "InvalidLangKey", $"Language codes can only be {languagesList}!");


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
   
  
           

            // Russian
            AddTranslation("ru", "CategoryIdInvalid", "Категория не может быть пустой!");
   
      
         


            // English
            AddTranslation("en", "CategoryIdInvalid", "Category cannot be empty!");
    
       
         

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
            #region ShippingMethodValidationMessages

            AddTranslation("az", "DisCountNegativeNumberCheck", "Endirimli Qiymet  0-dan kiçik ola bilməz!");
            AddTranslation("az", "PriceNegativeNumberCheck", "Qiymət  0-a bərabər və ya kiçik ola bilməz!");

            AddTranslation("en", "DisCountNegativeNumberCheck", "Discount price cannot be less than 0!");
            AddTranslation("en", "PriceNegativeNumberCheck", "Price cannot be equal to or less than 0!");

            AddTranslation("ru", "DisCountNegativeNumberCheck", "Цена со скидкой не может быть меньше 0!");
            AddTranslation("ru", "PriceNegativeNumberCheck", "Цена не может быть равна 0 или меньше 0!");



            #endregion

            #region ProductDtoValidation

            AddTranslation("az", "DisCountChecked", "Endirimli Qiymət 0-dan kiçik ola bilməz!");
            AddTranslation("az", "PriceChecked", " Qiymət 0-dan böyük olamalıdır!");
            AddTranslation("az", "PictureİsRequired", "Məhsulun şəklini əlavə edin!");
            AddTranslation("az", "SubCategoryİsRequired", "Məhsulun Kateqoriyasını əlavə edin!");
            AddTranslation("az", "ProductCodeİsRequired", "Məhsulun Kodu əlavə edin!");

            AddTranslation("ru", "DisCountChecked", "Цена со скидкой не может быть меньше 0!");
            AddTranslation("ru", "PriceChecked", "Цена должна быть больше 0!");
            AddTranslation("ru", "PictureİsRequired", "Добавьте изображение продукта!");
            AddTranslation("ru", "SubCategoryİsRequired", "Добавьте категорию продукта!");
            AddTranslation("ru", "ProductCodeİsRequired", "Добавьте код продукта!");

            AddTranslation("en", "DisCountChecked", "Discounted price cannot be less than 0!");
            AddTranslation("en", "PriceChecked", "Price must be greater than 0!");
            AddTranslation("en", "PictureİsRequired", "Please add a product image!");
            AddTranslation("en", "SubCategoryİsRequired", "Please add a product category!");
            AddTranslation("en", "ProductCodeİsRequired", "Please add a product code!");
            //update dto validation message
            // Azerbaijani
            AddTranslation("az", "IdRequired", "Id boş olmamalıdır!");
            AddTranslation("az", "IdInvalid", "Id düzgün deyil!");
            AddTranslation("az", "ProductNameRequired", "Məhsul adı boş olmamalıdır!");
            AddTranslation("az", "DescriptionRequired", "Məhsul açıqlaması boş olmamalıdır!");
            AddTranslation("az", "SubCategoriesIDRequired", "Kateqoriya boş olmamalıdır!");
            AddTranslation("az", "SubCategoriesIDLength", "Minimum 1 dənə kateqoriya olmalıdır!");
            AddTranslation("az", "PictureUrlsOrNewPicturesRequired", "Məhsul şəkli əlavə edilməlidir!");
            AddTranslation("az", "SizesRequired", "Ölçü boş olmamalıdır!");
                  AddTranslation("az", "DiscountPriceInvalid", "Endirimli Qiymət 0-dan kiçik ola bilməz!");
            AddTranslation("az", "PriceInvalid", "Qiymət 0-dan böyük olmalıdır!");
            AddTranslation("az", "ProductCodeRequired", "Məhsul kodu boş olmamalıdır!");

            // Russian
            AddTranslation("ru", "IdRequired", "Id не может быть пустым!");
            AddTranslation("ru", "IdInvalid", "Id некорректен!");
            AddTranslation("ru", "ProductNameRequired", "Название продукта не может быть пустым!");
            AddTranslation("ru", "DescriptionRequired", "Описание продукта не может быть пустым!");
            AddTranslation("ru", "SubCategoriesIDRequired", "Категория не может быть пустой!");
            AddTranslation("ru", "SubCategoriesIDLength", "Должна быть хотя бы одна категория!");
            AddTranslation("ru", "PictureUrlsOrNewPicturesRequired", "Необходимо добавить изображение продукта!");
            AddTranslation("ru", "SizesRequired", "Размеры не могут быть пустыми!");
            AddTranslation("ru", "DiscountPriceInvalid", "Скидочная цена не может быть меньше 0!");
            AddTranslation("ru", "PriceInvalid", "Цена должна быть больше 0!");
            AddTranslation("ru", "ProductCodeRequired", "Код продукта не может быть пустым!");


            // English
            AddTranslation("en", "IdRequired", "Id cannot be null or empty!");
            AddTranslation("en", "IdInvalid", "Id cannot be invalid!");
            AddTranslation("en", "ProductNameRequired", "Product name cannot be null or empty!");
            AddTranslation("en", "DescriptionRequired", "Product description cannot be null or empty!");
            AddTranslation("en", "SubCategoriesIDRequired", "Subcategories cannot be null or empty!");
            AddTranslation("en", "SubCategoriesIDLength", "At least one subcategory is required!");
            AddTranslation("en", "PictureUrlsOrNewPicturesRequired", "Product picture must be provided!");
            AddTranslation("en", "SizesRequired", "Sizes cannot be null or empty!");
            AddTranslation("en", "DiscountPriceInvalid", "Discount price cannot be less than 0!");
            AddTranslation("en", "PriceInvalid", "Price must be greater than 0!");
            AddTranslation("en", "ProductCodeRequired", "Product code cannot be null or empty!");




            #endregion

        }
    }
}
