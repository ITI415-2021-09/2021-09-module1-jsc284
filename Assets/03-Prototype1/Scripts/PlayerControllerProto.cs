using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControllerProto : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public bool projectileIsOnTheGround = true;

    private Rigidbody rb;
    private int count;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 5;

        SetCountText();
        winTextObject.SetActive(false);

    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Lives: " + count.ToString();
        if(count <= -1)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if(Input.GetButtonDown("Jump") && projectileIsOnTheGround)
        {
            rb.AddForce(new Vector3(0, 10, 0), ForceMode.Impulse);
            projectileIsOnTheGround = false;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            projectileIsOnTheGround = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            other.gameObject.SetActive(false);
            count = count - 1;

            SetCountText();
        }

        if (other.gameObject.CompareTag("Heal"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
        }

        // If there are -1 lives left, restart the game.
        if (count == -1)
        {
            SceneManager.LoadScene("Main-Prototype 1");
        }
    }
}
