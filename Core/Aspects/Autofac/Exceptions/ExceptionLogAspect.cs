using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Logging;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;

namespace Core.Aspects.Autofac.Exceptions;

public class ExceptionLogAspect : MethodInterception
{
    private LoggerServiceBase _loggerServiceBase;

    public ExceptionLogAspect(Type loggerService)
    {
        if (loggerService.BaseType != typeof(LoggerServiceBase))
        {
            throw new System.Exception(AspectMessages.WrongLoggerType);
        }

        _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
    }
    protected override void OnException(IInvocation invocation, System.Exception e)
    {
        LogDetailWithException LogDetailWithException = GetLogDetail(invocation);
        LogDetailWithException.ExceptionMessage = e.Message;
        _loggerServiceBase.Error(LogDetailWithException.ExceptionMessage);
    }

    private LogDetailWithException GetLogDetail(IInvocation invocation)
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

        var logDetailWithException = new LogDetailWithException
        {
            MethodName = invocation.Method.Name,
            LogParameters = logParameters
        };

        return logDetailWithException;
    }
}
