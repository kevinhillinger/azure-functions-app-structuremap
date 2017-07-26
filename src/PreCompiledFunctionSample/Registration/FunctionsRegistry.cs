using Microsoft.Azure;
using Microsoft.Azure.Documents.Client;
using PreCompiledFunctionSample.Data;
using PreCompiledFunctionSample.Services;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreCompiledFunctionSample.Registration
{
    class FunctionsRegistry : Registry
    {
        public FunctionsRegistry()
        {
            var endpoint = new Uri(CloudConfigurationManager.GetSetting("endpoint"));
            var authKey = CloudConfigurationManager.GetSetting("authKey");

            // IMPORTANT: making document client provider a singleton ensures sharing doc client across all functions (in function app)
            For<DocumentClientProvider>().Use<DocumentClientProvider>().Singleton()
                .Ctor<Uri>().Is(endpoint)
                .Ctor<string>().Is(authKey);

            var databaseId = CloudConfigurationManager.GetSetting("database");
            var collectionId = CloudConfigurationManager.GetSetting("collection");

            For(typeof(DocumentDBRepository<>)).Use(typeof(DocumentDBRepository<>))
                .Ctor<string>("databaseId").Is(databaseId);

            For<DocumentClient>().Use(c => c.GetInstance<DocumentClientProvider>().GetClient()).Singleton();

            //TODO: map all services below
            For<IFunctionService>().Use<FunctionService>();
        }
    }
}
