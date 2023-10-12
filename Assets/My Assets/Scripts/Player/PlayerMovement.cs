using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    public LayerMask visibleLayers;
    public LayerMask terrainlayer;
    public LayerMask coinLayer;
    public LayerMask breakableLayer;
    public Limmits lim;
    public SpriteRenderer spriteR;
    public bool canMove = true;
    public float actionDelay = 1f; //Seconds
    public float shakeDurantion = .15f, shakeIntensity = .2f; //Seconds

    float nextTime = 0f;

    public AudioClip hitAudio, jumpAudio;
    public AudioSource audSource;

    [SerializeField]
    GameObject trail;

    void Awake() { instance = this; }

    void Start()
    {
        trail.SetActive(true);
    }

    void Update()
    {
        if (canMove && Input.anyKeyDown)
            Movement();
    }

    void Movement()
    {
        if (!TimerCheck()) return;

        Vector2 axis = GetSides();
        Vector3 oldPos = transform.position;

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, axis, Mathf.Infinity, visibleLayers);
        Debug.DrawLine(transform.position, DevTools.Vec3toVec2(transform.position) + axis * 100, Color.red, .01f);

        foreach (RaycastHit2D h2d in hit)
        {
            if (h2d.collider != null)
            {
                if (DevTools.IsInLayerMask(h2d.collider.gameObject.layer, coinLayer))
                {
                    h2d.collider.GetComponent<CoinItem>().GetCoin();
                    h2d.collider.gameObject.SetActive(false);
                }

                if (DevTools.IsInLayerMask(h2d.collider.gameObject.layer, breakableLayer))
                {

                    transform.position = TransformLimits(h2d.point);
                    if (transform.position != oldPos)
                    {
                        h2d.collider.GetComponent<BreakableTile>().DamageItem();
                        StartCoroutine(MainCamera.instance.cameraShake.Shake(shakeDurantion, shakeIntensity));
                    }

                    break;
                }

                if (DevTools.IsInLayerMask(h2d.collider.gameObject.layer, terrainlayer))
                {
                    transform.position = TransformLimits(h2d.point);
                    break;
                };
            }
        }
    }

    Vector2 GetSides()
    {
        float vecX = 0;
        float vecY = 0;

        //Priority to X axis;
        vecX = Input.GetAxisRaw("Horizontal");
        if (vecX == 0) vecY = Input.GetAxisRaw("Vertical");

        Vector2 vec = new Vector2(vecX, vecY);

        if (vec != Vector2.zero) StartTimer();
        return vec;
    }

    Vector2 TransformLimits(Vector2 finalLocation)
    {

        float selectedLimmitsX = transform.position.x, selectedLimmitsY = transform.position.y;

        float x = transform.position.x - finalLocation.x;
        float y = transform.position.y - finalLocation.y;

        if (Mathf.Abs(x) > 1 || Mathf.Abs(y) > 1)
        {
            audSource.clip = jumpAudio;
            audSource.Play();
        }


        if (Mathf.Abs(x) > Mathf.Abs(y))
        {
            if (x > 0)
            {
                selectedLimmitsX = lim.right.transform.localPosition.x + finalLocation.x;
                spriteR.flipX = true;
            }

            if (x < 0)
            {
                selectedLimmitsX = lim.left.transform.localPosition.x + finalLocation.x;
                spriteR.flipX = false;
            }
        }
        else if (Mathf.Abs(x) < Mathf.Abs(y))
        {
            if (y > 0)
            {
                selectedLimmitsY = lim.top.transform.localPosition.y + finalLocation.y;
                spriteR.flipY = false;
            }

            if (y < 0)
            {
                selectedLimmitsY = lim.bottom.transform.localPosition.y + finalLocation.y;
                spriteR.flipY = true;
            }
        }
        return new Vector2(selectedLimmitsX, selectedLimmitsY);
    }

    [Serializable]
    public struct Limmits
    {
        public GameObject top, bottom, left, right;
    }

    public void StopMovement()
    {
        canMove = false;
    }

    public void RestoreMovement()
    {
        canMove = true;
    }

    void StartTimer()
    {
        nextTime = Time.unscaledTime + actionDelay;
    }

    bool TimerCheck()
    {
        return nextTime < Time.unscaledTime;
    }
}

