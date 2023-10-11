using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMentPlayer : MonoBehaviour
{
    public float horizontal;
    private float speed;
    [SerializeField] private InputMobile inputMobile;
    public float jumpPower = 16f;
    private bool isFacingRight = true;
    public bool doubleJump;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AnimationPlayer animationPlayer;
    [SerializeField] private GameObject jumpParticle;
    [SerializeField] private GameObject moveParticle;
    [SerializeField] private bool falled = false;
    [SerializeField] private PlayerAttack playerAttack;
    private AudioManager audioManager;

    public bool grounded = false;

    private void Start()
    {
        speed = 6f;
        jumpPower = 20f;
        audioManager = AudioManager.instance;
        StartCoroutine(RunParticle());
    }
    void Update()
    {
        if (IsGrounded())
        {
            if (!playerAttack.attacking)
            {
                if (horizontal != 0)
                {
                    animationPlayer.ChangeAnimationState("run02");
                }
                else
                {
                    animationPlayer.ChangeAnimationState("idle02");
                }
            }
            if (!falled)
            {
                audioManager.PlaySfx("fall");
                GameObject newParticle = ObjectPool.instance.Get(ObjectPool.instance.fallParticle);
                newParticle.SetActive(true);
                newParticle.transform.position = transform.position + new Vector3(0, -0.18f, 0);
                newParticle.transform.localScale = new Vector3(5f, 5f, 1);
                animationPlayer.ChangeAnimationState("idle02");
                falled = true;
            }

        }
        else
        {
            if (!playerAttack.attacking)
            animationPlayer.ChangeAnimationState("fall02");
        }

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

    IEnumerator RunParticle()
    {
        while (true)
        {
            if (IsGrounded() && horizontal != 0 && !playerAttack.attacking)
            {
                audioManager.PlaySfx("footstep");
                GameObject newParticle = ObjectPool.instance.Get(ObjectPool.instance.moveParticle);
                newParticle.SetActive(true);
                newParticle.transform.position = transform.position + new Vector3(0, -0.1f, 0);
                newParticle.transform.localScale = new Vector3(5f, 5f, 1);
                Vector3 _localScale = newParticle.transform.localScale;
                newParticle.transform.localScale = new Vector3(_localScale.x * transform.localScale.x, _localScale.y, _localScale.z);
            }
            yield return new WaitForSeconds(0.25f);
        }
    }
    public void JumpUp()
    {
        if (IsGrounded() || doubleJump)
        {
            if (!playerAttack.attacking)
            {
                animationPlayer.ChangeAnimationState("jump02");
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                doubleJump = !doubleJump;

                audioManager.PlaySfx("jump");
                GameObject newParticle = ObjectPool.instance.Get(ObjectPool.instance.jumpParticle);
                newParticle.SetActive(true);
                newParticle.transform.position = transform.position + new Vector3(0, -0.1f, 0);
                newParticle.transform.localScale = new Vector3(5f, 5f, 1);
            }
        }
    }

    public void SetDoubleJump()
    {
        if(grounded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }
    }
    private void FixedUpdate()
    {
        if (!playerAttack.isAttackPressed)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    public bool IsGrounded()
    {
        return grounded;
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

    public void EmitParticleMovement(string nameParticle, Vector3 offsetPos)
    {
        GameObject newParticle = ObjectPool.instance.Get(ObjectPool.instance.fallParticle);
        newParticle.SetActive(true);
        newParticle.transform.position = transform.position + new Vector3(0, -0.18f, 0);
        newParticle.transform.localScale = new Vector3(5f, 5f, 1);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 normal = collision.GetContact(0).normal;
            if (normal == Vector3.up)
            {
                grounded = true;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            falled = false;
        }
    }
}
