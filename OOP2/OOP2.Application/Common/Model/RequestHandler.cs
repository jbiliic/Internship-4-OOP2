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
            await HandleRequestAsync(request, resault);
            return resault;
        }

        protected abstract Task<Resault<TResault>> HandleRequestAsync(TRequest request, Resault<TResault> resault);
        protected abstract Task<bool> AuthorizeRequest(TRequest request);
    }
}
