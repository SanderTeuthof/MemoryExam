using Memory.Models;
using System;
using System.ComponentModel;
using UnityEngine;

namespace Memory.View
{
    public class PlayerView : BaseView<PlayerModel>
    {
        [SerializeField]
        private string _name;

        public EventHandler ActiveChanged;
        public EventHandler ScoreChanged;


        private void Start()
        {
            Model.Name = _name;
        }

        private void Update()
        {
            if(Model.IsActive)
            {
                Model.ElapsedTime += Time.deltaTime;
            }
            
        }

        public override void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsActive")
                ActiveChanged?.Invoke(this, new EventArgs());
            else if(e.PropertyName =="Score")
                ScoreChanged?.Invoke(this, new EventArgs());
            
        }
    }
}