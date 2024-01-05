using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.AutoFac.NullCheck
{
    [Serializable]
    public class NullCheckAspect : MethodInterception
    {
        protected override void OnBefore(IInvocation invocation)
        {
            foreach (var argument in invocation.Arguments)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException("Parametre boş olamaz");
                }
            }
        }
    }
}
