namespace OOP2.Application.Common.Model
{
    public class SuccessResponse
    {
        public  bool IsSuccess { get; init; }

        public SuccessResponse()
        {
            
        }
        public SuccessResponse(bool value)
        {
            IsSuccess = value;
        }
    }
}
