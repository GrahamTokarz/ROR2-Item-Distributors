using System;
using System.Collections.Generic;
using System.Text;
using BepInEx.Configuration;

namespace AutoItemPickup
{
    public class CachedConfigEntry<T>
    {
        protected ConfigWrapper<T> _wrapper;
        protected T _value;

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!_value.Equals(value))
                    OnValueChanged?.Invoke(value);
                _value = value;
                _wrapper.Value = value;
            }
        }

        public event Action<T> OnValueChanged;

        public void RefreshValue()
        {
            Value = _wrapper.Value;
        }
        
        public CachedConfigEntry(ConfigWrapper<T> wrapper)
        {
            _wrapper = wrapper;
            RefreshValue();
        }

        public static implicit operator CachedConfigEntry<T>(ConfigWrapper<T> wrapper)
        {
            return new CachedConfigEntry<T>(wrapper);
        }

        public static implicit operator T(CachedConfigEntry<T> wrapper)
        {
            return wrapper.Value;
        }
    }

    public class CachedConfigEntry<T, T2>
    {
        protected ConfigWrapper<T> _wrapper;
        protected T2 _value;

        protected Func<T, T2> wrapperToValue;
        protected Func<T2, T> valueToWrapper;

        public T2 Value
        {
            get
            {
                return _value;
            }
            set
            {
                if (!_value.Equals(value))
                    OnValueChanged?.Invoke(value);
                _value = value;
                _wrapper.Value = valueToWrapper(value);
            }
        }

        public void RefreshValue()
        {
            Value = wrapperToValue(_wrapper.Value);
        }

        public event Action<T2> OnValueChanged;

        public CachedConfigEntry(ConfigWrapper<T> wrapper, Func<T, T2> wrapperToValue, Func<T2, T> valueToWrapper)
        {
            _wrapper = wrapper;
            this.wrapperToValue = wrapperToValue;
            this.valueToWrapper = valueToWrapper;
            RefreshValue();
        }

        public static implicit operator T2(CachedConfigEntry<T, T2> wrapper)
        {
            return wrapper.Value;
        }
    }

    namespace Extensions
    {
        public static class Extensions
        {
            public static CachedConfigEntry<T, T2> ToCachedConfigEntry<T, T2>(this ConfigWrapper<T> wrapper, Func<T, T2> wrapperToValue, Func<T2, T> valueToWrapper)
            {
                return new CachedConfigEntry<T, T2>(wrapper, wrapperToValue, valueToWrapper);
            }
        }
    }
}
