using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolAttendance_BusinessLayer.Container
{
    public interface IQRCodeGeneratorService
    {
        Task<string> GenerateQRCodeAsync(string text);
    }

}
