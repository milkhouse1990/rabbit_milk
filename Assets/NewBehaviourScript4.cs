using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NewBehaviourScript4 : MonoBehaviour {
    public Transform bullet;
    private int invincible;
    int id=1;
    int timer=0;
    int skill=0;
    int atk=1;
    private int hp=2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //enemy behavior
        //lei->thinking();
        if (timer == 0)
            switch (id)
            {
                case 1:
                    timer = 3 * 60;
                    skill = 1;
                    break;
                case 10:
                    float[] p = { 25, 25, 25, 25 };
                    switch (choose(p))
                    {
                        case 0:
                            timer = 60;
                            skill = 1;
                            break;
                        case 1:
                            timer = 60;
                            skill = 2;
                            break;
                        case 2:
                            timer = 60;
                            skill = 3;
                            break;
                        case 3:
                            timer = 60;
                            skill = 4;
                            break;
                    }
                    break;
            }
        //enemy_use skill
        if (skill!=0)
        {
            switch (id)
            {
                case 1:
                    if (skill == 1)
                    {
                        Transform t_temp=Instantiate(bullet, transform.position+new Vector3(-1,0,0), Quaternion.identity);
                        t_temp.GetComponent<Rigidbody2D>().velocity = new Vector3(-10, 0, 0);                          
                    }
                    break;
                case 10:
                    switch (skill)
                    {
                        case 1:
                            GetComponent<Rigidbody2D>().velocity = new Vector3(0, -1, 0);
                            break;
                        case 2:
                            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 1, 0);
                            break;
                        case 3:
                            GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 0, 0);
                            break;
                        case 4:
                            GetComponent<Rigidbody2D>().velocity = new Vector3(1, 0, 0);
                            break;
                    }
                    break;
            }
            skill = 0;
        }

            timer--;
            /*if (lei->x<xview || lei->x> xview + xscreen)
			{
				lei = enemy_list.erase(lei);
				continue;
			}*/

			
			
		
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


