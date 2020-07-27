using System;

namespace QuestionOverdose.Models
{
    public class PageModel
    {
        public PageModel(int count, int pageNumber, int pageSize)
        {
            TotalItems = count;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public int PageNumber { get; private set; }

        public int TotalPages { get; private set; }

        public int TotalItems { get; private set; }
    }
}
