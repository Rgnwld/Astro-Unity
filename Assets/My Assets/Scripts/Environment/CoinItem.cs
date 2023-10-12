using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinItem : MonoBehaviour
{

    [SerializeField]
    GameObject pickupPSAux, spawnableAudioSource;

    [SerializeField]
    AudioClip pickupCoinAudio;

    void Start()
    {
        GameManager.instance.AddTotalCoin();
    }

    public void GetCoin()
    {
        GameManager.instance.AddCoin();
        GameObject pps = Instantiate(pickupPSAux, gameObject.transform.position, Quaternion.identity);
        pps.GetComponent<ParticleSystem>().Play();
        Destroy(pps, 1f);

        GameObject _asAux = Instantiate(spawnableAudioSource, gameObject.transform.position, Quaternion.identity);
        _asAux.GetComponent<AudioSource>().clip = pickupCoinAudio;
        _asAux.GetComponent<AudioSource>().Play();
        Destroy(_asAux, 2f);

        this.gameObject.SetActive(false);
    }
}
