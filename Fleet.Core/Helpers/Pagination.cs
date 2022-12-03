﻿using System.Collections.Generic;
using Fleet.Core.ApiModels;

namespace Fleet.Core.Helpers
{
    public class Pagination<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public ApiResponse<T> Data { get; set; }
        
        public Pagination( int pageIndex, int pageSize, int count, ApiResponse<T> data )
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
        }
    }
}