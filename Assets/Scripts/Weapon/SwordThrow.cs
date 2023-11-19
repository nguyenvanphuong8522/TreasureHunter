using DG.Tweening;
using UnityEngine;

public class SwordThrow : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public Animator animator;
    public BoxCollider2D box;
    public CamShake camSake;
    private void OnEnable()
    {
        speed = 3;
        box.isTrigger = false;
        animator.Play("throw");
        Invoke(nameof(DisableSword), 3f);
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * speed * transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("OneWay"))
        {
            speed = 0;
            animator.Play("embeded");
            box.isTrigger = true;
            AudioManager.instance.PlaySfx("swordstop");
            camSake.ShakeCam();
        }
    }
    public void DisableSword()
    {
        ObjectPool.instance.Return(gameObject);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
