using FCG.Catalog.Domain.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FCG.Catalog.Domain.Common.Validation
{
    internal static class Guard
    {
        public static void AgainstEmptyId(int value, string paramName)
        {
            if (value <= 0)
                throw new DomainException($"{paramName} deve ser maior que zero.");
        }

        public static void AgainstNull<T>(T value, string name)
        {
            if (value is null)
                throw new DomainException($"{name} não pode ser nulo.");
        }

        public static void Against<TException>(bool condition, string message) where TException : Exception
        {
            if (condition) throw (TException)Activator.CreateInstance(typeof(TException), message)!;
        }
    }
}
