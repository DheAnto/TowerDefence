using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;
    private void Start()
    {
        var difficolta = MainMenu.Instance.difficoltaScelta;
        switch (difficolta)
        {
            case MainMenu.Difficolta.Facile:
                break;
            case MainMenu.Difficolta.Medio:
                hitPoints = Mathf.RoundToInt(hitPoints * 1.5f);
                break;
            case MainMenu.Difficolta.Difficile:
                hitPoints = Mathf.RoundToInt(hitPoints * 2f);
                break;
        }
    }
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }
}