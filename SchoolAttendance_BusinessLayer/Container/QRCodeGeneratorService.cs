using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SchoolAttendance_BusinessLayer.Container
{
    public class QRCodeGeneratorService
    {
        public string GenerateQRCode(string text)
        {
            // QRCode sınıfı QRCoder kütüphanesinde tanımlanmış olmalı
            using (var qrGenerator = new QRCodeGenerator())
            {
                using (var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
                {
                    // QRCode sınıfı burada kullanılmalı
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        using (var bitmap = qrCode.GetGraphic(20))
                        {
                            using (var ms = new MemoryStream())
                            {
                                bitmap.Save(ms, ImageFormat.Png);
                                var byteArray = ms.ToArray();
                                return $"data:image/png;base64,{Convert.ToBase64String(byteArray)}";
                            }
                        }
                    }
                }
            }
        }
    }
}
