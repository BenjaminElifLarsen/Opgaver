using System;
using System.Collections.Generic;
using System.Text;

namespace StorageSystemCore
{
    interface IConversion
    {
        protected T[] ArrayConversion<T>(object[] arrayToConvert);

    }
}
