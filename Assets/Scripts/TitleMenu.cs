using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    public GameObject titlingMenu;
    public GameObject LevelMenu;

    public GameObject canvas;

    private AssetBundle LoadedAssetBundles;
    private string[] scenePaths;

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

        SceneManager.LoadScene("Level 1 Soundedit", LoadSceneMode.Single);
    }

    public void ToWarehouseLevel(){
        
        SceneManager.LoadScene("Warehouse New Level", LoadSceneMode.Single);
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
