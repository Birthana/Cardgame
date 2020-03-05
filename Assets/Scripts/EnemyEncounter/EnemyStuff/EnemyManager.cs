using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<Enemy> enemies;

    public void SpawnEnemies()
    {
        enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
    }

    public IEnumerator Attacking()
    {
        foreach (var enemy in enemies)
        {
            enemy.Attack();
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void Remove(Enemy enemyToRemove)
    {
        enemies.Remove(enemyToRemove);
        if (enemies.Count == 0)
        {
            //DraftCardManager.instance.Draft();
        }
    }
}
