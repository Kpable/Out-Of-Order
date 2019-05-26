using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Shake : MonoBehaviour
{
    public float ShakeTime;
    public float ShakeStrength;
    public int ShakeVibrate;
    public bool Complete;

    public AudioSource audioSrc;
    public AudioClip clip;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();

    }
    // Start is called before the first frame update
    void Start()
    {
        Complete = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ShakeVend();
        }
    }
    public void ShakeVend()
    {
        if (Complete == true)
        {
            Complete = false;

            transform.DOShakeRotation(ShakeTime, new Vector3(0, 0, ShakeStrength), ShakeVibrate, 90, true)
                .OnComplete(() => { Complete = !Complete; });

            if (clip!= null)
                audioSrc.PlayOneShot(clip);
        }
    }

}
