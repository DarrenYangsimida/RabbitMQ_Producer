using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProducerApp.DI
{
    public class CallLoggerAsync : IAsyncInterceptor
    {
        private readonly ILogger _logger;

        public CallLoggerAsync(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<CallLoggerAsync>();
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            string methodName = "";
            object[] methodArguments = new object[] { };

            try
            {
                // Step 1. Do something prior to invocation.

                methodName = invocation.GetConcreteMethod()?.Name;
                methodArguments = invocation.Arguments;

                this._logger.LogWarning("Calling method {0} with parameters {1}  ", methodName, Newtonsoft.Json.JsonConvert.SerializeObject(methodArguments));

                invocation.Proceed();

                var task = (Task)invocation.ReturnValue;
                await task;

                // Step 2. Do something after invocation.
                this._logger.LogWarning("Done: result was {0}.", task);
            }
            catch (Exception e)
            {
                _logger.LogError(e, methodName, methodArguments);
                throw;
            }


        }


        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            try
            {
                // Step 1. Do something prior to invocation.

                //this._logger.LogWarning("Calling method {0} with parameters {1}... ", invocation.Method.Name, string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));
                this._logger.LogWarning("Calling method {0} with parameters {1}  ", invocation.GetConcreteMethod()?.Name, Newtonsoft.Json.JsonConvert.SerializeObject(invocation.Arguments));

                invocation.Proceed();
                var task = (Task<TResult>)invocation.ReturnValue;
                TResult result = await task;

                // Step 2. Do something after invocation.
                this._logger.LogWarning("Done: result was {0}.", result);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, invocation.GetConcreteMethod()?.Name, invocation.Arguments);
                throw;
            }


        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            try
            {
                //this._logger.LogWarning("Calling method {0} with parameters {1}... ", invocation.Method.Name, string.Join(", ", invocation.Arguments.Select(a => (a ?? "").ToString()).ToArray()));
                this._logger.LogWarning("Calling method {0} with parameters {1}  ", invocation.Method.Name, Newtonsoft.Json.JsonConvert.SerializeObject(invocation.Arguments));
                invocation.Proceed();
                this._logger.LogWarning("Done: result was {0}.", invocation.ReturnValue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, invocation.Method.Name, invocation.Arguments);
                throw;
            }
        }

    }
}
