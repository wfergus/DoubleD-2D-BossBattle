using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

   

    [SerializeField]
    private float approachSpeed = 0.02f;
    [SerializeField]
    private float growthBound = 1.2f;
    [SerializeField]
    private float shrinkBound = 0.7f;
    private float currentRatio = 1;

    private SpriteRenderer spriteRenderer;
    private float originalSpriteSize;
    private AudioSource audioSource;

    private Coroutine routine;
    private bool keepGoing = true;
    private bool closeEnough = false;

 
    void Awake()
    {
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        this.routine = StartCoroutine(this.Pulse());
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    IEnumerator Pulse()
    {
        while (keepGoing)
        {
            while (this.currentRatio != this.growthBound)
            {
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);
                this.spriteRenderer.transform.localScale = Vector3.one * currentRatio;
                yield return new WaitForEndOfFrame();
            }

            while (this.currentRatio != this.shrinkBound)
            {
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);
                this.spriteRenderer.transform.localScale = Vector3.one * currentRatio;

                yield return new WaitForEndOfFrame();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("whers the sound?");
            audioSource.Play();
        }
    }
}
