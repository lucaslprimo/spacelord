using UnityEngine;
using UnityEngine.UI;

public class Player : Being
{
    private static readonly string HORIZONTAL = "Horizontal";
    private static readonly string VERTICAL = "Vertical";
    private static readonly string GOING_UP = "goingUp";
    private static readonly string GOING_RIGHT = "goingRight";
    private static readonly string GOING_LEFT = "goingLeft";
    private static readonly string GOING_DOWN = "goingDown";

    public Image[] healthDots;
    public GameObject hurtSound;
    private Rigidbody2D body;
    private Vector2 moveAmmount;
    private Animator animator;
    private Animator animCamera;
    public SceneTransictions sceneTransictions;
    public float maxVelocity;

    private AudioSource jetpackSound;
    public ParticleSystem particleSystemJetpack;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        animCamera = Camera.main.GetComponent<Animator>();
        jetpackSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis(HORIZONTAL), Input.GetAxis(VERTICAL));
        moveAmmount = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            particleSystemJetpack.startLifetime = 5;

            if(!jetpackSound.isPlaying)
                jetpackSound.Play();

            if (moveInput.x != 0)
            {
                animator.SetBool(GOING_UP, false);
                animator.SetBool(GOING_DOWN, false);
                if (moveInput.x > 0)
                {
                    animator.SetBool(GOING_RIGHT, true);
                    animator.SetBool(GOING_LEFT, false);
                }
                else
                {
                    animator.SetBool(GOING_RIGHT, false);
                    animator.SetBool(GOING_LEFT, true);
                }
            }
            else if (moveInput.y != 0)
            {
                animator.SetBool(GOING_RIGHT, false);
                animator.SetBool(GOING_LEFT, false);
                if (moveInput.y > 0)
                {
                    animator.SetBool(GOING_UP, true);
                    animator.SetBool(GOING_DOWN, false);
                }
                else
                {
                    animator.SetBool(GOING_UP, false);
                    animator.SetBool(GOING_DOWN, true);
                }
            }
        }
        else
        {
            particleSystemJetpack.startLifetime = 0;
            jetpackSound.Pause();
            animator.SetBool(GOING_UP, false);
            animator.SetBool(GOING_DOWN, false);
            animator.SetBool(GOING_RIGHT, false);
            animator.SetBool(GOING_LEFT, false);
        }
    }

    private void FixedUpdate()
    {
        body.AddForce(moveAmmount * speed);
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxVelocity);
    }

    internal void Heal(int amount)
    {
        UpdateHealth(health + amount);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        animCamera.SetTrigger("shake");
        Instantiate(hurtSound, transform.position, transform.rotation);
    }


    public override void UpdateHealth(int newHealth)
    {
        base.UpdateHealth(newHealth);
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        for(int i = 0; i<maxHealth; i++)
        {
            if(i < health)
            {
                healthDots[i].gameObject.SetActive(true);
            }
            else{
                healthDots[i].gameObject.SetActive(false);
            }
        }
    }

    public override void Die()
    {
        base.Die();
        sceneTransictions.LoadScene("Lose");
    }

    internal void EquipeWeapon(Weapon weaponToEquip)
    {
        Transform weaponPosition  = GameObject.FindGameObjectWithTag("Weapon").transform;
        Destroy(GameObject.FindGameObjectWithTag("Weapon"));
        Instantiate(weaponToEquip, weaponPosition.position, weaponPosition.rotation, transform);
    }
}
