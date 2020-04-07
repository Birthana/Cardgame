using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// An effect which removes all block from all targets when played.
[CreateAssetMenu(menuName = "Card Game Stuff/Effects/Remove Block", fileName = "RemoveBlock")]
public class RemoveBlockEffect : CardEffect
{
    public int amount = 5;

    public override IEnumerator ApplyEffect(ActionContext context)
    {
        foreach (FieldEntity target in context.targets)
        {
            target.ClearBlock();
        }
        yield return new WaitForSeconds(0.5f);
    }
}
