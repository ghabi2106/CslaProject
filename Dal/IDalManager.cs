using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface IDalManager : IDisposable
    {
        T GetProvider<T>() where T : class;
    }
}
