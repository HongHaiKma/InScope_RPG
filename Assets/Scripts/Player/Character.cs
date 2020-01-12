using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public int speed;
    protected Vector2 direction;

    private Rigidbody2D myRigidbody;

    protected Animator myAnimator;

    public bool IsMoving
    {
        get
        {
            return (direction.x != 0 || direction.y != 0);
        }
    }
    protected bool isAttacking = false;
    protected Coroutine attackRoutine;

    protected virtual void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }


    protected virtual void Update()
    {
        HandleLayers();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        myRigidbody.velocity = direction.normalized * speed;
        // Debug.Log(direction.magnitude);
        // transform.Translate(direction * speed * Time.deltaTime);
    }

    public void HandleLayers()
    {
        if(IsMoving)
        {
            ActivateLayer("WalkLayer");
            myAnimator.SetFloat("x", direction.x);
            myAnimator.SetFloat("y", direction.y);

            StopAttack();
        }
        else if(isAttacking)
        {
            ActivateLayer("AttackLayer");
        }
        else
        {   
            ActivateLayer("IdleLayer");
        }
    }
    
    public void ActivateLayer(string layerName)
    {
        for(int i = 0; i < myAnimator.layerCount; i++)
        {
            myAnimator.SetLayerWeight(i, 0);
        }

        myAnimator.SetLayerWeight(myAnimator.GetLayerIndex(layerName), 1);
    }

    public void StopAttack()
    {
        isAttacking = false;
        myAnimator.SetBool("attack", isAttacking);

        if(attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
        }
    }
}