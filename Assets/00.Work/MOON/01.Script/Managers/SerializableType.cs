using System;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Managers
{
    [Serializable]
    public class SerializableType : ISerializationCallbackReceiver
    {
        [SerializeField]
        private string typeName;

        public Type Type
        {
            get => string.IsNullOrEmpty(typeName) ? null : Type.GetType(typeName);
            set => typeName = value?.AssemblyQualifiedName;
        }

        // Type을 인자로 받는 생성자
        public SerializableType(Type type)
        {
            Type = type;
        }

        // Type에서 SerializableType으로의 암시적 변환
        public static implicit operator SerializableType(Type type) => new SerializableType(type);

        // SerializableType에서 Type으로의 암시적 변환
        public static implicit operator Type(SerializableType serializableType) => serializableType?.Type;

        // ISerializationCallbackReceiver 구현
        public void OnBeforeSerialize()
        {
            // 타입 이름을 저장하기 전에 AssemblyQualifiedName으로 변환
            typeName = Type?.AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            // 역직렬화 후 타입 이름을 기반으로 Type 객체를 복원
            if (!string.IsNullOrEmpty(typeName))
            {
                Type = Type.GetType(typeName);
            }
        }
    }
}