using System.ComponentModel;
using UnityEngine;

namespace MemoryGame.View
{
    public abstract class ViewBaseClass<T> : MonoBehaviour where T : class, INotifyPropertyChanged
    {
        [SerializeField] private T _model;

        public T Model
        {
            get => _model;
            set
            {
                if (_model == value) { return; }
                if (null != _model) { _model.PropertyChanged -= OnModelPropertyChanged; }
                _model = value;
                Model.PropertyChanged += OnModelPropertyChanged;
            }
        }

        protected abstract void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e);
    }
}