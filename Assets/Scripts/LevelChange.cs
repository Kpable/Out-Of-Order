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
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Levels[LevelNum].SetActive(false);
            LevelNum++;
            if (LevelNum < Levels.Length)
            {
                Levels[LevelNum].SetActive(true);
            }
            else
            {
                LevelNum = 0;
                Levels[LevelNum].SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.Comma))
        {
            Levels[LevelNum].SetActive(false);
            LevelNum--;

            if (LevelNum < 0)
            {
                LevelNum = 0;
                Levels[LevelNum].SetActive(true);
            }
            else
            {
                LevelNum = 0;

                Levels[LevelNum].SetActive(true);
            }
        }
    }

    public void LevelProgress()
    {
        Levels[LevelNum].SetActive(false);
        LevelNum++;
        Levels[LevelNum].SetActive(true);

    }


}
