using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour {
    
    static public float explorechance=0.2f;
    public List<state> states;
    int bfrstate;
    int currentstate;
    string actiontaken;
    float reward = 0;
    public float lrate = 0.4f;
    public float drate = 0.5f;
    bool gotthem = false;
    bool hit = false;
    // Use this for initialization
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "pyrowasher") gotthem = true;
       

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name != "pyrowasher") hit = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name != "pyrowasher") hit = true;
    }









    void Start () {
        print("new one  "+explorechance);
        bfrstate = 100000;
        gotthem = false;
        hit = false;
        reward = -1000;
        Application.targetFrameRate = 100;
        transform.position = new Vector3(0, 0, 0);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        states = new List<state>();
        for (int i = 0; i <= 20; i++)
        {
            
            for (int j = 0; j <= 25; j++)
            {
                state s = new state();
                s.x = i;
                s.y = -j;
                s.Start();
                states.Add(s);
                
            }
        }

        if (savedata.got1)
        {
            states = savedata.t;
           
        }
        else{
            savedata.t = states;
            savedata.got1 = true;
        }
        

        for (int i = 0; i < states.Count; i++)
        {
            if (Mathf.Abs((int)transform.position.x) == states[i].x && (int)transform.position.y == states[i].y)
            {
                
                currentstate = i;
                
            }
        }
        float ex = Random.Range(0, 1.0f);
        if (ex >= explorechance)
        {
            float maxq = states[currentstate].action[0].qvalue;
            int maxiact = 0;
            print(states[currentstate].action.Count);
            
            for (int i = 0; i < states[currentstate].action.Count; i++) 
            {
                if (states[currentstate].action[i].qvalue >= maxq)
                {
                    maxq = states[currentstate].action[i].qvalue;
                    maxiact = i;
                }
               
            }
            string take = states[currentstate].action[maxiact].act;
            if (take=="up") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50);
            else if (take == "down") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -50);
            else if (take == "left") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-50, 0);
            else if (take == "right") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 0);
            actiontaken = take;
        }
        else
        {
            int rani = Random.Range(0, 4);
            string take = states[currentstate].action[rani].act;
            if (take == "up") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50);
            else if (take == "down") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -50);
            else if (take == "left") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-50, 0);
            else if (take == "right") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 0);
            actiontaken = take;
        }


    }
	
	// Update is called once per frame
	void Update () {
        reward = -100;
        if (gotthem) reward = 100000;
        if (hit) { reward -= 100; print("hit"); }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        bfrstate = currentstate;
        
        for (int i = 0; i < states.Count; i++)
        {
            if (Mathf.Abs((int)transform.position.x) == states[i].x && (int)transform.position.y == states[i].y)
            {
                
                currentstate = i;

            }
        }
        if (states[bfrstate].x== states[currentstate].x && states[bfrstate].y == states[currentstate].y)
        {
            reward -= 100;
        }
        foreach (action acti in states[bfrstate].action)
        {
            if (acti.act == actiontaken)
            {
                
                
                
                //Q(a,s)=Q(a,s) + learning rate*(reward + discount rate*Q(a with max q, s+1)-Q(a,s)



              
                acti.qvalue = acti.qvalue + lrate * (reward + drate * states[currentstate].maxq() - acti.qvalue);
                if (gotthem) { savedata.t = states; explorechance-=0.001f; SceneManager.LoadScene(0); }
            }
            
        }


        float ex = Random.Range(0, 1.0f);
        if (ex >= explorechance)
        {
            float maxq = states[currentstate].action[0].qvalue;
            int maxiact = 0;
            for (int i = 0; i < states[currentstate].action.Count; i++)
            {
                if (states[currentstate].action[i].qvalue >= maxq)
                {
                    maxq = states[currentstate].action[i].qvalue;
                    maxiact = i;
                }
            }
            string take = states[currentstate].action[maxiact].act;
            if (take == "up") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50);
            else if (take == "down") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -50);
            else if (take == "left") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-50, 0);
            else if (take == "right") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 0);
            actiontaken = take;
            print(maxq);
            
        }
        else
        {
            int rani = Random.Range(0, 4);
            
            string take = states[currentstate].action[rani].act;
            if (take == "up") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 50);
            else if (take == "down") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -50);
            else if (take == "left") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-50, 0);
            else if (take == "right") gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(50, 0);
            actiontaken = take;
        }







    }
}
