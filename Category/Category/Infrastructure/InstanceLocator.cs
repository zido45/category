using System;
using System.Collections.Generic;
using System.Text;

namespace Category.Infrastructure
{
    using ViewModels;
 
    public class InstanceLocator
    {
        public MainViewModel Main { get; set; }
        public InstanceLocator()
        {
            Main = new MainViewModel();
        }
    }
}
