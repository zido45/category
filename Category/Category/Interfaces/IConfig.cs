using System;
using System.Collections.Generic;
using System.Text;

namespace Category.Interfaces
{
    using SQLite.Net.Interop;

    public interface IConfig
    {

        string DirectoryDB { get; }
        ISQLitePlatform Platform { get; }

    }
}
