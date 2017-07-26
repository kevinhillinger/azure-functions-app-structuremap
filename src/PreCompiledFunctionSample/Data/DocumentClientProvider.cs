using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreCompiledFunctionSample.Data
{
    class DocumentClientProvider
    {
        private static Uri Endpoint;
        private static string AuthKey;

        private Lazy<DocumentClient> internalProvider = new Lazy<DocumentClient>(() =>
        {
            var client = new DocumentClient(Endpoint, AuthKey,
            new ConnectionPolicy
            {
                ConnectionMode = ConnectionMode.Direct,
                ConnectionProtocol = Protocol.Tcp
            });

            return client;
        });
        

        public DocumentClientProvider(Uri endpoint, string authKey)
        {
            Endpoint = endpoint;
            AuthKey = authKey;
        }

        public DocumentClient Create()
        {
            return internalProvider.Value;
        }
    }
}
