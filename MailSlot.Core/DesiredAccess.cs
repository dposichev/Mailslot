using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSlot.Core
{
    /// <summary>
    /// Data Access Mask
    /// </summary>
    [Flags]
    public enum DesiredAccess : UInt32
    {
        GenericRead = 0x80000000,
        GenericWrite = 0x40000000,
        GenericExecute = 0x20000000,
        GenericAll = 0x10000000
    }
}
