﻿namespace E_commerce.API.Helpers
{
    public class Pagination<T> where T :class
    {

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageNumber, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}
