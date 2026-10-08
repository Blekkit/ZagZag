using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatValue", menuName = "Scriptable Objects/FloatValue")]
public class FloatValue : ScriptableObject
{
    public float Value { get; private set; }
    public float MaxValue { get; private set; }
    public UnityEvent OnValueChanged;

    public void SetValue(float value)
    {
        this.Value = value;
        OnValueChanged?.Invoke();
    }

    public void SetMaxValue(float maxValue)
    {
        this.MaxValue = maxValue;
    }
}
