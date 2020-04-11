using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSlot.Core
{
    public enum CreationDisposition : UInt32
    {
        /// <summary>
        /// Всегда создавать новый файл
        /// </summary>
        CREATE_ALWAYS = 2,

        /// <summary>
        /// Создается новый файл, если он еще не был создан
        /// </summary>
        CREATE_NEW = 1,

        /// <summary>
        /// Открыть файл
        /// </summary>
        OPEN_ALWAYS = 4,

        /// <summary>
        /// Открывает файл или устройство, только если оно существует.
        /// </summary>
        OPEN_EXISTING = 3,

        /// <summary>
        /// Открывает файл и усекает его, так что его размер равен нулю, только если он существует.
        /// </summary>
        TRUNCATE_EXISTING = 5
    }
}
