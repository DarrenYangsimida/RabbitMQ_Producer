using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProducerApp.DI
{
    public class CallLogger : IInterceptor
    {
        readonly IAsyncInterceptor _asyncInterceptor;

        public CallLogger(IAsyncInterceptor asyncInterceptor)
        {
            _asyncInterceptor = asyncInterceptor;
        }

        public void Intercept(IInvocation invocation)
        {
            _asyncInterceptor.ToInterceptor().Intercept(invocation);
        }

    }
}
