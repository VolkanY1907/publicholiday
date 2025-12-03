using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace PublicHolidayTracker
{
    class Program
    {
        // Tatil listesini hafızada tutmak için bir liste
        static List<Holiday> allHolidays = new List<Holiday>();
        
        // API çağrıları için HttpClient
        static HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            Console.WriteLine("Veriler indiriliyor, lütfen bekleyiniz...");

            // 2023, 2024 ve 2025 yılları için verileri çekip listeye ekliyoruz
            await LoadHolidays(2023);
            await LoadHolidays(2024);
            await LoadHolidays(2025);

            Console.WriteLine("Veriler başarıyla yüklendi.");
            Console.WriteLine();

            bool exit = false;
            while (!exit)
            {
                // Menü yazdırma
                Console.WriteLine("===== PublicHolidayTracker =====");
                Console.WriteLine("1. Tatil listesini göster (yıl seçmeli)");
                Console.WriteLine("2. Tarihe göre tatil ara (gg-aa formatı)");
                Console.WriteLine("3. İsme göre tatil ara");
                Console.WriteLine("4. Tüm tatilleri 3 yıl boyunca göster (2023–2025)");
                Console.WriteLine("5. Çıkış");
                Console.Write("Seçiminiz: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowHolidaysByYear();
                        break;
                    case "2":
                        SearchByDate();
                        break;
                    case "3":
                        SearchByName();
                        break;
                    case "4":
                        ShowAllHolidays();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Programdan çıkılıyor...");
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyiniz.");
                        break;
                }
                Console.WriteLine();
            }
        }

        // API'den veri çeken ve listeye ekleyen metod
        static async Task LoadHolidays(int year)
        {
            try
            {
                string url = $"https://date.nager.at/api/v3/PublicHolidays/{year}/TR";
                string jsonString = await client.GetStringAsync(url);

                // JSON verisini Holiday listesine dönüştürme (Deserialize)
                // Büyük/küçük harf duyarlılığını kapatıyoruz çünkü API PascalCase dönebilir ama sınıfımız camelCase
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                List<Holiday> holidays = JsonSerializer.Deserialize<List<Holiday>>(jsonString, options);

                if (holidays != null)
                {
                    allHolidays.AddRange(holidays);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{year} yılı verileri alınırken hata oluştu: {ex.Message}");
            }
        }

        // Seçenek 1: Yıla göre listeleme
        static void ShowHolidaysByYear()
        {
            Console.Write("Listelenecek yılı girin (Örn: 2024): ");
            string inputYear = Console.ReadLine();

            // Girilen yılın tatillerini filtrele
            // StartsWith kullanarak yıl kontrolü yapıyoruz çünkü tarih formatı yyyy-MM-dd
            bool found = false;
            foreach (var h in allHolidays)
            {
                if (h.date.StartsWith(inputYear))
                {
                    PrintHoliday(h);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Bu yıla ait tatil bulunamadı veya veri yok.");
            }
        }

        // Seçenek 2: Tarihe göre arama (gg-aa)
        static void SearchByDate()
        {
            Console.Write("Aranacak tarihi girin (gg-aa formatında, örn: 01-01): ");
            string inputDate = Console.ReadLine();

            // Kullanıcı gg-aa giriyor ama veritabanında yyyy-MM-dd formatında
            // Bu yüzden tarihin sonunun girilen değerle bitip bitmediğini kontrol edebiliriz
            // Ancak gg-aa formatını -MM-dd formatına çevirip aramak daha doğru olur.
            // Örnek input: 29-10. Veri: 2023-10-29.
            // Input'u ters çevirip arayalım veya split edelim.

            string[] parts = inputDate.Split('-');
            if (parts.Length != 2)
            {
                Console.WriteLine("Hatalı format. Lütfen gg-aa şeklinde giriniz.");
                return;
            }

            string day = parts[0].PadLeft(2, '0');
            string month = parts[1].PadLeft(2, '0');
            string searchPattern = $"-{month}-{day}"; // Örn: -10-29

            bool found = false;
            foreach (var h in allHolidays)
            {
                // h.date stringi yyyy-MM-dd formatında
                if (h.date.EndsWith(searchPattern))
                {
                    PrintHoliday(h);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Belirtilen tarihte bir tatil bulunamadı.");
            }
        }

        // Seçenek 3: İsme göre arama
        static void SearchByName()
        {
            Console.Write("Tatil adını girin (Örn: Cumhuriyet): ");
            string searchName = Console.ReadLine();

            bool found = false;
            foreach (var h in allHolidays)
            {
                // Büyük küçük harf duyarlılığı olmadan arama (LocalName içinde)
                if (h.localName.Contains(searchName, StringComparison.OrdinalIgnoreCase))
                {
                    PrintHoliday(h);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Bu isimle eşleşen tatil bulunamadı.");
            }
        }

        // Seçenek 4: Tüm tatilleri göster
        static void ShowAllHolidays()
        {
            if (allHolidays.Count == 0)
            {
                Console.WriteLine("Hiç veri yok.");
                return;
            }

            foreach (var h in allHolidays)
            {
                PrintHoliday(h);
            }
        }

        // Yardımcı metod: Tatil bilgisini ekrana yazdırma
        static void PrintHoliday(Holiday h)
        {
            Console.WriteLine($"Tarih: {h.date} | Tatil: {h.localName} ({h.name})");
        }
    }
}
