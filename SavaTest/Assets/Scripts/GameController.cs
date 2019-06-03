/**
 * author Ricardo @ www.uareshy.cn QD陈作军
 * emial  1131308355@qq.com
 * 
 * 
 * 
 * */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour
{
    public static GameController _instance;
    //暂停状态
    public bool isPaused = true;
    //menu对象
    public GameObject MenuGO;

    public GameObject[] targetGOs;//游戏怪物目标数组

    private void Awake()
    {
        _instance = this;
        //游戏开始时默认暂停
        Pause();
    }

    private void Update()
    {
        //按esc暂停
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    //游戏菜单呼出
    private void Pause()
    {
        //激活菜单
        isPaused = true;
        MenuGO.SetActive(true);
        Time.timeScale = 0;//停止时间

        Cursor.visible = true;//显示鼠标

    }

    //关闭菜单
    private void UnPause()
    {
        //未暂停菜单
        isPaused = false;
        MenuGO.SetActive(false);
        Time.timeScale = 1;//正常游戏进行

        Cursor.visible = false;//隐藏鼠标

    }

    //保存游戏状态信息
    private Save CreateSaveGO()
    {
        Save save = new Save();
        foreach(GameObject targetGO in targetGOs)
        {
            TargerManager targerManager = targetGO.GetComponent<TargerManager>();
            if(targerManager.activeMonster != null)
            {
                save.livingTargetPositions.Add(targerManager.targetPosition);//保存怪物位置
                int type = targerManager.activeMonster.GetComponent<MonsterManager>().monsterType;
                save.livingMonsterTypes.Add(type);
            }
        }
        //保存分数\射击数
        save.shootNum = UIManager._Instance.shootNum;
        save.score = UIManager._Instance.score;

        return save;
    }
    //读取游戏并设置游戏
    private void SetGame(Save save)
    {
        //清空怪物
        foreach(GameObject targetGO in targetGOs)
        {
            targetGO.GetComponent<TargerManager>().UpdateMonsters();

        }
        //读取位置
        for(int i = 0; i < save.livingTargetPositions.Count; i++)
        {
            int position = save.livingTargetPositions[i];
            int type = save.livingMonsterTypes[i];
            targetGOs[position].GetComponent<TargerManager>().ActiveMonsterByType(type);
        }
        //更新UI
        UIManager._Instance.shootNum = save.shootNum;
        UIManager._Instance.score = save.score;
        UnPause();

    }

    //通过二进制保存
    private void SaveByBin()
    {
        //序列化
        Save save = CreateSaveGO();
        //创建二进制格式化程序
        BinaryFormatter bf = new BinaryFormatter();
        //创建文件流
        FileStream fileStream = File.Create(Application.dataPath + "/SreamingFile" + "/byBin.txt");

        //序列号对象 参数：文件流 序列号对象
        bf.Serialize(fileStream, save);
        //关闭流
        fileStream.Close();
        //保存成功提示
        if (File.Exists(Application.dataPath + "/SreamingFile" + "/byBin.txt"))
        {
            UIManager._Instance.ShowMessage("保存成功!");
        }
        else
        {
            UIManager._Instance.ShowMessage("没有保存成功!");
        }
    }
    private void LoadByBin()
    {
        //反序列化
        if (File.Exists(Application.dataPath + "/SreamingFile" + "/byBin.txt"))
        {


            BinaryFormatter bf = new BinaryFormatter();
            FileStream fileStream = File.Open(Application.dataPath + "/SreamingFile" + "/byBin.txt", FileMode.Open);
            //转化为Save对象
            Save save = (Save)bf.Deserialize(fileStream);
            fileStream.Close();

            SetGame(save);

        }
        else
        {
            UIManager._Instance.ShowMessage("没有存档!");
        }
    }

    //通过Json
    private void SaveByJson()
    {

    }
    private void LoadByJson()
    {

    }

    //继续游戏接口
    public void ContinueGame()
    {
        UnPause();
        UIManager._Instance.ShowMessage("");

    }

    //新游戏接口
    public void Newgame()
    {
        foreach(GameObject targetGo in targetGOs)
        {
            targetGo.GetComponent<TargerManager>().UpdateMonsters();//刷新所有怪物
        }
        //刷新UI
        UIManager._Instance.shootNum = 0;
        UIManager._Instance.score = 0;
        UIManager._Instance.ShowMessage("");

        UnPause();
    }

    //退出游戏
    public void QuitGame()
    {
        Application.Quit();
    }
    //保存游戏
    public void SaveGame()
    {
        SaveByBin();
        
        

    }

    //加载游戏
    public void LoadGame()
    {
        LoadByBin();
        UIManager._Instance.ShowMessage("");

    }
}
