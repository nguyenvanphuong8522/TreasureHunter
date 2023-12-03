using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class HealEnemy : MonoBehaviour
{
    public EnemyAnimation animationEnemy;
    [SerializeField] private int maxHeal;
    [SerializeField] public int currentHeal;
    [SerializeField] private Rigidbody2D rb;
    private float force;
    [SerializeField] private MovementEnemy movementEnemy;
    [SerializeField] private BoxCollider2D boxCollider;
    public bool takeDamed;

    private void Start()
    {
        force = 4f;
        takeDamed = false;
        currentHeal = maxHeal;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackArea"))
        {
            if (currentHeal > 0)
            {
                int atk = collision.gameObject.GetComponentInParent<PlayerAttack>().atk;
                float x = collision.transform.parent.localScale.x;
                float deltaX = transform.position.x - collision.transform.position.x;
                if(deltaX > 0 && transform.localScale.x < 0)
                {
                    movementEnemy.Flip();
                    movementEnemy.SetTarget(0);
                }
                else if(deltaX < 0 && transform.localScale.x > 0)
                {
                    movementEnemy.Flip();
                    movementEnemy.SetTarget(1);
                }
                if (!takeDamed)
                {
                    TakeDame(atk, new Vector2(x, 1));
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.contacts[0].normal == Vector2.up)
            {
                if (currentHeal <= 0)
                    Die();
                FreezeAll();
                takeDamed = false;
            }
        }
    }


    public void TakeDame(int atk, Vector2 direcionAttackArea)
    {
        takeDamed = true;
        currentHeal -= atk;
        EffectHit(direcionAttackArea);
    }
    public void EffectHit(Vector2 direcionAttackArea)
    {
        UnFreezePosition();
        SetAnimationHit();
        AddForce(direcionAttackArea);
        StopAllCoroutines();
        StartCoroutine(DelayMovement());
        
    }
    public void SetAnimationHit()
    {
        if (currentHeal == 0)
            animationEnemy.ChangeAnimationState("die");
        else
            animationEnemy.ChangeAnimationState("hit");
    }

    public void AddForce(Vector2 direcionAttackArea)
    {
        direcionAttackArea.Normalize();
        Vector2 directForce = new Vector2(direcionAttackArea.x, 2);
        if (currentHeal == 0)
            force = 5.5f;
            
        rb.AddForce(directForce * force, ForceMode2D.Impulse);
    }
    public void UnFreezePosition()
    {
        boxCollider.isTrigger = false;
        rb.constraints &= ~RigidbodyConstraints2D.FreezeAll;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    public void FreezeAll()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        boxCollider.isTrigger = true;
    }
    IEnumerator DelayMovement()
    {
        movementEnemy.speed = 0;
        yield return new WaitForSeconds(1);
        movementEnemy.speed = 1f;
        DelayAnimation();
    }

    public void DelayAnimation()
    {
        if (currentHeal > 0)
        {
            if (movementEnemy.speed == 0)
                animationEnemy.ChangeAnimationState("idle");
            else
                animationEnemy.ChangeAnimationState("run");
        }
    }
    public void Die()
    {
        animationEnemy.PlayAnimation("deadGround");
        movementEnemy.speed = 0;
        boxCollider.enabled = false;
    }
}
