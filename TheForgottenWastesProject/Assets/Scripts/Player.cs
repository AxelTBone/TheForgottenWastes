using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Animator")]
    [SerializeField] private Animator anime;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private Rigidbody2D body;
    [SerializeField] private new Camera camera;
    private Vector2 movement;
    private Vector2 mousePos;

    [Header("Shooting Settings")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletForce = 20f;

    [Header("Damage Target")]
    public HealthManager healthManagerScript;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        anime.SetFloat("Speed", movement.sqrMagnitude);
        if (!(anime.GetFloat("Speed") > 0))
        {
            FindObjectOfType<AudioManager>().Play("PlayerWalk");
        }
    }

    private void FixedUpdate()
    {
        body.MovePosition(body.position + movement * moveSpeed * Time.fixedDeltaTime);
        Vector2 directionLook = mousePos - body.position; // Two vectors subtracted point to each other
        float angle = Mathf.Atan2(directionLook.y, directionLook.x) * Mathf.Rad2Deg; // Returns angle
        body.rotation = angle;
        if (healthManagerScript.currentHealth <= 0)
        {
            FindObjectOfType<AudioManager>().Stop("Theme");
            FindObjectOfType<AudioManager>().Stop("PlayerWalk");
            FindObjectOfType<AudioManager>().Play("PlayerDeath");
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    private void Shoot() // Creates bullet
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        bulletBody.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        FindObjectOfType<AudioManager>().Play("PlayerGunshot");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            FindObjectOfType<AudioManager>().Play("OnHit");
            healthManagerScript.TakeDmg(15);
        }
        if(collision.collider.CompareTag("HealthPack"))
        {
            FindObjectOfType<AudioManager>().Play("OnHeal");
            healthManagerScript.AddHealth(30);
        }
        if (collision.collider.CompareTag("Supplies"))
        {
            FindObjectOfType<AudioManager>().Play("PickUpSupplies");
            ScoreManager.instance.AddSupplies();
        }
    }
}