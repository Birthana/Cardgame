using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance = null;
    public List<Enemy> enemies;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
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
            DraftCardManager.instance.Draft();
        }
    }
}
