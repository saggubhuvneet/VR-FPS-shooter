using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

public class EnenmyController : MonoBehaviour
{
    public float LookRadius = 10f;
    private Transform player;
    public GameObject AlienEnemy;
    public Animator Alien;
    public GameObject bloodParticle;
    //public AudioClip attackAudio;


    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI scoreText1;
    private static int Score;

    public float enemyHealth;
    public Image enemyHealthImage;

    public static bool isAttacking;
    private bool isDying = false;

    //public Module manager;

    NavMeshAgent Agent;
    AudioSource audioSource;

    void Start()
    {
        //Score = 0;
        enemyHealth = 1f;
        Agent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();

        isAttacking = false;

        player = GameObject.Find("XRPlayer").transform;
        scoreText = GameObject.Find("score").GetComponent<TextMeshProUGUI>();
        scoreText1 = GameObject.Find("score_1").GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        enemyHealthImage.fillAmount = enemyHealth;
        scoreText.text = "Score-" + Score.ToString();
        scoreText1.text = "Score-" + Score.ToString();

        if (enemyHealth <= 0f)
        {
            //EnemyDie();
            StartCoroutine(EnemyDie());
            //Destroy(gameObject);
            isDying = true;
            return;
        }
        

        if (distance <= Agent.stoppingDistance)
        {
            AttackPlayer();   //--------------------------attack
            FaceTarget();
            isAttacking = true;
            return;
        }
        

        if (distance <= LookRadius)
        {
            isAttacking = false;
            FollowPlayer();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isDying == false)
        {
            if (isAttacking == false)
            {
                if (other.CompareTag("bullet"))
                {
                    FollowPlayer();
                }
            }
        }
        
    }

    void FaceTarget()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //-----------------------------------------------------------------------------------------------------------------Following player
    void FollowPlayer()
    {
        Agent.SetDestination(player.position);
        AlienEnemy.GetComponent<Animator>().Play("enemy_walk");
    }

    //----------------------------------------------------------------------------------------------------------------Attacking player
    void AttackPlayer()
    {
        if(isDying == false)
        {
            //audioSource.PlayOneShot(attackAudio);

            Debug.Log("attack is active");
            AlienEnemy.GetComponent<Animator>().Play("enemyAttack");
            Gun_Manager.playerHealth -= 0.005f;
        }
        
    }

    IEnumerator EnemyDie()
    {
     
        bloodParticle.SetActive(true);
        isAttacking = false;
        Debug.Log("die is called");
        Score++;

        AlienEnemy.GetComponent<Animator>().Play("enemyDie");
        //Alien.SetBool("isDying", true);
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
        //gameObject.SetActive(false);



    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}
