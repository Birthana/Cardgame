using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public static EnemyManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemyManager();
            }
            return _instance;
        }
    }
    private static EnemyManager _instance = null;
    private List<Enemy> _enemies = new List<Enemy>();
    public List<Enemy> enemies { get => _enemies; }

    public IEnumerator Attacking()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Attack();
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void Add(Enemy enemy)
    {
        _enemies.Add(enemy);
    }

    public void Remove(Enemy enemyToRemove)
    {
        _enemies.Remove(enemyToRemove);
        if (_enemies.Count == 0)
        {
            // DraftCardManager.instance.Draft();
        }
    }
}
