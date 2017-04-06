using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    bool act;
    int costume;
    bool change;
    bool left;
    int x, y, vx, vy;
    int motion;
    int speed;
    int sizeh, sizev;
    private bool trans_finish;
    private int invincible;
	const KeyCode BUTTON_UP = KeyCode.W;
	KeyCode BUTTON_DOWN = KeyCode.S;
	KeyCode BUTTON_LEFT = KeyCode.A;
	KeyCode BUTTON_RIGHT = KeyCode.D;
	KeyCode BUTTON_A = KeyCode.L;
	KeyCode BUTTON_B = KeyCode.K;
	KeyCode BUTTON_X = KeyCode.O;
	KeyCode BUTTON_Y = KeyCode.J;
	KeyCode BUTTON_L = KeyCode.U;
	KeyCode BUTTON_R = KeyCode.I;
	KeyCode BUTTON_SELECT = KeyCode.Alpha1;
	KeyCode BUTTON_START = KeyCode.Alpha2;

    // Use this for initialization
    void Start () {
        act = true;
        costume = 0;
		motion = 0;
		change = false;
		speed = 5;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position += new Vector3 (1, 0, 0);
        //-------------------------------------------------INPUT
        //press control 
        if (act)
        {
           //act mode
              if (Input.GetKeyDown(BUTTON_X))
              {
                  if (costume != 0)
                      costume = 0;
              }
              if (Input.GetKeyDown(BUTTON_Y))
              {
                  if (costume==0)
                  {
                      if (!change)
                      {
                          /*object new_bullet(1, x, y, 10 - 20 * left, 0, "bullet", 64, 64, 0);
                            new_bullet.left = left;
                            milk_listobj.push_back(new_bullet);*/
                        }
                    }
				}
				if (Input.GetKeyDown(BUTTON_B))
				{
					if (!change)
					{
						/*if (WallCheck(x - tile_unit / 2, y + tile_unit) || WallCheck(x + tile_unit / 2, y + tile_unit))
						{
						    vy = -20;
						}*/
					}
				}
				if (Input.GetKeyDown(BUTTON_A))
				{
					if (!change)
					{
						if (motion == 2)
							{
								costume = 5;
								speed = 20;
								motion = 0;
								//gimmick clothes(0, x, y, "rabbit", tile_unit, tile_unit, 0);
                                //gimmick_list.push_back(clothes);
							}
						else if (costume == 5)
						{
							/*//clothes check
							for (lgi = gimmick_list.begin(); lgi != gimmick_list.end();)
							{
								if (lgi->id == 0)
								{
									if (damage_check((object)* lgi))
									{
										if (WallCheck(x - sizeh/2+1, y - sizev) || WallCheck(x + sizeh/2, y - sizev))
											break;
										else
										{
										    costume = 0;
											speed = 10;
											motion = 0;
											sizev = 160;
											lgi = gimmick_list.erase(lgi);
										}
										break;
									}
									break;
								}
								else
									lgi++;
							}
*/

						}

					}
				}
				if (Input.GetKeyDown(BUTTON_UP))
				{
					if (!change)
					{
						 /*//npc check
						for (lni = npc_listnpc.begin(); lni != npc_listnpc.end(); ++lni)
						{
							if (damage_check(* lni))
							{
								act = false;
								avg.dialogue_next = lni->txt;
								break;
							}
						}*/
					}
					else
					{
						costume = 1;
						trans_finish = true;
					}
				}
				if (Input.GetKeyDown(BUTTON_DOWN))
				{
					if (change)
					{
						//costume = 2;
						//trans_finish = true;
					}
					else
					{
						if (costume != 5)
						{
							motion = 2;
							name = "rabbit";
							sizev = 64;
						}
                    }
				}
				if (Input.GetKeyDown(BUTTON_LEFT))
				{
					if (change)
					{
						//costume = 3;
						//trans_finish = true;
					}
				}
				if (Input.GetKeyDown(BUTTON_RIGHT))
				{
					if (change)
					{
						//costume = 4;
						//trans_finish = true;
					}
				}
				if (Input.GetKeyDown(BUTTON_DOWN))
				{
					if (change)
					{

					}
					else
					{
						if (costume != 5)
						{
							motion = 0;
						    name = "rabbit";
						    sizev = 160;
						}
					}
				}			
		}
		else
		{
			if (Input.GetKeyDown(BUTTON_A))
            {
				//avg.pause = false;
				//avg.err = false;
				//avg.nextpage();
			}
		}
	//pressing control
	if (act)
	{
		//act mode
		//transform waiting check
		if (Input.GetKey(BUTTON_X))
		{
			if (!trans_finish)
				change = true;
			else
				change = false;
		}
		else
		{
			change = false;
			trans_finish = false;
		}
		if (change)
		{
			//transform control
		}
		else
		{
			//action control
			if (Input.GetKey(BUTTON_LEFT))
			{
				if (motion != 2)
				{
					left = true;
					/*while (WallCheck(x - speed - sizeh / 2, y-sizev+1) || WallCheck(x - speed - sizeh / 2, y))
						x++;*/
					
					transform.position += new Vector3(-speed*Time.deltaTime, 0, 0);

					/*for (lgi = gimmick_list.begin(); lgi != gimmick_list.end(); lgi++)
					{
						if (lgi->id>0 && lgi->id< 127)
							if (damage_check(* lgi))
								x = lgi->x + lgi->sizeh / 2 + sizeh / 2;

					}
					//name = "rabbit2";*/
				}
				
			}
			if (Input.GetKey(BUTTON_RIGHT))
			{
				if (motion != 2)
				{
					left = false;
					
						/*
					while (WallCheck(x + speed + sizeh / 2, y - sizev+1) || WallCheck(x + speed + sizeh / 2, y))
						x--;*/
						transform.position += new Vector3(speed*Time.deltaTime, 0, 0);

					

					/*for (lgi = gimmick_list.begin(); lgi != gimmick_list.end(); lgi++)
					{
						if (lgi->id>0 && lgi->id< 127)
							if (damage_check(* lgi))
								x = lgi->x - lgi->sizeh / 2 - sizeh / 2;

					}
*/
					//name = "rabbit2";
				}
				
			}
			else
				name = "rabbit";
		}
		if (!change)
		{

			//-------------------------------------------------milk PHASE
			//milk status update
			if (invincible > 0)
				invincible--;
			//milk move
			//milk fall
			//transform.position+=new Vector3(0,vy,0);
				//if (Physics2D.

			/*//地面检测
			if (WallCheck(x - tile_unit / 2, y + 1) || WallCheck(x + tile_unit / 2, y + 1))
			{
				y = (y+1) / tile_unit* tile_unit-1;
				vy = 0;
			}

			else
				vy += 1;
			for (lgi = gimmick_list.begin(); lgi != gimmick_list.end(); lgi++)
			{
				if (lgi->id>0 && lgi->id< 128)
				{
					y++;
					if (lgi->damage_check(milk))
					{
						y = lgi->y - lgi->sizev;
						vy = 0;
					}
					else
						y--;
					lgi->effect(&milk);
				}
					
						
			}
			
			//milk bullet phase
			for (loi = milk_listobj.begin(); loi != milk_listobj.end();)
			{
				//milk bullet behavior
				loi->motion();
				if (loi->x<xview || loi->x> xview + xscreen)
				{
					loi = milk_listobj.erase(loi);
					continue;
				}

				//enemy_damage_check
				for (lei = enemy_list.begin(); lei != enemy_list.end();)
				{
					if (lei->id< 100)
						if (lei->damage_check(*loi))
						{
							lei->nHp -= 1;
							if (lei->nHp == 0)
								lei = enemy_list.erase(lei);
							else
								lei++;
						}
						else
							lei++;
					else
						lei++;
					
				}

				loi++;

			}
			
			//event check
			for (loi = event_listobj.begin(); loi != event_listobj.end(); )
			{
				if (damage_check(* loi))
				{
					loi = event_listobj.erase(loi);
					if (x > 1280)
                        LoadMap(3, hdc, bufdc, mapdc);
					else
					{
						act = false;
						avg.boot("1");
					}
						
						//LoadMap(2, hdc, bufdc, mapdc);
					break;
				}
				else
					loi++;
			}


			//-----------------------------------------------ENEMY PHASE
			for (lei = enemy_list.begin(); lei != enemy_list.end(); )
			{
				//enemy behavior
				lei->thinking();
lei->UseSkill(&enemy_list);
lei->update();
lei->motion();
				if (lei->x<xview || lei->x> xview + xscreen)
				{
					lei = enemy_list.erase(lei);
					continue;
				}

				//milk_damage_check
				if (damage_check((object)* lei))
				{
					nHp -= lei->atk;
					if (nHp <= 0)
					{
						nHp = 0;
						die();
                        LoadMap(room_no, hdc, bufdc, mapdc);
						break;
					}
					x -= 64 * (lei->x - x >= 0 ? 1 : -1);
					invincible = 60;
				}
				lei++;
			}
			//----------------------------------------------GIMMICK PHASE
			for (lgi = gimmick_list.begin(); lgi != gimmick_list.end();)
			{
				if (lgi->id< 127)
				{
					//lgi->effect(&milk);
					//lgi->motion();
				}		
				if (lgi->id>127)
					if (damage_check((object)* lgi))
					{
						lgi->effect(&milk);
lgi = gimmick_list.erase(lgi);
						continue;
					}
				if (lgi->x< 0)
				{
					lgi = gimmick_list.erase(lgi);
				}
				else
					lgi++;
			}


		}
		
			
				
				
	}
	else
	{
		act = avg.step();
		//act = avg.display(mdc);	
	}
	
	//---------------------------------------------------DRAW
	//camera
	if (roll == 0)
	{
		if (x - xview > xscreen / 2 && cols* tile_unit - x > xscreen / 2)
			xview = x - xscreen / 2;
		if (x - xview<xscreen / 2 && x> xscreen / 2)
			xview = x - xscreen / 2;
		//bounder check
		while (x< 0)
            x++;
	}
	
	if (y > yscreen)
	{
		die();
        LoadMap(room_no, hdc, bufdc, mapdc);
	}
	if (roll==1)
		if (x<xview)
		{
			die();
            LoadMap(room_no, hdc, bufdc, mapdc);
		}

	CImage image;
	//npc draw
	for (lni = npc_listnpc.begin(); lni != npc_listnpc.end(); ++lni)
	{
		lni->draw(mdc, xview, 0);

		if (damage_check(* lni))
		{
			if (act)
			{
				image.Load("Res\\up.png");
				image.Draw(mdc, x - tile_unit*1.5, y - tile_unit, tile_unit, tile_unit, 0, 0, tile_unit, tile_unit);
				image.Destroy();
			}
		}

	}
	//enemy draw
	for (lei = enemy_list.begin(); lei != enemy_list.end(); ++lei)
	{
		lei->draw(mdc, xview, 0);
	}
	//environment draw
	for (lgi = gimmick_list.begin(); lgi != gimmick_list.end(); ++lgi)
	{
		lgi->draw(mdc, xview, 0);

		if (lgi->id==0)
			if (act)
			{
				if (damage_check((object)* lgi))
				{
					image.Load("Res\\up.png");
					image.Draw(mdc, x - tile_unit*1.5, y - tile_unit, tile_unit, tile_unit, 0, 0, tile_unit, tile_unit);
					image.Destroy();
				}
				
			}
	}
	//milk draw
	
	image.Load("Res\\rabbit.png");

	if (!(invincible % 2))
		draw(mdc, xview, 0);
	image.Destroy();
	
	//milk bullet
	for (loi = milk_listobj.begin(); loi != milk_listobj.end(); ++loi)
	{
		loi->draw(mdc, xview, 0);
	}
	
		
	//UI
	//CImage image;
	image.Load("Res\\face.png");
	image.Draw(mdc, 0, 0, tile_unit*2,tile_unit*2,0,0,tile_unit*2,tile_unit*2);
	image.Destroy();
	image.Load("Res\\heart.png");
	int i;
	for (i = 0; i<nHp/4;i++)
		image.Draw(mdc, (i+2)* tile_unit, 0, tile_unit, tile_unit, tile_unit*4, 0, tile_unit, tile_unit);
	if (i<mHp/4)
        image.Draw(mdc, (i + 2)* tile_unit, 0, tile_unit, tile_unit, tile_unit*(nHp % 4), 0, tile_unit, tile_unit);
	i++;
	for (; i<mHp/4; i++)
		image.Draw(mdc, (i+2)* tile_unit, 0, tile_unit, tile_unit, 0, 0, tile_unit, tile_unit);
	image.Destroy();


    sprintf_s(str, "%d / %d", nHp, mHp);
    TextOut(mdc, 0, 0, str, strlen(str));
	//boss hp
	if (boss_flag)
	{
		image.Load("Res\\face.png");
		image.Draw(mdc, 1100, 500, tile_unit* 2, tile_unit* 2, 0, 0, tile_unit* 2, tile_unit* 2);
		image.Destroy();
        sprintf_s(str, "%d / %d", enemy_list.begin()->nHp, enemy_list.begin()->mHp);
        TextOut(mdc, 1100, 400, str, strlen(str));
	}
	//dialogue draw
	if (!act)
	{
		avg.draw(mdc, xview, 0);
	}
    //map
    //SelectObject(mapdc, fullmap);
    BitBlt(mdc, 0, 0, xscreen, yscreen, mapdc, xview, 0, SRCAND);
    //display
    //SelectObject(mdc, bmp);
    BitBlt(hdc, 0, 0, xscreen, yscreen, mdc, 0, 0, SRCCOPY);


tPre = GetTickCount();

pNum++;
	if (pNum == 8)
		pNum = 0;	
	}
}

bool check_in(int x, int y, const object enemy)
{
    if (x >= enemy.x - enemy.sizeh / 2 + 1)
        if (x <= enemy.x + enemy.sizeh / 2)
            if (y >= enemy.y - enemy.sizev + 1)
                if (y <= enemy.y)
                    return true;
    return false;
}
bool damage_check(const object enemy)
{
    //milk:def enemy:atk
    if (invincible > 0)
        return false;
    //check four points
    if ((check_in(x - sizeh / 2 + 1, y - sizev + 1, enemy)) || (check_in(x + sizeh / 2, y - sizev + 1, enemy)) || (check_in(x - sizeh / 2 + 1, y, enemy)) || (check_in(x + sizeh / 2, y, enemy)))
        return true;
    return false;
}*/
			}
		}
	}

	void onCollisionEnter(Collision2D collision)
	{
		transform.position = new Vector3 (0, 0, 0);
	}
	void FixedUpdate()
	{
		Physics2D.velocityThreshold = 0;
	}
}