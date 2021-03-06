﻿using Ninject.Modules;
using NLog;
using SolutionCrawler.Classes;
using SolutionCrawler.Interfaces;

namespace SolutionCrawler
{
    class ApplicationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(x =>
            {
                var scope = x.Request.ParentRequest.Service.FullName;
                var log = (ILog)LogManager.GetLogger(scope, typeof(Log));
                return log;
            });
            Bind<IApp>().To<App>().InTransientScope();

            //Declare Transient
            Bind<IFileReader>().To<FileReader>().InTransientScope();
            Bind<IFileWriter>().To<FileWriter>().InTransientScope();
            Bind<IDankCommander>().To<DankCommander>().InTransientScope();

            //Declare Singleton
            Bind<IXMLDeserializer>().To<XMLDeserializer>().InSingletonScope();
        }
    }
}
