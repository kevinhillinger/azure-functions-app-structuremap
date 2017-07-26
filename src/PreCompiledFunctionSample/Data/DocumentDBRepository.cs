using Microsoft.Azure.Documents.Client;
using PreCompiledFunctionSample.Model;
using System;
using System.Collections.Generic;

namespace PreCompiledFunctionSample.Data
{
    class DocumentDBRepository<T>
        where T : class, new()
    {
        private string databaseId;
        private string collectionId;

        private static readonly Dictionary<Type, string> collectionIds = new Dictionary<Type, string>() {
            { typeof(FunctionModel), "FunctionCollectionId" },
        };

        public DocumentDBRepository(string databaseId, DocumentClient client)
        {
            this.databaseId = databaseId;
            this.collectionId = collectionIds[typeof(T)];
        }
    }
}