using NeoMix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoMix.ViewModel
{
    public class ViewsVM
    {
        public List<View> ViewsAll;
        public int Count;
        public List<View> ViewsType;

        public ViewsVM(List<View> viewsAll, int count, List<View> viewsType)
        {
            ViewsAll = viewsAll;
            Count = count;
            ViewsType = viewsType;
        }
    }
}