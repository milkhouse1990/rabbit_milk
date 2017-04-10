using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy1 : MonoBehaviour {
    public Transform bullet;
    private int invincible;
    int id=1;
    int skill=0;
    int atk=1;
    private int hp=2;
    private int timer=0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //enemy behavior
        //lei->thinking();
        GameObject p1 = GameObject.Find("milk");
        if (p1!=null)
        {
            if (System.Math.Abs(p1.transform.position.x - gameObject.transform.position.x) < 5)
                if (timer==0)
            {
                Transform new_bullet=Instantiate(bullet, transform.position, transform.rotation);
                new_bullet.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-transform.localScale.x-1 * 10, 0);
                new_bullet.gameObject.transform.localScale = -transform.localScale;
                    timer = 60;
            }
                
        }	
				
	}
    void FixedUpdate()
    {
        if (timer>0)
        timer--;
    }

    int choose(float[] probs)
    {
        //将事件元素加入到数组中，如上面有4个元素，分别为50,25,20,5
        float total = 0;
        foreach (float elem in probs)
            total += elem;
        //Random.value方法返回一个0—1的随机数
        float randomPoint = Random.value * total;
        for (int i = 0; i < probs.Length; i++)
        {
            if (randomPoint < probs[i])
                return i;
            else
                randomPoint -= probs[i];
        }
        return probs.Length - 1;
    }
    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "weapon")
        {
            hp -= 1;
            if (hp <= 0)
                Destroy(this.gameObject);
        }
            
    }*/
}


