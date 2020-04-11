using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSlot.Core
{
    public class MailslotServer
    {
        private IntPtr _handelMailslot;

        public Int32 CreateMailSlot(string mailslotName)
        {
            string fullMailslotName = $@"\\.\mailslot\{mailslotName}";

            IntPtr handle = MailSlotFunctions.CreateMailslot(fullMailslotName, 0, 0, IntPtr.Zero);

            _handelMailslot = handle;

            return handle.ToInt32();
        }

        public string ReadMessage()
        {
            if (_handelMailslot == null)
                throw new NullReferenceException("Не инициализирована работа с mailslot");

            int messageBytes;
            int bytesRead;
            int messages;

            if (!MailSlotFunctions.GetMailslotInfo(_handelMailslot, IntPtr.Zero, out messageBytes, out messages, IntPtr.Zero))
                return "";

            if (messageBytes == -1)
            {
                return "";
            }

            var bBuffer = new byte[messageBytes];

            if (!MailSlotFunctions.ReadFile(_handelMailslot, bBuffer, messageBytes, out bytesRead, IntPtr.Zero) || bytesRead == 0)
                return "";

            string result = Encoding.Unicode.GetString(bBuffer);

            return result;
        }
    }
}
