using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// An effect which increases the block of all targets when played.
[CreateAssetMenu(menuName = "Card Game Stuff/Effects/Increase Block", fileName = "IncreaseBlock")]
public class IncreaseBlockEffect : CardEffect
{
    public int amount = 5;

    public override IEnumerator ApplyEffect(ActionContext context)
    {
        foreach (FieldEntity target in context.targets)
        {
            target.AddBlock(amount);
        }
        yield break;
    }
}
