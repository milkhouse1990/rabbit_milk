using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets._2D {
    public class AvgEngine : MonoBehaviour {

        public Texture2D icon;
        public Texture2D frame;
        public Texture2D np;

        private string[] text;
        private string dialogue_next = "";
        //ifstream file;
        private string[] commands;
        private int i = 0;
        private string speaker = "";
        private string words = "";
        private bool pause = false;
        private bool wait = false;
        private int alarm;

        private const int FPS = 60;
        private const int BYTEPERLINE = 20;

        //for debug
        private string errmsg = "";
        private bool err = false;
        // Use this for initialization
        void Start() {
            System.Text.Encoding.GetEncoding("gb2312");
        }

        // Update is called once per frame
        void Update() {

            /*if keyboard_check_pressed(global.my_START)
            {
            file_text_close(file);
            file = -1;
            avgend = true;
            }

            if pause
            {
            if keyboard_check_pressed(global.my_A)
            {
            file_text_readln(file);
            pause = false;
            err = false;
            }
            }
            else if wait
            {
            }
            else*/
            if (!pause && !wait)
            {
                //command load
                if (i < commands.Length)
                {
                    //command analysis
                    //int i = 0;
                    string[] para = commands[i].Split(' ');
                    //command understanding
                    //pause time
                    if (para[0] == "pause")
                    {
                        wait = true;
                        int temp;
                        int.TryParse(para[1], out temp);
                        alarm = temp * FPS;
                    }
                    //say charaid text
                    else if (para[0] == "say")
                    {
                        pause = true;
                        speaker = para[1];
                        words = para[2];
                        for (int j = 3; j < para.Length; j++)
                        {
                            words += " ";
                            words += para[j];
                        }

                        //for (int j = 0; j < words.Length / BYTEPERLINE; j++)
                        //    text[j] = words.Substring(j * BYTEPERLINE, BYTEPERLINE);
                    }
                    //charaset charaid x y
                    else
                    {
                        words = "";
                        if (para[0] == "charaset")
                        {

                            //charaid = real(para[1]);
                            //charax = real(para[2]);
                            //charay = real(para[3]);
                            //time=para[4];
                            //instance_create(charax, charay, chaid2obj[charaid]);

                        }
                        //charaunset charaid
                        else if (para[0] == "charaunset")
                        {
                            //wait=true;
                            //time=para[2];
                            //with(chaid2obj[real(para[1])])instance_destroy();
                            //alarm[0]=time*room_speed;
                            //file_text_readln(file);
                        }
                        //charascale charaid xscale yscale
                        else if (para[0] == "charascale")
                        {
                            //charaid = real(para[1]);
                            //charax = real(para[2]);
                            //charay = real(para[3]);
                            //chaid2obj[charaid].image_xscale = charax;
                            //chaid2obj[charaid].image_yscale = charay;

                            //file_text_readln(file);
                        }
                        //charaanime charaid index
                        else if (para[0] == "charaanime")
                        {
                            //chaid = real(para[1]);
                            //chaid2obj[chaid].image_index=chaidind2spr[chaid,real(para[2])];
                            //file_text_readln(file);
                        }
                        //charamove charaid vx vy
                        else if (para[0] == "charamove")
                        {
                            //chaid = real(para[1]);
                            //if instance_exists(chaid2obj[chaid])
                            //{
                            //	chaid2obj[chaid].hspeed = real(para[2]);
                            //	chaid2obj[chaid].vspeed = real(para[3]);
                            //	file_text_readln(file);
                            //}
                            //else
                            //{
                            //	err = true;
                            //	errmsg = "can't find object " + para[1];
                            //	pause = true;
                            //}
                        }
                        //error
                        else
                        {
                            errmsg = "can't understand command: " + para[0];
                            err = true;
                            wait = true;
                        }
                    }
                }
                else
                {
                    GetComponent<Platformer2DUserControl>().enabled = true;
                    GetComponent<AvgEngineInput>().enabled = false;
                    enabled = false;
                }
            }
        }

        void OnGUI()
        {
            int pos;
            if (err)
            {
                GUI.Label(new Rect(100, 0, 640, 320), errmsg);
            }
            else if (pause)
            {
                //UI
                pos = 30;

                /*
                //milk
                if (speaker = "1")
                {
                //cos
                draw_sprite(spr_avatar_milk, global.necklace, view_xview + 5, view_yview + 5)
                //face
                if health > 8
                {
                draw_sprite(spr_avatar_milkf, 0, view_xview + 5, view_yview + 5);
                }
                else
                {
                draw_sprite(spr_avatar_milkf, 2, view_xview + 5, view_yview + 5);
                }
                image_speed = 0;
                }
                //chara

                else
                {
                draw_sprite(spr_avatar, real(speaker) - 2, view_xview + 635, view_yview + 5)
                }

                //dialogue
                draw_sprite(spr_dialogue, 0, view_xview + pos, view_yview + 5)
                draw_sprite_ext(spr_nextpage, 0, view_xview + 450 + pos, view_yview + 70, 1, 1, 1, c_white, al)

                */


                //TALK
                //draw_text(0, 0, speaker);
                //int i = 0;

                /*if (words != "")
                {

                words = "";
                }*/
                if (words != "")
                {
                    if (speaker == "1")
                        GUI.Label(new Rect(0, 0, 64, 64), icon);
                    GUI.Label(new Rect(32, 32, 64, 64), frame);
                    GUI.Label(new Rect(640, 32, 64, 64), np);
                    GUI.Label(new Rect(15 + pos, 15, 640, 320), words);
                }

            }

            //else if para[0] == "charaset"
            {
            }

        }

        public void Open(string title)
        {
            string path = "Line\\" + title + ".txt";
            commands = File.ReadAllLines(path,System.Text.Encoding.Default);
            i = 0;
        }

        public void NextPage()
        {
            if (pause)
            {
                pause = false;
                i++;
            }
        }

    }
}
