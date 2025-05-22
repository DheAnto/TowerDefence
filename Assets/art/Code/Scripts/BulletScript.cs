using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;  


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
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * bulletSpeed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0, 0, angle - 90);

    }

    private void OnCollisionEnter2D(Collision2D other)
{
    // Se colpisce un albero (Layer "Tree"), distruggi il proiettile
    if (other.gameObject.layer == LayerMask.NameToLayer("tree"))
    {
        Destroy(gameObject);
        return;
    }

    
    other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);  
    Destroy(gameObject);
}

}
