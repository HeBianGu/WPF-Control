using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace H.Data.Test
{
    public class Students : ObservableCollection<Student>
    {
        public Students()
        {

        }
        public Students(IEnumerable<Student> collection) : base(collection)
        {

        }
    }
}
