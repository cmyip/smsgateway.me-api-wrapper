using System.Collections.Generic;

namespace SmsGateway.MeApiWrapperCore
{
    public class QueryParameter
    {
        public List<FilterParam> Filters { get; set; } = new List<FilterParam>();
        public long Limit { get; set; } = 1000;
        public long Offset { get; set; } = 0;

        public List<OrderByParam> OrderBy { get; set; } = new List<OrderByParam>()
        {
            new OrderByParam()
            {
                Direction = "desc",
                Field = "id"
            },
        };
    }

    public class OrderByParam
    {
        public string Field { get; set; }
        public string Direction { get; set; }
    }

    public class FilterParam
    {
        public string Field { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }
    }
}