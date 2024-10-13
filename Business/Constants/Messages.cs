using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants;

public class Messages
{
    public static string ProductAdded = "Ürün başarıyla eklendi";
    public static string ProductDeleted = "Ürün başarıyla silindi";
    public static string ProductUpdated = "Ürün başarıyla güncellendi";
    public static string ProductNotFound = "Ürün bulunamadı";
    public static string ProductNameAlreadyExists = "Ürün ismi zaten mevcut";

    public static string CategoryAdded = "Kategori başarıyla eklendi";
    public static string CategoryDeleted = "Kategori başarıyla silindi";
    public static string CategoryUpdated = "Kategori başarıyla güncellendi";
    public static string CategoryNotFound = "Kategori bulunamadı";
    public static string CategoryNameAlreadyExists = "Kategori ismi zaten mevcut";

    public static string StoreBillAdded = "Mağaza hesabı başarıyla eklendi";
    public static string StoreBillNotFound = "Mağaza hesabı bulunamadı";
    public static string StoreBillDeleted = "Mağaza hesabı başarıyla silindi";
    public static string StoreBillUpdated = "Mağaza hesabı başarıyla güncellendi";

    public static string BillAdded = "Hesap başarıyla eklendi";
    public static string BillNotFound = "Hesap bulunamadı";
    public static string BillUpdated = "Hesap başarıyla güncellendi";
    public static string BillDeleted = "Hesap başarıyla silindi";

    public static string TableAdded = "Masa ekleme başarılı";
    public static string TableNotFound = "Masa bulunamadı";
    public static string TableDeleted = "Masa silme başarılı";
    public static string TableUpdated = "Masa güncelleme başarılı";

    public static string UserNotFound = "Kullanıcı bulunamadı";
    public static string PasswordError = "Şifre hatalı";
    public static string SuccessfulLogin = "Sisteme giriş başarılı";
    public static string UserAlreadyExists = "Bu kullanıcı zaten mevcut";
    public static string UserRegistered = "Kullanıcı başarıyla kaydedildi";
    public static string AccessTokenCreated = "Access token başarıyla oluşturuldu";

    public static string AuthorizationDenied = "Yetkiniz yok";
    public static string FormIncomplete = "Lütfen tüm gerekli alanları doldurun.";

    public static string CategoryHasProduct = "Kategoriye ait ürün olduğu için silinemez";

    public static string ProductHasOrder = "Ürüne ait sipairş olduğu için silinemez";

    public static string TableHasOrder = "Masaya ait hesap olduğu için silinemez";

    public static string BillHasOrder = "Hesaba ait sipariş olduğu için silinemez";

    public static string TableNotEmpty = "Masa statusü boş değil";

    public static string PaymentSuccess = "Tahsilat Başarılı";
    public static string OrdersMustBeServed = "Tahsilat için tüm siparişlerin 'Servis Edildi' durumunda olması gerekmektedir.";

    public static string ActiveStoreBillAlreadyExists = "Açık olan mağaza hesabı zaten mevcut";

    public static string TableNameAlreadyExists = "Masa ismi zaten mevcut";

    public static string StoreBillHasBills = "Mağaza hesabına ait masa hesabı olduğu için silinemez";

    public static string JustChefDelete = "İşleme alınımış siparişleri sadece Cehf kullanıcı silebilir.";
}
