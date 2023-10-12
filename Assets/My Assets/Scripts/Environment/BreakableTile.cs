using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableTile : MonoBehaviour
{
    public GameObject spawnableAudioSource, breakPSAux;
    [SerializeField] AudioClip hitSound;
    [SerializeField]Animator spriteAnim;

    [SerializeField] int maxLife = 3, currentLife = 0;
    private void Start()
    {
        GameManager.instance.AddBreakableItem();
        currentLife = 3;    
    }

    public void BreakItem()
    {
        GameManager.instance.AddBrokenItem();
        GameManager.instance.RemoveBreakableItem();

        GameObject pps = Instantiate(breakPSAux, gameObject.transform.position, Quaternion.identity);
        Destroy(pps, 1f);

        this.gameObject.SetActive(false);
    }

    public void DamageItem()
    {
        GameObject _asAux = Instantiate(spawnableAudioSource, gameObject.transform.position, Quaternion.identity);
        _asAux.GetComponent<AudioSource>().clip = hitSound;
        _asAux.GetComponent<AudioSource>().Play();
        Destroy(_asAux, 2f);

        spriteAnim.SetTrigger("Hit");

        currentLife--;
        if (currentLife <= 0)
        {
            BreakItem();
        }
    }
}
