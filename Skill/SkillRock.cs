using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRock : Skill
{
    public SkillRock(int id, string name, string info, int damage, int cross, int useMp, int coolTime, int rating, int active) : base(id, name, info, damage, cross, useMp, coolTime, rating, active)
    {
        id = -1;
        Sprite = GameManager.instance.skillRock;
    }

    public void use(int goalX, int goalY)
    {
    }

    void Start()
    {

    }

    void Update()
    {

    }
}
