using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMentPlayer : MonoBehaviour
{
    public static MoveMentPlayer instance;
    public float horizontal;
    public float speed;
    private float jumpPower;
    private bool isFacingRight = true;
    public bool doubleJump;
    public bool isTakeDamaging;

    [SerializeField] private InputMobile inputMobile;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public AnimationPlayer animationPlayer;
    [SerializeField] private PlayerAttack playerAttack;

    public bool falled = false;
    private AudioManager audioManager;
    public bool grounded = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        horizontal = 0;
        speed = 5f;
        jumpPower = 19f;
        audioManager = AudioManager.instance;
    }
    void Update()
    {
        if(GameManager.instance.gameState == GAMESTATE.START)
        {
            if (!inputMobile.isMoving)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
            }
            if (Input.GetButtonDown("Jump"))
            {
                JumpUp();
            }
            SetDoubleJump();
            Flip();
        }
    }

    public void JumpUp()
    {
        if (grounded || doubleJump)
        {
            if (!playerAttack.attacking)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                doubleJump = !doubleJump;

                audioManager.PlaySfx("jump");
                animationPlayer.EmitParticle("jumpParticle", new Vector3(0, -0.1f, 0), new Vector3(5f, 5f, 1));
            }
        }
    }

    public void SetDoubleJump()
    {
        if(!inputMobile.jumbutton)
        {
            if (grounded && (!Input.GetButton("Jump")))
            {
                doubleJump = false;
            }
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.instance.gameState == GAMESTATE.START)
        {
            if (!playerAttack.attacking && !isTakeDamaging)
            {
                rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }
    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    public void DelayFall()
    {
        animationPlayer.falledAnim = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWay") && !grounded)
        {
            Vector3 normal = collision.GetContact(0).normal;
            if (/*normal == Vector3.up*/true)
            {
                grounded = true;
            }
        }
        
        if(collision.gameObject.CompareTag("Ground"))
        {
            if(GameManager.instance.gameState == GAMESTATE.END)
            {
                animationPlayer.ChangeAnimationState("deadGround");
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }
        }

        if(collision.gameObject.CompareTag("Coin2"))
        {
            ObjectPool.instance.Return(collision.gameObject);
            GameManager.instance.coin++;
            UiPresent.Instance.UpdateCoinText();
            AudioManager.instance.PlaySfx("coin");
            
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("OneWay"))
        {

            grounded = false;
            //falled = false;
            //animationPlayer.falledAnim = false;
        }
    }
    public void SpawnEffectCollect(int index, Vector3 pos, float time)
    {
        GameObject effect = ObjectPool.instance.Get(ObjectPool.instance.collectEffects[index]);
        effect.transform.position = pos;
        effect.transform.localScale = Vector3.one * 5;
        effect.SetActive(true);
        StartCoroutine(DelayReturn(effect, time));
    }

    IEnumerator DelayReturn(GameObject x, float time)
    {
        yield return new WaitForSeconds(time);
        ObjectPool.instance.Return(x);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.SetActive(false);
            GameManager.instance.coin++;
            SpawnEffectCollect(0, collision.gameObject.transform.position, 0.3f);
            GameManager.instance.UpdateScore(1);
            UiPresent.Instance.UpdateCoinText();
            AudioManager.instance.PlaySfx("coin");
        }
    }
}
