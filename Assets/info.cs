using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class info : MonoBehaviour
{
    public int weapon;
    if(!플레이어의 무기 소유 여부(리스트로 weapon 순서에 true false로 )){
        string name = "???";
        string explain = "아직 발견하지 못했습니다.";
    }
    else{
        switch(weapon)
        {
            case 1:
                string name = "채찍(Whip)";
                string explain = "좌우로 적을 관통해 공격합니다.";
                break;
            
            case 2:
                string name = "피눈물(Blood Tear)";
                string explain = "좌우로 적을 관통해 공격합니다.";
                break;

            case 3:
                string name = "마법 지팡이(Magic Wand)";
                string explain = "좌우로 적을 관통해 공격합니다.";
                break;

            case 4:
                string name = "채찍(Whip)";
                string explain = "좌우로 적을 관통해 공격합니다.";
                break;

            case 5:
                string name = "채찍(Whip)";
                string explain = "좌우로 적을 관통해 공격합니다.";
                break;

            case 1:
                string name = "채찍(Whip)";
                string explain = "좌우로 적을 관통해 공격합니다.";
                break;

            case 1:
                string name = "채찍(Whip)";
                string explain = "좌우로 적을 관통해 공격합니다.";
                break;
                
        }
    }
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
