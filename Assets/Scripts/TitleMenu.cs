using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    public GameObject titlingMenu;
    public GameObject LevelMenu;

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        titlingMenu.SetActive(true);
        LevelMenu.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToHouseLevel(){

        //SceneManager.LoadScene("TempHouseSceneName");
    }

    public void ToWarehouseLevel(){
        
        //SceneManager.LoadScene("TempWarehouseSceneName");
    }

    public void ToLevelSelect(){
        titlingMenu.SetActive(false);
        LevelMenu.SetActive(true);
    }

    public void ToTitleScreen(){
        LevelMenu.SetActive(false);
        titlingMenu.SetActive(true);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
