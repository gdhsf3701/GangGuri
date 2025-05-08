using System;
using UnityEngine;

namespace _00.Work.MOON._01.Script.Managers
{
    [Serializable]
    public class SerializableType : ISerializationCallbackReceiver, IEquatable<SerializableType>
    {
        [SerializeField]
        private string typeName;

        public Type Type
        {
            get => string.IsNullOrEmpty(typeName) ? null : System.Type.GetType(typeName);
            set => typeName = value?.AssemblyQualifiedName;
        }

        // 생성자
        public SerializableType(Type type)
        {
            Type = type;
        }

        // 암시적 변환 연산자
        public static implicit operator SerializableType(Type type) => new SerializableType(type);
        public static implicit operator Type(SerializableType serializableType) => serializableType?.Type;

        // ISerializationCallbackReceiver
        public void OnBeforeSerialize()
        {
            typeName = Type?.AssemblyQualifiedName;
        }

        public void OnAfterDeserialize()
        {
            if (!string.IsNullOrEmpty(typeName))
                Type = System.Type.GetType(typeName);
        }

        // ==, != 연산자 오버로드
        public static bool operator ==(SerializableType a, SerializableType b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(SerializableType a, SerializableType b)
            => !(a == b);

        // IEquatable<SerializableType> 구현
        public bool Equals(SerializableType other)
        {
            if (other is null) return false;
            // 둘 다 Type이 null이면 동등, 하나만 null이면 비동등
            if (Type == null && other.Type == null) return true;
            if (Type == null || other.Type == null) return false;
            // AssemblyQualifiedName 기준 비교
            return Type.AssemblyQualifiedName == other.Type.AssemblyQualifiedName;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as SerializableType);
        }

        public override int GetHashCode()
        {
            // typeName 이 null 이면 0, 아니면 문자열 해시코드 사용
            return typeName?.GetHashCode() ?? 0;
        }
    }
}
