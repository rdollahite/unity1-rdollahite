using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformAttributes : MonoBehaviour
{
    public float offset = 2.5f;    

    private float threshold;

    [SerializeField] private BoxCollider2D platformCollider;
    [SerializeField] private GameObject character;
    [SerializeField] private Transform characterTransformer;

    // Start is called before the first frame update
    void Start()
    {
        platformCollider = GetComponent<BoxCollider2D>();
        character = GameObject.FindWithTag("Player");
    }

    private void disableCollider()
    {
        character = GameObject.FindWithTag("Player");
        characterTransformer = character.transform;

        threshold = transform.position.y + offset;
    
        if (characterTransformer.position.y < threshold)
        {
            platformCollider.enabled = false;
        }
        else
        {
            platformCollider.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        disableCollider();
    }
}
