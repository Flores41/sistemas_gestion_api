using Abstraction;
using SistemaVentas.Abstraction;
using System;

namespace Abstraction
{
    public interface IDBContext<T> : ICrud<T>
    {
    }
}
