using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coin : MonoBehaviour
{
    
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Collider2D coinCollider;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider == playerCollider)
        {
            CollectBottle();
        }
        else 
        {
            // allows other object to pass through
            coinCollider.enabled = false;
            Invoke("EnableCollider", 2.0f);
        }
    }

    private void EnableCollider()
    {
        coinCollider.enabled = true;
    }

    private void CollectBottle()
    {
        gameObject.SetActive(false);
        Score.scoreValue += 1;
        Debug.Log("Score is: " + Score.scoreValue);
        //GameManager.instance.IncreaseScore();
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
