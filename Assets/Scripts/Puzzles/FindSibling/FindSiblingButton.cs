using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindSiblingButton : MonoBehaviour
{
    public string type;
    private GameObject controller;
    private GameObject puzzle_script;
    private int need;
    private GameObject sibling;
    private float timer=0.75f;


    private void Start()
    {
        puzzle_script = GameObject.Find("FindSibling Script");
        controller = GameObject.Find("Interface Main");
        PlayerPrefs.SetString("sibling", "x");
        PlayerPrefs.SetInt("sibling_pairs", 0);
    }
    public void OnClickSibling()
    {
        if(PlayerPrefs.GetInt("need_findsibling")==0)
        {
            if(PlayerPrefs.GetString("sibling") == "x")
            {
                PlayerPrefs.SetString("sibling", type);
                PlayerPrefs.SetString("sibling_name", gameObject.name);
                gameObject.transform.position = new Vector3(transform.position.x - 2000, transform.position.y, transform.position.z);
                puzzle_script.GetComponent<ResetGameFindSibling>().PlaySound();
            }
            else
            {
                sibling = GameObject.Find(PlayerPrefs.GetString("sibling_name"));

                if (PlayerPrefs.GetString("sibling") == type)
                {
                    sibling.transform.position = new Vector3(sibling.transform.position.x + 2000, sibling.transform.position.y, sibling.transform.position.z);
                    puzzle_script.GetComponent<ResetGameFindSibling>().PlaySound();
                    gameObject.SetActive(false);
                    sibling.SetActive(false);
                    PlayerPrefs.SetInt("sibling_pairs", PlayerPrefs.GetInt("sibling_pairs") + 1);
                    PlayerPrefs.SetString("sibling", "x");
                    PlayerPrefs.SetString("sibling_name", "");
                    if (PlayerPrefs.GetInt("sibling_pairs") == 12)
                    {
                        controller.GetComponent<StartGame>().AfterGameFindSibling();
                    }
                }
                else
                {
                    gameObject.transform.position = new Vector3(transform.position.x - 2000, transform.position.y, transform.position.z);
                    puzzle_script.GetComponent<ResetGameFindSibling>().PlaySound();
                    PlayerPrefs.SetInt("need_findsibling", 1);
                    need = 1;
                    PlayerPrefs.SetString("sibling", "x");
                    PlayerPrefs.SetString("sibling_name", "");
                }
            }
        }
        
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt("need_findsibling")==1 && need==1)
        {
            timer -= Time.deltaTime;
            if(timer<=0)
            {
                timer = 0.75f;
                

                gameObject.transform.position = new Vector3(transform.position.x + 2000, transform.position.y, transform.position.z);
                sibling.transform.position = new Vector3(sibling.transform.position.x + 2000, sibling.transform.position.y, sibling.transform.position.z);
                PlayerPrefs.SetInt("need_findsibling", 0);
                puzzle_script.GetComponent<ResetGameFindSibling>().PlaySound();
                need = 0;
            }
        }
    }
}
