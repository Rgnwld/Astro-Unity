using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteBG : MonoBehaviour
{
    Renderer rend;
    [SerializeField]Vector2 velocity;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MaterialUpdate();
    }

    void MaterialUpdate()
    {
        rend.material.mainTextureOffset = new Vector2(rend.material.mainTextureOffset.x + velocity.x * Time.deltaTime, rend.material.mainTextureOffset.y + velocity.y  * Time.deltaTime);
    }

    public void MaterialColorUpdate(Color col)
    {
        rend.material.color = col;
    }

    public void ResetColor()
    {
        rend.material.color = Color.white;
    }
}
