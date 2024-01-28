#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public enum FacePartType : byte
{
    Nose,
    EyeLeft,
    EyeRight,
    EyebrowLeft,
    EyebrowRight,
    Mouth,
}

[System.Serializable]
public sealed class FaceRigPart
{
    [SerializeField] FacePartType type;
    [SerializeField] Transform handle = null!;
    [SerializeField] Transform? BonePosition;
    [SerializeField] Transform? BoneRotation;
    [SerializeField] float controlDirection; // z angle

    [SerializeField] Vector2 positionMinus;
    [SerializeField] Vector2 positionZero;
    [SerializeField] Vector2 positionPlus;
    [SerializeField] float rotationMinus; // z angle
    [SerializeField] float rotationZero; // z angle
    [SerializeField] float rotationPlus; // z angle

    public float Rate { get; private set; } = 0f;

    public FacePartType Type => type;
    public Transform Handle => handle;
    public Vector2 ControlDirection => Quaternion.Euler(0f, 0f, controlDirection) * Vector2.right;

    public ref readonly Vector2 PositionMinus => ref positionMinus;
    public ref readonly Vector2 PositionZero => ref positionZero;
    public ref readonly Vector2 PositionPlus => ref positionPlus;
    public Quaternion RotationMinus => Quaternion.Euler(0f, 0f, rotationMinus);
    public Quaternion RotationZero => Quaternion.Euler(0f, 0f, rotationZero);
    public Quaternion RotationPlus => Quaternion.Euler(0f, 0f, rotationPlus);

    public void ApplyTransform(float rate)
    {
        Rate = rate;

        if (BonePosition != null)
        {
            BonePosition.localPosition = CalcPosition(rate);
        }
        if (BoneRotation != null)
        {
            BoneRotation.localRotation = CalcRotation(rate);
        }
    }

    Vector2 CalcPosition(float rate)
    {
        if (rate < -1f || rate > 1f) throw new System.ArgumentOutOfRangeException(nameof(rate));

        if (rate == 0f) return positionZero;

        if (rate < 0f)
        {
            return Vector2.Lerp(positionMinus, positionZero, 1f + rate);
        }
        return Vector2.Lerp(positionZero, positionPlus, rate);
    }

    Quaternion CalcRotation(float rate)
    {
        if (rate < -1f || rate > 1f) throw new System.ArgumentOutOfRangeException(nameof(rate));

        if (rate == 0f) return RotationZero;

        if (rate < 0f)
        {
            return Quaternion.Lerp(RotationMinus, RotationZero, 1f + rate);
        }
        return Quaternion.Lerp(RotationZero, RotationPlus, rate);
    }
}

public sealed class FaceRigInfo : MonoBehaviour, IEnumerable<FaceRigInfo?>
{
    [SerializeField] FaceRigPart? nose;
    [SerializeField] FaceRigPart? eyeLeft;
    [SerializeField] FaceRigPart? eyeRight;
    [SerializeField] FaceRigPart? eyebrowLeft;
    [SerializeField] FaceRigPart? eyebrowRight;

    FaceRigPart?[]? parts;

    public System.ReadOnlySpan<FaceRigPart?> Parts => parts ??= new[] { nose, eyeLeft, eyeRight, eyebrowLeft, eyebrowRight };

    public bool TryGetNose([NotNullWhen(true)] out FaceRigPart? value) => (value = nose) != null;
    public bool TryGetEyeLeft([NotNullWhen(true)] out FaceRigPart? value) => (value = eyeLeft) != null;
    public bool TryGetEyeRight([NotNullWhen(true)] out FaceRigPart? value) => (value = eyeRight) != null;
    public bool TryGetEyebrowLeft([NotNullWhen(true)] out FaceRigPart? value) => (value = eyebrowLeft) != null;
    public bool TryGetEyebrowRight([NotNullWhen(true)] out FaceRigPart? value) => (value = eyebrowRight) != null;

    public FaceRigPart? this[FacePartType type] => type switch
    {
        FacePartType.Nose => nose,
        FacePartType.EyeLeft => eyeLeft,
        FacePartType.EyeRight => eyeRight,
        FacePartType.EyebrowLeft => eyebrowLeft,
        FacePartType.EyebrowRight => eyebrowRight,
        FacePartType.Mouth => null,
        _ => null,
    };

    public System.ReadOnlySpan<FaceRigPart?>.Enumerator GetEnumerator() => Parts.GetEnumerator();

    IEnumerator<FaceRigInfo> IEnumerable<FaceRigInfo?>.GetEnumerator()
        => throw new System.NotImplementedException();

    IEnumerator IEnumerable.GetEnumerator()
        => throw new System.NotImplementedException();

#if UNITY_EDITOR
    [SerializeField, Range(-1f, 1f)] float noseRate = 0f;
    [SerializeField, Range(-1f, 1f)] float eyeLeftRate = 0f;
    [SerializeField, Range(-1f, 1f)] float eyeRightRate = 0f;
    [SerializeField, Range(-1f, 1f)] float eyebrowLeftRate = 0f;
    [SerializeField, Range(-1f, 1f)] float eyebrowRightRate = 0f;

    private void OnValidate()
    {
        if (UnityEditor.EditorApplication.isPlaying) return;

        if (TryGetNose(out var nose))
        {
            nose.ApplyTransform(noseRate);
        }
        if (TryGetEyeLeft(out var eyeLeft))
        {
            eyeLeft.ApplyTransform(eyeLeftRate);
        }
        if (TryGetEyeRight(out var eyeRight))
        {
            eyeRight.ApplyTransform(eyeRightRate);
        }
        if (TryGetEyebrowLeft(out var eyebrowLeft))
        {
            eyebrowLeft.ApplyTransform(eyebrowLeftRate);
        }
        if (TryGetEyebrowRight(out var eyebrowRight))
        {
            eyebrowRight.ApplyTransform(eyebrowRightRate);
        }
    }
#endif //UNITY_EDITOR
}
