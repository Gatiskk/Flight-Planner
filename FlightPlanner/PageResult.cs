using System;

namespace FlightPlanner
{
    public class PageResult
    {
        public int Page { get; set; }
        public int totalItems { get; set; }
        public Array Items { get; set; }

        public PageResult(Array _items)
        {
            Page = 0;
            Items = _items;
            totalItems = _items.Length;
        }
    }
}