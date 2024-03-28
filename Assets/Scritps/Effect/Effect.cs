using UnityEngine;

[CreateAssetMenu(fileName = "Effect", menuName = "Effect/Effect")]
public abstract class Effect : ScriptableObject
{
    public int value;
    public EffectTargetType targetType;
    public abstract void Execute(CharacterBase from,CharacterBase target);

}
