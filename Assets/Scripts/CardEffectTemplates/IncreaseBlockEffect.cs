using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Card Game Stuff/Effects/Increase Block", fileName="IncreaseBlock")]
public class IncreaseBlockEffect : CardEffect
{
    public int amount = 5;

    public override void ApplyEffect(ActionContext context) {
        foreach (FieldEntity target in context.targets) {
            target.AddBlock(amount);
        }
    }
}
