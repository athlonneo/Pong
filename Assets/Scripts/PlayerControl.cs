using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public KeyCode upButton = KeyCode.W;
    public KeyCode downButton = KeyCode.S;
    public KeyCode skillButton = KeyCode.LeftShift;

    private bool skillFlag = false;
    public Vector3 skillScale3D;
    public float skillScale = 2.0f;
    public int skillDuration = 3;
    public int skillCooldown = 6;

    public float speed = 10.0f;

    public float yBoundary = 9.0f;

    private Rigidbody2D rigidBody2D;

    private int score;

    private ContactPoint2D lastContactPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        skillScale3D = new Vector3(1, skillScale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = rigidBody2D.velocity;

        if (Input.GetKey(upButton))
        {
            velocity.y = speed;
        }
        else if (Input.GetKey(downButton))
        {
            velocity.y = -speed;
        }
        else
        {
            velocity.y = 0.0f;
        }

        rigidBody2D.velocity = velocity;

        Vector3 position = transform.position;

        if (position.y > yBoundary)
        {
            position.y = yBoundary;
        }
        else if (position.y < -yBoundary)
        {
            position.y = -yBoundary;
        }

        transform.position = position;

        if (skillFlag == false)
        {
            if (Input.GetKey(skillButton))
            {
                ActivateSkill();
            }
        }
    }

    public void IncrementScore()
    {
        score++;
    }

    public void ResetScore()
    {
        score = 0;
    }

    public int Score
    {
        get { return score; }
    }

    public ContactPoint2D LastContactPoint
    {
        get { return lastContactPoint; }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Ball"))
        {
            lastContactPoint = collision.GetContact(0);
        }
    }

    void ActivateSkill()
    {
        skillFlag = true;
        transform.localScale = skillScale3D;
        Invoke("DeactivateSkill", skillDuration);
        Invoke("ReadySkill", skillCooldown);
    }

    void DeactivateSkill()
    {
        transform.localScale = new Vector3(1,1,1);
    }

    void ReadySkill()
    {
        skillFlag = false;
    }

    public void InitializeSkill()
    {
        DeactivateSkill();
        ReadySkill();
    }
}
