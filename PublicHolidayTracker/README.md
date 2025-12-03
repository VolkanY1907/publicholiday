# PublicHolidayTracker

Türkiye Resmi Tatil Takip Uygulaması (2023-2025)

Bu proje, C# Konsol Uygulaması olarak geliştirilmiştir. `date.nager.at` API servisini kullanarak 2023, 2024 ve 2025 yılları için Türkiye'deki resmi tatilleri çeker ve kullanıcıya sorgulama imkanı sunar.

## Gereksinimler

- Visual Studio 2022 (veya daha yeni sürüm)
- .NET 6.0, .NET 7.0 veya .NET 8.0 SDK

## Kurulum ve Çalıştırma

1. `PublicHolidayTracker.sln` dosyasını Visual Studio ile açın.
2. Çözüm Gezgini'nde (Solution Explorer) `PublicHolidayTracker` projesine sağ tıklayın ve "Derle" (Build) seçeneğini seçin.
3. "Başlat" (Start) butonuna basarak veya `F5` tuşu ile uygulamayı çalıştırın.

## Kullanım

Uygulama açıldığında veriler otomatik olarak indirilir. Ardından aşağıdaki menüden seçim yapabilirsiniz:

1. **Tatil listesini göster (yıl seçmeli):** Belirttiğiniz yılın tatillerini listeler.
2. **Tarihe göre tatil ara (gg-aa formatı):** Örn: `29-10` yazarak 29 Ekim tarihindeki tatilleri (tüm yıllar için) bulabilirsiniz.
3. **İsme göre tatil ara:** Tatil isminin bir kısmını yazarak arama yapabilirsiniz (Örn: "Ramazan").
4. **Tüm tatilleri 3 yıl boyunca göster:** Yüklenen tüm tatil verisini ekrana basar.
5. **Çıkış:** Programı kapatır.
