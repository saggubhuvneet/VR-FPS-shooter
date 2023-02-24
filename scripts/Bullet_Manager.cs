using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Manager : MonoBehaviour
{
    public float life = 3;
    public EnenmyController enemyController;

    private void Awake()
    {
        Destroy(gameObject, life);

    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //enemyController.enemyHealth -= 0.05f;
            other.gameObject.GetComponent<EnenmyController>().enemyHealth -= 0.05f;
           
        }
    }

}
