using System;
using UnityEngine;

[Serializable]
public struct DoorAnimations
{
    [SerializeField] private AnimationClip _opening;
    [SerializeField] private AnimationClip _closing;

    public int Opening { get { return GetHash(_opening); } }
    public int Closing { get { return GetHash(_closing); } }

    private int GetHash(AnimationClip clip)
        => Animator.StringToHash(clip.name);
}