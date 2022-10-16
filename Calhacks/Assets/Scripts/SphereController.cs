using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SphereController : MonoBehaviour
{
    public float gravity;
    public float fanForce;
    public float fanHeight;
    public float hitCooldown;

    public float hitTimer;

    public DisplayGyroscope displayGyroscope;
    public HealthController health;
    public Transform spawn;
    public TextMeshProUGUI gameEndText;
    public GameManager gameManager;

    public Rigidbody rb;
    public Camera cam;
    public Quaternion zeroQuaternion;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //cam = Camera.current;
        ResetZero();

        hitTimer = 0;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = zeroQuaternion * cam.transform.forward;
        rb.AddForce(gravity * new Vector3(-dir.x, dir.y, -dir.z), ForceMode.Force);
        //rb.AddForce(gravity * Vector3.down, ForceMode.Force);

        displayGyroscope.UpdateText(cam.transform.forward.ToString());

        //if (Input.GetKey(KeyCode.W))
        //    rb.AddForce(Vector3.forward);
        //if (Input.GetKey(KeyCode.A))
        //    rb.AddForce(Vector3.left);
        //if (Input.GetKey(KeyCode.S))
        //    rb.AddForce(Vector3.back);
        //if (Input.GetKey(KeyCode.D))
        //    rb.AddForce(Vector3.right);
    }

    public void ResetZero()
    {
        zeroQuaternion.SetFromToRotation(cam.transform.forward, Vector3.down);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Fan"))
        {
            //Debug.Log("a");
            //other.transform.position.y
            rb.AddForce(Mathf.Lerp(fanForce, 0, (transform.position.y - other.transform.position.y) / fanHeight) * Vector3.up, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitTimer <= 0 && collision.collider.CompareTag("Damage"))
        {
            if (health.TakeDamage())
            {
                Die();
            }
            else
            {
                hitTimer = hitCooldown;
            }
        }

        if (collision.collider.CompareTag("Goal"))
        {
            gameManager.Next();
        }
    }

    public void Respawn()
    {
        if (health.TakeDamage())
        {
            Die();
        }
        else
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        transform.position = spawn.position + new Vector3(0, 0.06f, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        health.ResetHealth();

        gameObject.SetActive(true);
    }

    public void Die()
    {
        gameObject.SetActive(false);
        gameEndText.text = "Player " + GameManager.Player + " died! Player " + (3 - GameManager.Player) + " wins!";
        gameEndText.gameObject.SetActive(true);
    }
}
