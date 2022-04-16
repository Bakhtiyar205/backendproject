using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndProject.Utilities.Pagination
{
    public class Paginate<T>
    {
        public Paginate(List<T> datas, int currentPage, int totalPage )
        {
            Datas = datas;
            CurrentPage = currentPage;
            TotalPage = totalPage;
        }
        public List<T> Datas { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
    }
}
