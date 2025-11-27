namespace OOP2.Application.Common.Model
{
    public class SuccessResponse<TValue> where TValue : class
    {
        public int? Id { get; set; }
        public  bool? IsSuccess { get; init; }
        public TValue? Value { get; init; }

        public SuccessResponse()
        {
            
        }
        public SuccessResponse(bool? value , int? id , TValue Item)
        {
            IsSuccess = value;
            Value = Item;
            Id = id;
        }
    }
}
