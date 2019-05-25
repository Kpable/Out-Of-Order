using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 30), .1f).From(Quaternion.Euler(0, 0, -30)).SetLoops(4, LoopType.Yoyo);
        }
    }
}
