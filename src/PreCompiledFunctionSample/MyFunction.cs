using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using StructureMap;
using PreCompiledFunctionSample.Registration;
using PreCompiledFunctionSample.Services;

namespace PreCompiledFunctionSample
{
    public class MyFunction
    {
        // declare a static container at the top with lazy instantiation

        static Lazy<IContainer> containerProvider = new Lazy<IContainer>(() =>
        {
            var instance = new Container(c =>
            {
                c.AddRegistry<FunctionsRegistry>();
            });
            return instance;
        });

        /// <summary>
        /// container to get instances
        /// </summary>
        static IContainer Container
        {
            get { return containerProvider.Value; }
        }

        public static async Task<HttpResponseMessage> Run(HttpRequestMessage req)
        {
            var service = GetService<FunctionService>();

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            // Get request body
            dynamic data = await req.Content.ReadAsAsync<object>();

            // Set name to query string or body data
            name = name ?? data?.name;

            return name == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + name);
        }

        private static T GetService<T>()
        {
            return Container.GetInstance<T>();
        }
    }
}
