using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Entities
{
    public class Entity : MonoBehaviour
    {
        protected Dictionary<Type, IEntityComponent> _components;


        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IEntityComponent>();
            AddComponents();
            InitializeComponents();
        }

        private void AddComponents()
        {
            GetComponentsInChildren<IEntityComponent>().ToList()
                .ForEach(component => _components.Add(component.GetType(), component));
        }

        private void InitializeComponents()
        {
            _components.Values.ToList().ForEach(component => component.Initialize(this));
        }

        public T GetCompo<T>() where T : IEntityComponent
            => (T)_components.GetValueOrDefault(typeof(T));
    }
}