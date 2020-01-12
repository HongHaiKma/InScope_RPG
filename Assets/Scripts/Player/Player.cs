using UnityEngine;
using System.Collections;

public class Player : Character
{
    [SerializeField]
    private Stat health;
    [SerializeField]
    private float healthValue;
    
    [SerializeField]
    private Stat mana;
    [SerializeField]
    private float manaValue;

    private float initHealth = 100;
    private float initMana = 50;
    [SerializeField]
    private GameObject[] spellPrefab;

    [SerializeField]
    private Block[] blocks;

    [SerializeField]
    private Transform[] exitPoints;
    private int exitIndex = 2;
    private Transform target;
    public Transform MyTarget {get; set;}

    protected override void Start()
    {
        health.Initialize(initHealth, initHealth);
        mana.Initialize(initMana, initMana);

        base.Start();
    }

    protected override void Update()
    {
        GetInput();

        // InLigthOfSight();

        Debug.Log(LayerMask.GetMask("Block"));

        base.Update();
    }

    public void GetInput()
    {
        direction = Vector2.zero;

        if(Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
            mana.MyCurrentValue -= 10;
        }
        if(Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
            mana.MyCurrentValue += 10;
        }

        //if direction only = not +=, player move only 1 direction
        if(Input.GetKey(KeyCode.W))
        {
            exitIndex = 0;
            direction += Vector2.up;
        }
        if(Input.GetKey(KeyCode.A))
        {
            exitIndex = 3;
            direction += Vector2.left;
        }
        if(Input.GetKey(KeyCode.S))
        {
            exitIndex = 2;
            direction += Vector2.down;
        }
        if(Input.GetKey(KeyCode.D))
        {
            exitIndex = 1;
            direction += Vector2.right;
        }
        
    }

    public IEnumerator Attack(int spellIndex)
    {
        isAttacking = true;

        myAnimator.SetBool("attack", isAttacking);

        yield return new WaitForSeconds(1f);

        Instantiate(spellPrefab[spellIndex], exitPoints[exitIndex].position, Quaternion.identity);

        StopAttack();
    }

    public void CastSpell(int spellIndex)
    {
        Block();

        if(MyTarget != null && !isAttacking && !IsMoving && InLigthOfSight())
        {
            attackRoutine = StartCoroutine(Attack(spellIndex));
        }
    }

    public bool InLigthOfSight()
    {
        Vector3 targetDirection = (MyTarget.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, Vector2.Distance(transform.position, MyTarget.transform.position), 256);

        if(hit.collider == null)
        {
            return true;
        }

        return false;
    }

    public void Block()
    {
        foreach (Block b in blocks)
        {
            b.Deactivate();
        }

        blocks[exitIndex].Activate();
    }
}
