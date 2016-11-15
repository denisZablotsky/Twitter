using Ninject;
using System.Web.Mvc;
using System;
using System.Web.Routing;
using Twitter.Data;

namespace Twitter.Infrastructure
{
    public class NinjectControllerFactory: DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controlType)
        {
            // Geting object of Control by controller type
            return controlType == null ? null : (IController)ninjectKernel.Get(controlType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IUserRepository>().To<EfUserRepository>();
            ninjectKernel.Bind<ITweetRepository>().To<EfTweetRepository>();
            ninjectKernel.Bind<IUserProfileRepository>().To<EfUserProfileRepository>();
        }
    }
}