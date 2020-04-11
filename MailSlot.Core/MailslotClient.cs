using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSlot.Core
{
    public class MailslotClient
    {
        private IntPtr _handelMailslot;

        public Int32 OpenMailslot(string mailslotName, string remoteComputer = ".")
        {
            string fullMailslotName = $@"\\{remoteComputer}\mailslot\{mailslotName}";

            IntPtr handle = MailSlotFunctions.CreateFile(
                fullMailslotName,
                DesiredAccess.GenericWrite,
                ShareMode.FILE_SHARE_READ,
                IntPtr.Zero,
                CreationDisposition.OPEN_EXISTING,
                0,
                IntPtr.Zero);

            _handelMailslot = handle;

            return handle.ToInt32();
        }

        public int SendMessage(string message)
        {
            if (_handelMailslot == null)
                throw new NullReferenceException("Не инициализировано подключение к mailslot");

            Byte[] bMessage = Encoding.Unicode.GetBytes(message);
            Int32 writenBytes;

            bool isSucceeded = MailSlotFunctions.WriteFile(_handelMailslot, bMessage, bMessage.Length, out writenBytes, IntPtr.Zero);

            return (int)writenBytes;
        }
    }
}
