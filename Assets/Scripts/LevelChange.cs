using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChange : MonoBehaviour
{
    public GameObject[] Levels;

    [SerializeField] private GameObject ActiveLevel;
    public int LevelNum;

    // Start is called before the first frame update
    void Start()
    {
        LevelNum = 0;
        Levels[LevelNum].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelProgress()
    {
        Levels[LevelNum].SetActive(false);
        LevelNum++;
        Levels[LevelNum].SetActive(true);
        
    }
}
