using PreCompiledFunctionSample.Data;
using PreCompiledFunctionSample.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PreCompiledFunctionSample.Services
{
    class FunctionService : IFunctionService
    {
        private readonly DocumentDBRepository<FunctionModel> repository;

        public FunctionService(DocumentDBRepository<FunctionModel> repository)
        {
            this.repository = repository;
        }
    }

    interface IFunctionService { }
}
