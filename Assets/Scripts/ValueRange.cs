using UnityEngine;
using System;

[Serializable]
public struct ValueRange<T>
{
    [field: SerializeField] public T MinValue { get; private set; }
    [field: SerializeField] public T MaxValue { get; private set; }
}