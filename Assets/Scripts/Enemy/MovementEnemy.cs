using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public Vector3 target;
    public float speed;
    public EnemyAnimation enemyAnimation;
    void Start()
    {
        target = a.transform.position;
        enemyAnimation.ChangeAnimationState("run");
    }

    void Update()
    {
        if(Mathf.Abs(target.x - transform.position.x) > 0.1f)
        {
            transform.Translate((target - transform.position).normalized * speed * Time.deltaTime);
        }
        else
        {
            if (target == a.transform.position)
                target = b.transform.position;
            else
                target = a.transform.position;
            speed = 0;
            enemyAnimation.ChangeAnimationState("idle");
            Invoke(nameof(DelayChangeDirection), 2);
        }
    }

    public void DelayChangeDirection()
    {
        speed = 1;
        Flip();
        enemyAnimation.ChangeAnimationState("run");
    }

    public void Flip()
    {
        Vector3 _localscale = transform.localScale;
        _localscale.x *= -1;
        transform.localScale = _localscale;
    }
}
