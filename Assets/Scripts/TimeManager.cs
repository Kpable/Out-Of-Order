using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public bool pause;

    private float timercalc;
    private int GameTimeDelta;
    public int scale;
    private int MissionTimer;

    public int MissionCount;
    public int[] MissionTimes;

    // Start is called before the first frame update
    void Start()
    {
        MissionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timercalc += Time.deltaTime * scale;

        if (timercalc > 1)
        {
            GameTimeDelta = Mathf.RoundToInt(timercalc);
            Countdown(GameTimeDelta);
            timercalc = 0;
            GameTimeDelta = 0;
        }
    }

    private void Countdown(int GameTimeDelta)
    {
        if (pause != true)
        {
            MissionTimer = MissionTimer - GameTimeDelta;
        }
        if( MissionTimer < 0)
        {
            //Mission Fail
        }
    }

    public void SetMissionTimer(int MissionCount)
    {
        MissionCount = MissionCount;
        MissionTimer = MissionTimes[MissionCount];
    }

}
