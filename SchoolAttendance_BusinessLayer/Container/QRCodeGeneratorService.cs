using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SchoolAttendance_BusinessLayer.Container
{
    public class QRCodeGeneratorService : IQRCodeGeneratorService
    {
        public async Task<string> GenerateQRCodeAsync(string text)
        {
            using (var qrGenerator = new QRCodeGenerator())
            {
                using (var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
                {
                    using (var qrCode = new QRCode(qrCodeData))
                    {
                        using (var bitmap = qrCode.GetGraphic(20))
                        {
                            using (var ms = new MemoryStream())
                            {
                                bitmap.Save(ms, ImageFormat.Png);
                                var base64String = Convert.ToBase64String(ms.ToArray());
                                return $"data:image/png;base64,{base64String}";
                            }
                        }
                    }
                }
            }
        }
    }


}
