using System;

[Serializable]
public struct EffectData
{
    public EEffect EffectType;
    public float EffectDuration;
    public float EffectStrength;

    /// <summary>
    /// Specifies the interval, in seconds, at which effects are applied.
    /// </summary>
    public float EffectTickRate;
    
    /// <summary>
    /// only fill if effect is scating
    /// </summary>
    public ScateData ScateData;
}
