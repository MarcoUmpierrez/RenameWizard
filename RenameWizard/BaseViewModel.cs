using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RenameWizard
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<string, object> _properties = [];

        public event PropertyChangedEventHandler PropertyChanged;

        public T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (_properties.ContainsKey(propertyName))
            {
                return (T)_properties[propertyName];
            }
            else
            {
                return default;
            }
        }

        public void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            _properties[propertyName] = value;
            OnPropertyChanged(propertyName);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
