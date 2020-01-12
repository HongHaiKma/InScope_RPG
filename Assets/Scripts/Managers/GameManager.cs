using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Player player;

    void Start()
    {
        
    }


    void Update()
    {
        ClickTarget();
    }

    public void ClickTarget()
    {
        if(Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity, 512);

            if(hit.collider != null)
            {
                if(hit.collider.tag == "Enemy")
                {
                    player.MyTarget = hit.transform;
                }
            }
            else
            {
                player.MyTarget = null;
            }
        }
    }
}
