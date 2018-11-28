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


    private Coroutine routine;
    private bool keepGoing = true;
    private bool closeEnough = false;
 
    void Awake()
    {
        // Find the text  element we want to use
        this.spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();

        // Then start the routine
        this.routine = StartCoroutine(this.Pulse());
    }
    IEnumerator Pulse()
    {
        // Run this indefinitely
        while (keepGoing)
        {
            // Get bigger for a few seconds
            while (this.currentRatio != this.growthBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, growthBound, approachSpeed);

                // Update our text element
                this.spriteRenderer.transform.localScale = Vector3.one * currentRatio;


                yield return new WaitForEndOfFrame();
            }

            // Shrink for a few seconds
            while (this.currentRatio != this.shrinkBound)
            {
                // Determine the new ratio to use
                currentRatio = Mathf.MoveTowards(currentRatio, shrinkBound, approachSpeed);

                // Update our text element
                this.spriteRenderer.transform.localScale = Vector3.one * currentRatio;


                yield return new WaitForEndOfFrame();
            }
        }
    }
}
