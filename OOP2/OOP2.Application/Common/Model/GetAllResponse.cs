using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2.Application.Common.Model
{
    public class GetAllResponse<TValue>
    {
        public IReadOnlyList<TValue> Items { get; set; } = new List<TValue>();
        public bool IsSuccess => Items != null && Items.Count > 0;
        public bool isEmpty => Items == null || Items.Count == 0;
        public GetAllResponse()
        {
            
        }
        public GetAllResponse(List<TValue> items)
        {
            Items = items;
        }
    }
}
