using UnityEngine;

public class HealEnemy : MonoBehaviour
{
    public EnemyAnimation animationEnemy;
    [SerializeField] private int maxHeal;
    [SerializeField] private int currentHeal;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private MovementEnemy movementEnemy;
    [SerializeField] private BoxCollider2D boxCollider;

    private void Start()
    {
        currentHeal = maxHeal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackArea"))
        {
            int atk = collision.gameObject.GetComponentInParent<PlayerAttack>().atk;
            
            boxCollider.isTrigger = false;
            animationEnemy.ChangeAnimationState("hit");
            Invoke(nameof(DelayAnimation), 0.25f);
            float x = collision.transform.parent.localScale.x;
            EffectHit(new Vector2(x, 1));
            TakeDame(atk);
        }
    }
    public void EffectHit(Vector2 direcionAttackArea)
    {
        rb.constraints &= ~RigidbodyConstraints2D.FreezeAll;
        rb.constraints = ~RigidbodyConstraints2D.FreezePosition;
        rb.AddForce(direcionAttackArea.normalized * force, ForceMode2D.Impulse);
    }

    public void DelayAnimation()
    {
        if (movementEnemy.speed == 0)
            animationEnemy.ChangeAnimationState("idle");
        else
            animationEnemy.ChangeAnimationState("run");
    }

    public void TakeDame(int atk)
    {
        currentHeal -= atk;
        if(currentHeal <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        rb.constraints &= ~RigidbodyConstraints2D.FreezeAll;
        transform.rotation = Quaternion.Euler(0, 0, -45);
        boxCollider.isTrigger = true;
    }
}
