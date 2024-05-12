namespace E_commerce.Core.Sharing
{
    public class ProductParams
    {
        public int MaxPageSize { get; set; } = 15;
        private int pageSize = 3;

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value > MaxPageSize ? MaxPageSize: value; }
        }

        public int PageNumber { get; set; } = 1;
        public string Sort { get; set; }
        public int? CategoryId { get; set; }
        private string search;

        public string Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }

    }
}
