using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public bool pause;
    public GameObject Player;

    private float timercalc;
    private int GameTimeDelta;
    public int scale;
    public int MissionTimer;

    public int MissionCount;
    public int[] MissionTimes;
    public GameObject[] MissionDestinations;

    public GameObject VendingMachine;

    [SerializeField] private int ShakeBase;
    [SerializeField] private int ShakeHigh;
    [SerializeField] private int ShakeLow;
    public int ShakeDeviation;

    public GameObject UIParent;


    // Start is called before the first frame update
    void Start()
    {
        MissionCount = 0;
        SetMissionTimer(MissionCount);
        VendingMachine = this.gameObject;
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

        if (Player.GetComponent<Collider2D>().IsTouching(MissionDestinations[MissionCount].GetComponent<Collider2D>()) && Player.GetComponent<PlayerCollisions>().info.Below == true)
        {
            MissionCount++;
            SetMissionTimer(MissionCount);
        }
    }

    private void Countdown(int GameTimeDelta)
    {
        if (pause != true)
        {
            MissionTimer = MissionTimer - GameTimeDelta;

            if (ShakeHigh > MissionTimer)
            {
                VendingMachine.GetComponent<Shake>().ShakeVend();
                ShakeHigh = 0;
            }
            if (ShakeBase > MissionTimer)
            {
                VendingMachine.GetComponent<Shake>().ShakeVend();
                ShakeBase = 0;
            }
            if (ShakeHigh > MissionTimer)
            {
                VendingMachine.GetComponent<Shake>().ShakeVend();
                ShakeLow = 0;
            }
        }
        if (MissionTimer < 0)
        {
            //Mission Fail
        }
    }

    public void SetMissionTimer(int MissionCount)
    {
        MissionTimer = MissionTimes[MissionCount];
        ShakeBase = Mathf.RoundToInt(Random.Range(ShakeDeviation + 1, MissionTimer - 1));
        ShakeHigh = ShakeBase + ShakeDeviation;
        ShakeLow = ShakeBase - ShakeDeviation;
    }


}
