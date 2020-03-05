using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private int block;
    public TextMeshPro healthText;
    //Temorary. Need to decouple.
    [SerializeField] private EnemyManager enemies;

    private void Start()
    {
        currentHealth = maxHealth;
        block = 0;
        UpdateUI();
        enemies = FindObjectOfType<EnemyManager>();
    }

    public void TakeDamage(int damage)
    {
        if (block < damage)
        {
            int leftoverDamage = damage - block;
            block = 0;
            currentHealth -= leftoverDamage;
        }
        else
        {
            block -= damage;
        }
        UpdateUI();
        if (currentHealth <= 0)
        {
            enemies.Remove(gameObject.GetComponent<Enemy>());
            Destroy(this.gameObject);
        }
    }

    public void AddBlock(int block)
    {
        this.block += block;
        UpdateUI();
    }

    public void RemoveAllBlock()
    {
        block = 0;
        UpdateUI();
    }

    public void SetMaxHealth(int playerHealth)
    {
        maxHealth = playerHealth;
        currentHealth = maxHealth;
        UpdateUI();
    }

    public void UpdateUI()
    {
        healthText.text = "" + (currentHealth + block) + " / " + maxHealth;
    }
}
