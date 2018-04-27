using Autofac;
using Autofac.Integration.Mvc;
using PlaceUp.Context;
using PlaceUp.Context.PlaceRepository;
using PlaceUp.Context.CategoryRepository;
using PlaceUp.Context.TagRepository;
using PlaceUp.Context.FeedbackRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PlaceUp.Autofac
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PlaceRepository>().As<IPlaceRepository>().WithParameter("context", new DataContext());
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().WithParameter("context", new DataContext());
            builder.RegisterType<TagRepository>().As<ITagRepository>().WithParameter("context", new DataContext());
            builder.RegisterType<FeedbackRepository>().As<IFeedbackRepository>().WithParameter("context", new DataContext());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}