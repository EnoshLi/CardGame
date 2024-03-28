using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Effect/DamageEffect")]
public class DamageEffect : Effect
{
    public override void Execute(CharacterBase from, CharacterBase target)
    {
        switch (targetType)
        {
            case EffectTargetType.Self:
                break;
            case EffectTargetType.Target:
                target.TakeDamage(value);
                Debug.Log($"执行了{value}点伤害");
                break;
            case EffectTargetType.All:
                foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    enemy.GetComponent<CharacterBase>().TakeDamage(value);
                }
                break;
        }
    }
}
