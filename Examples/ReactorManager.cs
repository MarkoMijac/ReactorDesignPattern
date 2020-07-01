using ReactorDesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples
{
    public static class ReactorManager
    {
        private static IReactor _defaultReactor;
        public static IReactor GetDefaultReactor()
        {
            if (_defaultReactor == null)
            {
                _defaultReactor = new Reactor();
            }

            return _defaultReactor;
        }
    }
}
