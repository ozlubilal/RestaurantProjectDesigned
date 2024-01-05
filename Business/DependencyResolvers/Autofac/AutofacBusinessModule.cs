using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

           // builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            builder.RegisterType<BillManager>().As<IBillService>().SingleInstance();
            builder.RegisterType<EfBillDal>().As<IBillDal>().SingleInstance();

            builder.RegisterType<FloorManager>().As<IFloorService>().SingleInstance();
            builder.RegisterType<EfFloorDal>().As<IFloorDal>().SingleInstance();

            builder.RegisterType<BillStatusManager>().As<IBillStatusService>().SingleInstance();
            builder.RegisterType<EfBillStatusDal>().As<IBillStatusDal>().SingleInstance();

            builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();
            builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();

            builder.RegisterType<OrderStatusManager>().As<IOrderStatusService>().SingleInstance();
            builder.RegisterType<EfOrderStatusDal>().As<IOrderStatusDal>().SingleInstance();

            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
            builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

            builder.RegisterType<SeaterOfTableManager>().As<ISeaterOfTableService>().SingleInstance();
            builder.RegisterType<EfSeaterOfTableDal>().As<ISeaterOfTableDal>().SingleInstance();

            builder.RegisterType<TableManager>().As<ITableService>().SingleInstance();
            builder.RegisterType<EfTableDal>().As<ITableDal>().SingleInstance();

            builder.RegisterType<TableStatusManager>().As<ITableStatusService>().SingleInstance();
            builder.RegisterType<EfTableStatusDal>().As<ITableStatusDal>().SingleInstance();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();
            builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            builder.RegisterType<StoreBillManager>().As<IStoreBillService>().SingleInstance();
            builder.RegisterType<EfStoreBillDal>().As<IStoreBillDal>().SingleInstance();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
            builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();


        }
    }
}
