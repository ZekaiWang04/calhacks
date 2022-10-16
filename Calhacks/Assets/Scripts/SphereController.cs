using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SphereController : MonoBehaviour
{
    public float gravity;
    public float fanForce;
    public float fanHeight;
    public float bumperImpulse;
    public float hitCooldown;
    public float respawnTimer;

    public float hitTimer;
    public bool frozen;

    public DisplayGyroscope displayGyroscope;
    public HealthController health;
    public Transform spawn;
    public TextMeshProUGUI gameEndText;
    public GameManager gameManager;
    public GameObject tapText;

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
        if (respawnTimer > 0)
        {
            respawnTimer -= Time.deltaTime;
        }

        if (rb.constraints == RigidbodyConstraints.FreezeAll && respawnTimer <= 0)
        {
            if (Input.touchCount != 0)
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                ResetZero();
                tapText.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = zeroQuaternion * cam.transform.forward;
        rb.AddForce(gravity * new Vector3(-dir.x, dir.y, -dir.z), ForceMode.Force);

        //displayGyroscope.UpdateText(cam.transform.forward.ToString());

        //rb.AddForce(gravity * Vector3.down, ForceMode.Force);
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

        if (collision.collider.CompareTag("Bumper"))
        {
            rb.AddForce(bumperImpulse * collision.GetContact(0).normal, ForceMode.Impulse);
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
        transform.position = spawn.position + new Vector3(0, 0.05f, 0);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        tapText.SetActive(true);
        respawnTimer = 0.2f;

        gameObject.SetActive(true);
    }

    public void Die()
    {
        gameObject.SetActive(false);
        gameEndText.text = "Player " + GameManager.Player + " died! Player " + (3 - GameManager.Player) + " wins!";
        gameEndText.gameObject.SetActive(true);
    }
}
