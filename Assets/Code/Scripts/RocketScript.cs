using UnityEngine;

public class RocketScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rocketRb;

    [Header("Attributes")]
    [SerializeField] private float rocketSpeed = 1f;
    [SerializeField] private int rocketDamage = 1;
    [SerializeField] private float rocketExplosionRadius = 1.5f;
    [SerializeField] private LayerMask enemyLayerMask;

    private Transform target;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rocketRb.linearVelocity = direction * rocketSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Explode();
    }

    private void Explode(){ 
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, rocketExplosionRadius, enemyLayerMask);

        foreach (Collider2D col in hits)
        {
            Health hp = col.GetComponent<Health>();
            if (hp != null)
                hp.TakeDamage(rocketDamage);
        }

    Destroy(gameObject);
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rocketExplosionRadius);
    }
}
