using System.Collections.ObjectModel;
using System.Windows;

namespace Company
{
    public class ChangesRepository
    {
        private Window w;
        public ObservableCollection<Change> ChangesList { get; set; }

        public ChangesRepository(Window W)
        {
            this.w = W;
            this.ChangesList = new ObservableCollection<Change>();
        }
    }
}
