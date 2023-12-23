using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace H.Data.Test
{
    public class TestViewModels : ObservableCollection<TestViewModel>
    {
        public TestViewModels()
        {

        }
        public TestViewModels(IEnumerable<TestViewModel> collection) : base(collection)
        {

        }
    }
}
