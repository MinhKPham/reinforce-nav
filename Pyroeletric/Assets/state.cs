using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class state  {

    
    public int x;
    public int y;
    public List<action> action = new List<action>();
    public void Start()
    {
        action a1 = new action();
        a1.act = "up";
        action a2 = new action();
        a2.act = "down";
        action a3 = new action();
        a3.act = "left";
        action a4 = new action();
        a4.act = "right";
        action.Add(a1);
        action.Add(a2);
        action.Add(a3);
        action.Add(a4);

    }
    public float maxq()
    {
        float max=action[0].qvalue ;
        for (int i = 0; i < action.Count; i++)
        {
            if (action[i].qvalue>=max)max=action[i].qvalue;
        }
        return max;
    }
}
