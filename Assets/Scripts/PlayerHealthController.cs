using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField] private HealthBarManager healthBarManager;

    private const int PISTOL_BULLET_DAMAGE = 8;
    private const int M4_BULLET_DAMAGE = 5;

    private float health = 300f;

    float Health
    {
        get => health;
        set => health = value;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            GetComponent<CharacterController>().enabled = false;
            Debug.Log("Player KO");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PistolBullet"))
        {
            Health -= PISTOL_BULLET_DAMAGE;
            healthBarManager.UpdateHealthBars(Health);
            Debug.Log("Pistol hit");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("M4Bullet"))
        {
            Health -= M4_BULLET_DAMAGE;
            healthBarManager.UpdateHealthBars(Health);
            Debug.Log("M4 hit");
            Destroy(other.gameObject);
        }
    }
}
