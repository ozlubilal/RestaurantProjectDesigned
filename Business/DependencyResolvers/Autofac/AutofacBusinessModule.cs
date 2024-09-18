using Autofac;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Concrete;
using Business.Concretes;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstracts;
using DataAccess.Concretes;
using System.Reflection; // Bu satırı ekleyin
using Module = Autofac.Module;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();
        builder.RegisterType<EfProductDal>().As<IProductDal>().SingleInstance();

        builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
        builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

        builder.RegisterType<BillManager>().As<IBillService>().SingleInstance();
        builder.RegisterType<EfBillDal>().As<IBillDal>().SingleInstance();

        builder.RegisterType<OrderManager>().As<IOrderService>().SingleInstance();
        builder.RegisterType<EfOrderDal>().As<IOrderDal>().SingleInstance();

        builder.RegisterType<UserManager>().As<IUserService>();
        builder.RegisterType<EfUserDal>().As<IUserDal>();

        //builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();
        //builder.RegisterType<EfOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

        //builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
        //builder.RegisterType<EfUserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

        builder.RegisterType<StoreBillManager>().As<IStoreBillService>().SingleInstance();
        builder.RegisterType<EfStoreBillDal>().As<IStoreBillDal>().SingleInstance();

        builder.RegisterType<TableManager>().As<ITableService>().SingleInstance();
        builder.RegisterType<EfTableDal>().As<ITableDal>().SingleInstance();



        builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
        builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();




        //   builder.RegisterType<FileLogger>().SingleInstance();

        // AutoMapper'ı kaydediyoruz
        builder.Register(context => new MapperConfiguration(cfg =>
        {
            // Tüm profilleri otomatik olarak yüklemek için Assembly taraması
            cfg.AddMaps(Assembly.GetExecutingAssembly()); // Assembly sınıfı için System.Reflection gereklidir
        })).AsSelf().SingleInstance();

       // builder.RegisterType<MemoryCache>().As<IMemoryCache>().SingleInstance();

        // IMapper servisini kaydediyoruz
        builder.Register(context =>
        {
            var ctx = context.Resolve<IComponentContext>();
            var config = ctx.Resolve<MapperConfiguration>();
            return config.CreateMapper();
        }).As<IMapper>().InstancePerLifetimeScope();
       
        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
          .EnableInterfaceInterceptors(new ProxyGenerationOptions()
          {
              Selector = new AspectInterceptorSelector()
          }).SingleInstance();
    }
}
