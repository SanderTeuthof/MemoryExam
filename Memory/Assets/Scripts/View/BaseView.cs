using UnityEngine;
using System.ComponentModel;

namespace Memory.View
{
    public class BaseView<T> : MonoBehaviour where T : class, INotifyPropertyChanged
    {
        private T _model;

        public T Model
        {
            get { return _model; }
            set
            {
                if (_model != null)
                {
                    _model.PropertyChanged -= Model_PropertyChanged;
                }
                _model = value;
                if (_model != null)
                {
                    _model.PropertyChanged += Model_PropertyChanged;
                }
            }
        }

        public virtual void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            // Override this method in derived classes to respond to property changes in the model
        }
    }
}
