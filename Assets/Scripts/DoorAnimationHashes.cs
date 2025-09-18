using UnityEngine;

public static class DoorAnimationHashes
{
    public static readonly int Open = Animator.StringToHash(nameof(Open));
    public static readonly int Close = Animator.StringToHash(nameof(Close));
}