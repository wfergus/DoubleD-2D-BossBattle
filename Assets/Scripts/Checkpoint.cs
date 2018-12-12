using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Checkpoint : MonoBehaviour
{

    [SerializeField]
    private float inactivatedRotationSpeed = 100, activatedRotationSpeed = 0, inactiveScalePulseSpeed = 100, activatedScalePulseSpeed = 0;

    [SerializeField]
    private float inactivatedScale = 1, activatedScale = 1.5f;

    [SerializeField]
    private Color inactivatedColor, activatedColor;

    private bool isActivated;
    private SpriteRenderer spriteRenderer;


   
    private float originalSpriteSize;


    private Coroutine routine;
    //private bool keepGoing = true;
    //private bool closeEnough = false;
  
   
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateColor();
    }
    private void Update()
    {
        UpdateRotation();
      
    }

    private void UpdateRotation()
    {
        float rotationSpeed = inactivatedRotationSpeed;
        if (isActivated)
            rotationSpeed = activatedRotationSpeed;

        transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);

    }
    public void UpdateScalePulse()
    {
        float scalePulseSpeed = inactiveScalePulseSpeed;
        if (isActivated)
            scalePulseSpeed = activatedScalePulseSpeed;


    }
    private void UpDateScale()
    {
        float scale = inactivatedScale;
        if (isActivated)
            scale = activatedScale;

        transform.localScale = Vector3.one * scale;
    }
    private void UpdateColor()
    {
        Color color = inactivatedColor;
        if (isActivated)
            color = activatedColor;

        spriteRenderer.color = color;
    }
    public void SetIsActivated(bool value)
    {
        isActivated = value;
        UpDateScale();
        UpdateColor();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isActivated)
        {
            Debug.Log("Player entered Checkpoint.");
            PlayerCharacter player = collision.GetComponent<PlayerCharacter>();
            player.SetCurrentCheckpoint(this);
            player.isDead = false;
        }
    }
}
