namespace OOP2.Application.Common.Model
{
    public abstract class RequestHandler<TRequest,TResault> where TResault : class where TRequest : class
    {
        public Guid RequestId = Guid.NewGuid(); 

        public async Task<Resault<TResault>> ProccessRequestAsync(TRequest request)
        {
            var resault = new Resault<TResault>();
            if (! await AuthorizeRequest(request)) 
            {
                resault.setUnauthorized();
                return resault;
            }
            await HandlePostRequestAsync(request, resault);
            return resault;
        }

        protected abstract Task<Resault<TResault>> HandlePostRequestAsync(TRequest request, Resault<TResault> resault);
        protected abstract Task<Resault<TResault>> HandleGetRequestAsync(TRequest request, Resault<TResault> resault);
        protected abstract Task<Resault<TResault>> HandlePutRequestAsync(TRequest request, Resault<TResault> resault);
        protected abstract Task<Resault<TResault>> HandleDeleteRequestAsync(TRequest request, Resault<TResault> resault);
        protected abstract Task<bool> AuthorizeRequest(TRequest request);
    }
}
