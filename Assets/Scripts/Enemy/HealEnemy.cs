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
                if (!takeDamed)
                {
                    TakeDame(atk, new Vector2(x, 1));
                }
            }
        }
        else if (collision.CompareTag("Sword"))
        {
            AudioManager.instance.PlaySfx("swordstop");
            ObjectPool.instance.Return(collision.gameObject);
            TakeDame(10, Vector2.up);
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
    public void SpawnEffectCollect(Vector3 pos, float time)
    {
        GameObject effect = ObjectPool.instance.Get(ObjectPool.instance.blood);
        effect.transform.position = pos;
        effect.transform.localScale = Vector3.one * 0.5f;
        effect.SetActive(true);
        StartCoroutine(DelayReturn(effect, time));
    }

    IEnumerator DelayReturn(GameObject x, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.instance.Return(x);
    }
    public void TakeDame(int atk, Vector2 direcionAttackArea)
    {
        takeDamed = true;
        currentHeal -= atk;
        SpawnEffectCollect(transform.position, 1f);
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
        yield return new WaitForSeconds(1);
    }
    public void Die()
    {
        animationEnemy.PlayAnimation("deadGround");
        boxCollider.enabled = false;
        GameManager.instance.kill++;
        GameManager.instance.UpdateScore(10);
        StartCoroutine(DelayEnableAnimator());
    }
    IEnumerator DelayEnableAnimator()
    {
        yield return new WaitForSeconds(1.5f);
        animationEnemy.animator.enabled = false;
        gameObject.SetActive(false);
    }
}
