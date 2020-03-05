using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public static EnemyManager instance {
        get {
            if (_instance == null) {
                _instance = new EnemyManager();
            }
            return _instance;
        }
    }
    private static EnemyManager _instance = null;
    private List<Enemy> enemies = new List<Enemy>();

    public IEnumerator Attacking()
    {
        foreach (var enemy in enemies)
        {
            enemy.Attack();
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void Add(Enemy enemy) {
        enemies.Add(enemy);
    }

    public void Remove(Enemy enemyToRemove)
    {
        enemies.Remove(enemyToRemove);
        if (enemies.Count == 0)
        {
            DraftCardManager.instance.Draft();
        }
    }
}
