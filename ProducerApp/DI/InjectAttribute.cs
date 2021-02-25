using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerApp.DI
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute
    {

    }
}
