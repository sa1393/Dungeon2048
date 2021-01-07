using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill> skillList = new List<Skill>();

    private void Awake()
    {
        //아이디 이름 설명 데미지 사거리 마나 쿨타임 등급 종류
        skillList.Add(new Skill1(0, "프리즈", "적에게 피해를 입힙니다.", 0.7f, 2, 30, 4, 1, 0));        
        skillList.Add(new Skill2(1, "불꽅을 피우리라", "적에게 피해를 입힙니다.", 1.2f, 2, 30, 4, 1, 0));        
        skillList.Add(new Skill3(2, "스파크", "적에게 피해를 입힙니다.", 1f, 2, 30, 4, 1, 0));        
        skillList.Add(new Skill4(3, "속공", "무기 재사용 대기 시간 초기화", 0, 0, 25, 7, 1, 1));
        skillList.Add(new Skill5(4, "근력 보충", "다음 공격에만 공격력 증가", 0, 0, 50, 5, 1, 1));
        //skillList.Add(new Skill6(5, "질주", "다음 이동 때 맵 끝까지 이동", 0, 0, 20, 10, 1, 1));
        //skillList.Add(new Skill7(6, "도움닫기", "이동 시 일정 확률로 한 번 더 이동한다", 0, 0, 0, 2, 1, 2));
        //skillList.Add(new Skill8(7, "흥분", "피격 시 다음 공격에만 치명타 확률 증가", 0, 0, 0, 2, 1, 2));
        skillList.Add(new Skill9(8, "빨리빨리", "매 턴마다 일정 확률로 재사용 대기 시간 1감소", 0, 0, 0, 2, 1, 2));


        //skillList.Add(new Skill(3, "내려 찍기", "적을 내려 찍어 피해를 입히고 기절 시킵니다", 30, 1, 1, 3, 1, true));
        //skillList.Add(new Skill(5, "월섬", "적을 베어 피해를 입힙니다.", 400, 1, 3, 4, 1, true));
        //skillList.Add(new Skill(7, "바위 낙하", "무작위 적에게 바위를 낙하시켜 피해를 입히고 기절 시킵니다.", 4, 1, 3, 2, 2, true));
        //skillList.Add(new Skill(8, "오염", "적을 오염 시켜 피해를 입히고 중독 디버프를 입힙니다. 중독 상태의 적은 매 턴마다 일정 확률로 즉사합니다. 적이 이미 중독 상태라면 중독 중첩 수에 비례하여 즉사 확률이 늘어납니다.", 4, 1, 3, 2, 2, true));
        //skillList.Add(new Skill(9, "재빠른 손놀림", "무기 재사용 대기 시간을 초기화 시킵니다.", 4, 1, 3, 2, 2, true));
        //skillList.Add(new Skill(10, "저주의 손길", "적에게 저주 디버프를 입힙니다. 저주 상태의 적은 합성할 수 없습니다.", 4, 1, 3, 2, 3, true));
        //skillList.Add(new Skill(11, "사슬 결박", "적을 사슬로 결박하여 피해를 입히고 속박 디버프를 입힙니다. 속박 상태의 적은 이동할 수 없습니다.", 40, 2, 3, 3, 2, true));
        skillList.Add(new SkillRock(0, "temp", "", 10, 1, 1, 1, 0, 0));
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
