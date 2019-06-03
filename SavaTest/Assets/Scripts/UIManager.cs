using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager _Instance;//单例
    //ui组件
    public Text shootNumText;
    public Text scoreText;
    //获取音乐开关
    public Toggle musicToggle;
    //背景音乐
    public AudioSource musicAudio;
    //private bool musicOn = true;

    public int shootNum = 0;
    public int score = 0;

    //提示信息
    public Text messageText;
    private void Awake()
    {
        _Instance = this;

        //判断背景音乐存储状态
        if (PlayerPrefs.HasKey("musicOn"))
        {
            if (PlayerPrefs.GetInt("musicOn") == 0)
            {
                musicToggle.isOn = false;
                musicAudio.enabled = false;

            }
            else
            {
                musicToggle.isOn = true;
                musicAudio.enabled = true;

            }
        }
        else
        {
            musicToggle.isOn = true;
            musicAudio.enabled = true;


        }
    }
    private void Update()
    {
        //更新UI
        shootNumText.text = shootNum.ToString();
        scoreText.text = score.ToString();

        //监控开关
        //MusicSwitch();
    }

    //背景音乐开关
    public void MusicSwitch()
    {
        if(musicToggle.isOn == false)
        {
            //musicOn = false;
            musicAudio.enabled = false;
            PlayerPrefs.SetInt("musicOn", 0);//保存状态
        }
        else
        {
            //musicOn = true;
            musicAudio.enabled = true;
            PlayerPrefs.SetInt("musicOn", 1);

        }
        PlayerPrefs.Save();//保存
    }



    //增加射击数
    public void AddShootNum()
    {
        shootNum += 1;
    }

    //增加分数
    public void AddScore()
    {
        score += 1;
    }

    public void ShowMessage(string str)
    {
        messageText.text = str;
    }

}
