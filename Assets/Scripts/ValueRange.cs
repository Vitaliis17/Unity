using UnityEngine;
using System;

[Serializable]
public struct ValueRange<T>
{
    [field: SerializeField] public T Min { get; private set; }
    [field: SerializeField] public T Max { get; private set; }
}