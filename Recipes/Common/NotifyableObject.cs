using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Recipes.Common {
    public class NotifyableObject : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChanged([CallerMemberName] string name = "") {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
