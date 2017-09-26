using TableWatcher.Base;

namespace TableWatcher.Factory
{
    public class TableWatcherStrategy<T> where T : class
    {
        private ITableWatcher<T> TableWatcherAbstract;

        public TableWatcherStrategy()
        {
            TableWatcherAbstract = new AdapterFactory<T>().GetAdapter();
        }

        public void InitializeTableWatcher()
        {
            TableWatcherAbstract.InitializeTableWatcher();
        }

        public void StartTableWatcher()
        {
            TableWatcherAbstract.StartTableWatcher();
        }

        public void StopTableWatcher()
        {
            TableWatcherAbstract.StopTableWatcher();
        }
    }
}
