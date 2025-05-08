using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using _00.Work.MOON._01.Script.Managers;
using AYellowpaper.SerializedCollections;

namespace _00.Work.MOON._01.Script.Core.DI
{
    [DefaultExecutionOrder(-10)] // 가장 빨리 실행되게
    public class Injector : MonoBehaviour
    {
        private const BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        private readonly Dictionary<Type, object> _registry = new Dictionary<Type, object>();

        private void Awake()
        {
            // 인터페이스를 구현한 모든 MonoBehaviour를 찾아 Provider로 등록
            IEnumerable<IDependencyProvider> providers = GetMonoBehaviours().OfType<IDependencyProvider>();
            foreach (var provider in providers)
            {
                RegisterProvider(provider);
            }

            // [Inject]가 붙은 모든 MonoBehaviour에 대해 주입 수행
            IEnumerable<MonoBehaviour> injectables = GetMonoBehaviours().Where(IsInjectable);
            foreach (var injectable in injectables)
            {
                Inject(injectable);
            }
        }

        private void Inject(MonoBehaviour injectableMono)
        {
            Type type = injectableMono.GetType();

            // 기존 필드 주입 로직...
            
            IEnumerable<FieldInfo> injectableFields = type.GetFields(_bindingFlags)
                .Where(f => Attribute.IsDefined(f, typeof(InjectAttribute)));

            foreach (FieldInfo field in injectableFields)
            {
                Type fieldType = field.FieldType;

                // SerializedDictionary<MonoScript, MonoBehaviour> 특수 처리
                if (fieldType.IsGenericType
                    && fieldType.GetGenericTypeDefinition() == typeof(SerializedDictionary<,>)
                    && fieldType.GetGenericArguments()[0] == typeof(SerializableType)
                    && fieldType.GetGenericArguments()[1] == typeof(MonoBehaviour))
                {
                    // 에디터에서만 동작하는 MonoScript.GetClass() 사용
                    var dict = (SerializedDictionary<SerializableType, MonoBehaviour>)field.GetValue(injectableMono);
                    // 안전하게 ToList()를 통해 키 복사
                    foreach (var key in dict.Keys.ToList())
                    {
                        Type scriptType = key;
                        object instance = Resolve(scriptType);
                        Debug.Assert(instance != null, $"Inject instance not found for {scriptType.Name}");
                        dict[key] = (MonoBehaviour)instance;
                    }
                    field.SetValue(injectableMono, dict);
                    continue;
                }

                // 일반 필드 주입
                object resolved = Resolve(fieldType);
                Debug.Assert(resolved != null, $"Inject instance not found for {fieldType.Name}");
                field.SetValue(injectableMono, resolved);
            }

            // [Inject] 메서드 주입
            IEnumerable<MethodInfo> injectableMethods = type.GetMethods(_bindingFlags)
                .Where(m => Attribute.IsDefined(m, typeof(InjectAttribute)));

            foreach (var method in injectableMethods)
            {
                var paramTypes = method.GetParameters().Select(p => p.ParameterType).ToArray();
                var paramValues = paramTypes.Select(Resolve).ToArray();
                method.Invoke(injectableMono, paramValues);
            }
        }

        private object Resolve(Type type)
        {
            _registry.TryGetValue(type, out object instance);
            return instance;
        }

        private bool IsInjectable(MonoBehaviour mono)
        {
            MemberInfo[] members = mono.GetType().GetMembers(_bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }

        private void RegisterProvider(IDependencyProvider provider)
        {
            // 클래스 자체에 [Provide]가 있으면 바로 등록
            if (Attribute.IsDefined(provider.GetType(), typeof(ProvideAttribute)))
            {
                _registry[provider.GetType()] = provider;
                return;
            }

            // Provide 어트리뷰트가 붙은 메서드를 찾아 반환 타입으로 인스턴스 생성 후 등록
            foreach (var method in provider.GetType().GetMethods(_bindingFlags))
            {
                if (!Attribute.IsDefined(method, typeof(ProvideAttribute))) continue;
                var returnType = method.ReturnType;
                var instance = method.Invoke(provider, null);
                Debug.Assert(instance != null, $"Provided method {method.Name} returned null.");
                _registry[returnType] = instance;
            }
        }

        private IEnumerable<MonoBehaviour> GetMonoBehaviours()
        {
            return FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
        }
    }
}
