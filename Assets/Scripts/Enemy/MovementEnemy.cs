using UnityEngine;

public class MovementEnemy : MonoBehaviour
{
    public GameObject a;
    public GameObject b;
    public Vector3 target;
    [HideInInspector]
    public float speed;
    public EnemyAnimation enemyAnimation;
    public HealEnemy healEnemy;
    void Start()
    {
        speed = 1;
        target = a.transform.position;
        enemyAnimation.ChangeAnimationState("run");
    }

    void Update()
    {
        if(healEnemy.currentHeal > 0)
        {
            if (Mathf.Abs(target.x - transform.position.x) > 0.1f)
            {
                transform.Translate((new Vector3(target.x, transform.position.y) - transform.position).normalized * speed * Time.deltaTime);
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
    }

    public void SetTarget(int value)
    {
        if(value == 0)
            target = a.transform.position;
        else 
            target = b.transform.position;
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
