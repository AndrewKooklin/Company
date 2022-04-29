using System.Collections.ObjectModel;

namespace Company
{
    class Changes
    {
        private MainWindow w;
        public ObservableCollection<Change> ChangesList { get; set; }

        public Changes(MainWindow W)
        {
            this.w = W;
            this.ChangesList = new ObservableCollection<Change>();
        }
    }
}
