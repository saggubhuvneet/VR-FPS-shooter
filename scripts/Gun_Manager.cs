using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TechXR.Core.Sense;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Gun_Manager : MonoBehaviour
{
    //------------------------------------------------------ bullet control
    public Transform BulletSpawner;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public GameObject Shoot_particle;
    public int MaxAmmo = 25;
    private int CurrentAmmo;
    private float ReloadTime = 4f;
    private bool isReloading = false;


    public AudioClip fireAudio;
    public AudioClip reloadAudio;
    //public AudioClip gameOverAudio;

    public TextMeshProUGUI ammoText;
    public TextMeshProUGUI ammoText1;

    //------------------------------------------------------ player information 
    public Image playerHealthImage;
    public Image playerHealthImage1;
    public static float playerHealth;
    public Animator Canvas;

    AudioSource audioSource;

    private void Start()
    { 
        playerHealth = 1f;
        CurrentAmmo = MaxAmmo;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        playerHealthImage.fillAmount = playerHealth;
        playerHealthImage1.fillAmount = playerHealth;

        ammoText.text = CurrentAmmo.ToString();
        ammoText1.text = CurrentAmmo.ToString();

        if (SenseInput.GetButtonDown("B"))
        {
            SceneManager.LoadScene("MAinMenu");
        }
        if (playerHealth <= 0)
        {
            //audioSource.PlayOneShot(gameOverAudio);

            PlayerDie();
            return;
        }
        if (EnenmyController.isAttacking == true)
        {
            IsBeingAttacked();
        }

        if (isReloading)
            return;
        if(CurrentAmmo <= 0)
        {
            StartCoroutine(ReloadGun());
            return;
        }
        if (SenseInput.GetButton("L"))
            FireBullet();
        if (SenseInput.GetButtonDown("U"))
            StartCoroutine(ReloadGun()); 
    }
    IEnumerator ReloadGun ()
    {
        isReloading = true;

        audioSource.PlayOneShot(reloadAudio);
        GetComponent<Animator>().Play("reload");
        yield return new WaitForSeconds(ReloadTime);
        CurrentAmmo = MaxAmmo;

        isReloading = false;
    }
    public void FireBullet()
    {
        CurrentAmmo--;

        audioSource.PlayOneShot(fireAudio);
        GetComponent<Animator>().Play("fire");
        var Bullet = Instantiate(bulletPrefab, BulletSpawner.position, Quaternion.identity);
        Bullet.GetComponent<Rigidbody>().velocity = BulletSpawner.forward * bulletSpeed;
        Instantiate(Shoot_particle, BulletSpawner.position, Quaternion.identity);
    }
    public void IsBeingAttacked()
    {
        Canvas.GetComponent<Animator>().Play("canvasAttack");
    }
    public void PlayerDie()
    {
        Debug.Log(" Player dead");
        Canvas.GetComponent<Animator>().Play("canvasDie");
    }
}
