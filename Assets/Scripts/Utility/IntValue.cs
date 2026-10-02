using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "IntValue", menuName = "Scriptable Objects/IntValue")]
public class IntValue : ScriptableObject
{
    public int Value {  get; private set; }
    public UnityEvent OnValueChanged;

    public void SetValue(int value)
    {
        this.Value = value;
        OnValueChanged?.Invoke();
    }
}
