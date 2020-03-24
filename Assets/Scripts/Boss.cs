using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : Being
{
    [System.Serializable]
    public struct Phase
    {
        public int meteorCount;
        public float timeBetweenMeteor;
    }


    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    private Vector2 targetPosition;

    public float restTime;
    public Transform[] spawnPoints;
    public GameObject meteor;
    private Transform player;
    private Animator bossAnim;
    private Animator camAnim;
  
    public Blink blinkPanel;
    private bool finishedAction;

    public Slider lifebar;
    public GameObject uiInfo;

    public GameObject bossMusic;
    public GameObject explosion;
    public GameObject explosionSound;

    public Phase[] phases;
    private int actualPhase = 0;

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camAnim = Camera.main.GetComponent<Animator>();
        bossAnim = GetComponent<Animator>();

        lifebar.gameObject.SetActive(true);
        uiInfo.SetActive(true);
        lifebar.maxValue = maxHealth;
        lifebar.maxValue = maxHealth;

        StartCoroutine(Summon());
    }

    public override void Die()
    {
        Destroy(bossMusic);
        Instantiate(explosion, transform.position, transform.rotation);
        Instantiate(explosionSound, transform.position, transform.rotation);
        base.Die();
    }

    public override void UpdateHealth(int newHealth)
    {
        base.UpdateHealth(newHealth);
        lifebar.value = newHealth;
        if(actualPhase == 0 && newHealth < maxHealth / 2)
        {
            actualPhase = 1;
        }
    }

    IEnumerator Summon()
    {
        bossAnim.SetBool("summon", true);
        yield return new WaitForSeconds(1);
        for (int i = 0; i < phases[actualPhase].meteorCount; i++) {
            camAnim.SetTrigger("shake");
            blinkPanel.Play();
            int random = Random.Range(0, spawnPoints.Length);
            Transform originPoint = spawnPoints[random];

            if(player == null) { break; }

            Vector2 direction = originPoint.position - player.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Quaternion rotationCorrection = Quaternion.AngleAxis(90, Vector3.forward);
            Instantiate(meteor, originPoint.position, rotation * rotationCorrection);
            yield return new WaitForSeconds(phases[actualPhase].timeBetweenMeteor);
        }
        bossAnim.SetBool("summon", false);
        finishedAction = true;
    }


    IEnumerator RestAndMove()
    {   
        bossAnim.SetTrigger("vanish");
        yield return new WaitForSeconds(2);
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        targetPosition = new Vector2(randomX, randomY);
        transform.position = targetPosition;
        yield return new WaitForSeconds(restTime);
       
        bossAnim.SetTrigger("restart");
        yield return new WaitForSeconds(1);
        StartCoroutine(Summon());
    }

    private void Update()
    {
        if(player != null)
        {
            if (finishedAction)
            {
                finishedAction = false;
                StartCoroutine(RestAndMove());
            }
        }
    }


}
