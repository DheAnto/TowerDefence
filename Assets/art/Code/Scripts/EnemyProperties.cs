using UnityEngine;

public class EnemyProperties : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;
    [SerializeField] private float flashDuration = 0.1f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

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
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }

    }
    public void TakeDamage(int dmg)
    {
        Flash();
        hitPoints -= dmg;
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.onEnemyDestroy.Invoke();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void Flash()
    {
        spriteRenderer.color = Color.white;
        Invoke("ResetColor", flashDuration);

    }

    private void ResetColor()
    {
        spriteRenderer.color = originalColor;
    }


}