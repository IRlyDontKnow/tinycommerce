using System;
using System.Collections.Generic;

namespace TinyCommerce.BuildingBlocks.Application.Queries
{
    public class PagedResult<T> : List<T>
    {
        public PagedResult(IEnumerable<T> items, int totalCount, int currentPage, int perPage)
        {
            Items = items;
            TotalCount = totalCount;
            CurrentPage = currentPage;
            PerPage = perPage;
            TotalPages = (int) Math.Ceiling(totalCount / (double) perPage);
        }

        public IEnumerable<T> Items { get; }
        public int TotalCount { get; }
        public int CurrentPage { get; }
        public int PerPage { get; }
        public int TotalPages { get; }
        public bool HasPreviousPage => (CurrentPage > 1);
        public bool HasNextPage => (CurrentPage < TotalPages);
    }
}