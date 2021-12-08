using System.Collections.ObjectModel;

namespace System.Collections.Generic
{
    public static class ObsevableCollectionExtention
    {

        public static void Add<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
        public static void AddClear<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            collection.Clear();
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
