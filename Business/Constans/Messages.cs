using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constans
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductsListed = "Ürünler listelendi";
        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
        public static string ProductNameAlreadyExists = "Bu isimde zaten başka bir ürün var";
        public static string CategoryLimitExceded = "Kategori limiti aşıldığı için yeni ürün eklenemiyor";
        public static string AuthorizationDenied = "Yetkiniz yok.";


        public static string AddedSuccess = "Ekleme işlemi başarılı";
        public static string DeletedSuccess = "Silme işlemi başarılı";
        public static string UpdatedSuccess = "Güncelleme işlemi başarılı";
        public static string OrderOfBillExist = "Bu hesaba ait siparişler olduğu için hesap silinemez";
        public static string TableOfFloorExist = "Bu kata ait masalar olduğu için kat silinemez";

        public static string OrderOfProductExist = "Bu ürüne ait siparişler olduğu için kategori silinemez";

        public static string TableNameExist = "Bu isimde masa vardır";
        public static string ProductNameExist = "Bu isimde ürün vardır";
        public static string CategoryNameExist = "Bu isimde kategori vardır";
        public static string ProductOfTheCategoryExist = "Bu kategoriye ait ürünler olduğu için kategori silinemez";

        public static string TableNotEmpty = "Masa Boş değil";
        public static string FloorNameExist = "Bu isimde kat vardır";

        public static string BillOfStoreBillExist = "Günlük hesaba ait masa hesapları olduğu için silinemez";

        public static string StoreBillPassive = "Mağaza günlük hesap aktif değil";

        public static string ActiveBillOfStoreBillExist = "Bütün masa hesaplarını tahsil ediniz";
    }
}