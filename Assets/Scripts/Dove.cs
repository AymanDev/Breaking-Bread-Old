using UnityEngine;
using Random = UnityEngine.Random;

public class Dove : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    [SerializeField] private float timeForRandomMovement = 2f;
    public int phase;

    [SerializeField] private float speedPush = 20f;

    private bool right = true;
    private Animator animator;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        InvokeRepeating("RandomMovement", 0f, timeForRandomMovement);
        InvokeRepeating("RandomAttack", 2f, 1f);
    }

    private void RandomAttack()
    {
        if (phase == 1) return;

        var chance = Random.Range(0, 100);
        if (chance < 25)
        {
            var doveObject = GameObject.Find("DoveBoss");
            var playerObject = GameObject.Find("Player");
            phase = 1;
            var x = playerObject.transform.position.x;

            var newPos = playerObject.transform.position;
            newPos.Set(x, doveObject.transform.position.y, doveObject.transform.position.z);
            doveObject.transform.position = newPos;
            animator.Play("dove_attack");
        }
        else if (chance >= 25 && chance < 60)
        {
            phase = 1;
            var num = Random.Range(0, 3);

            switch (num)
            {
                case 0:
                    animator.Play("dove_attack2");
                    break;
                case 1:
                    animator.Play("dove_attack2_1");
                    break;
                case 2:
                    animator.Play("dove_attack2_2");
                    break;
            }
        }
    }

    private void RandomMovement()
    {
        if (phase == 1)
        {
            rigidBody.isKinematic = true;
        }

        if (phase != 0) return;
        if (rigidBody.isKinematic)
        {
            rigidBody.isKinematic = false;
        }

        var side = Random.Range(0, 100);
        var force = new Vector2(-speedPush, 200f);

        if (side < 50)
        {
            force = new Vector2(speedPush, 200f);
            if (!right)
            {
                Flip();
                right = true;
            }
        }
        else if (right)
        {
            Flip();
            right = false;
        }

        rigidBody.AddForce(force);
    }

    private void Flip()
    {
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        //transform.localScale = theScale;
    }
}