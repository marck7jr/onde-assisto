using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.ComponentModel
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
            {
                return false;
            }
            else
            {
                field = value;
                OnNotifyPropertyChanged(propertyName);
                return true;
            }
        }

        private void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
