using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Logging;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using Newtonsoft.Json; // JSON formatı için gerekli
using System;
using System.Collections.Generic;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;

        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            // LogDetail nesnesini JSON formatına çevirip logluyoruz
            string logDetailString = JsonConvert.SerializeObject(GetLogDetail(invocation));
            _loggerServiceBase.Info(logDetailString);
        }

        protected override void OnException(IInvocation invocation, Exception exception)
        {
            // İstisna detaylarını JSON formatına çevirip logluyoruz
            var logDetail = new LogDetailWithException
            {
                MethodName = invocation.Method.Name,
                LogParameters = GetLogParameters(invocation),
                ExceptionMessage = exception.Message
            };
            string logDetailString = JsonConvert.SerializeObject(logDetail);
            _loggerServiceBase.Error(logDetailString);
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            return new LogDetail
            {
                MethodName = invocation.Method.Name,
                LogParameters = GetLogParameters(invocation)
            };
        }

        private List<LogParameter> GetLogParameters(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            return logParameters;
        }
    }
}
