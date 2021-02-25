using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ProducerApp.DI
{
    public class InjectPropertySelector : DefaultPropertySelector
    {

        public InjectPropertySelector(bool preserveSetValues) : base(preserveSetValues)
        {
        }

        public override bool InjectProperty(PropertyInfo propertyInfo, object instance)
        {
            var attr = propertyInfo.GetCustomAttribute<InjectAttribute>(inherit: true);
            return attr != null && propertyInfo.CanWrite
                    && (!PreserveSetValues || (propertyInfo.CanRead && propertyInfo.GetValue(instance, null) == null));
        }

    }
}
