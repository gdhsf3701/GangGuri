using System.Collections.Generic;
using _00.Work.MOON._01.Script.Entities;
using _00.Work.MOON._01.Script.SO.Entity;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Players
{
    public class ChangeModel : MonoBehaviour,IEntityComponent
    {
        private Dictionary<string, GameObject> _model = new Dictionary<string, GameObject>();
        private string _currentModelName;
        public void Initialize(Entity entity)
        {
            
        }

        public void ChangeCarModel(string carPrefabName)
        {
            if (_currentModelName == carPrefabName ) return;
            if(_model.ContainsKey(carPrefabName) == false) return;
            if (string.IsNullOrEmpty(_currentModelName) == false)
            {
                _model[_currentModelName].SetActive(false);
            }
            _currentModelName = carPrefabName;
            _model[_currentModelName].SetActive(true);
        }

        public void AddDictionary(string key, GameObject obj)
        {
            if(obj == null)return;
            if(_model.ContainsKey(key)) return;
            GameObject real = Instantiate(obj, transform.position, transform.rotation, transform);
            real.GetComponentInChildren<MeshCollider>().enabled = false;
            _model.Add(key, real);
            real.SetActive(false);
        }
    }
}
