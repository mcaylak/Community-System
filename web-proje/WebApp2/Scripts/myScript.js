    function Validate() {

            if (mailKontrol()) {
                var password = document.getElementById("txtPassword").value;
                var confirmPassword = document.getElementById("txtConfirmPassword").value;
                var uzunluk = password.length;
                if (uzunluk>7) {
                    if (password != confirmPassword && password != null) {
                    alert("Parola Kontrolde Hata.Lütfen Tekrar Deneyiniz!");
                      return false;
                    }
                    else
                    {
                        return true;
                    }

                }
                else
                {
        alert("Parolanız en az 8 karakter olmalıdır");
    return false;
                }

            }
            else
            {
                return false;
            }

        }

  
        function deneme() {
            var password = document.getElementById("txtPassworda").value;
            var confirmPassword = document.getElementById("txtConfirmPassworda").value;
            if (password != confirmPassword && password != null) {
            alert("Parola Kontrolde Hata.Lütfen Tekrar Deneyiniz!");
        return false;
            }
            else if (password != "" && confirmPassword != null) {
                return true;
            }
            else
                return false;
        }

   
        function kontrol() {
            if (deneme() == true)
            {
            alert("Başarıyla Kayıt İsleminiz Gerçekleştirildi");
        }
            else
            {
            alert("Kayit İsleminde Hata");
        }
        }

        function katil() {
            alert("Etkinlige Katildiniz!");

        }
        function iptal() {
            alert("Etkinlige Katılımınız İptal Edildi!");
        }
        function katilma() {
            alert("Etkinlige Katılmanız İcin Önce Üye Olmalısınız veya Zaten Üye İseniz Giriş Yapınız!");
        }
        function etkinlikTalebiHata() {
            alert("Etkinlik Talebi Gönderebilmek İcin Hesabınıza Giriş Yapın veya Hesabınız Yoksa Kayıt Olun!");
        }



        function mailKontrol() {
            var mail = document.getElementById("mailKontrol").value;
            var uzunluk = mail.length;
            var kontrol = mail.substring(uzunluk - 14);
            if (kontrol == "sakarya.edu.tr") {
                return true;
            }
            else
            {
            alert("Uye Olmak İcin Sakarya Üniversitesi Mail Adresinizi Kullanmalısınız!");
        return false;
            }

        }
