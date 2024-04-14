using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    public GameObject titlingMenu;
    public GameObject LevelMenu;
    public GameObject CreditMenu;

    public GameObject canvas;

    private AssetBundle LoadedAssetBundles;
    
    [SerializeField]
    private string houseLevel;

    [SerializeField]
    private string warehouseLevel;

    // Start is called before the first frame update
    void Start()
    {
        titlingMenu.SetActive(true);
        LevelMenu.SetActive(false);
        CreditMenu.SetActive(false);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToHouseLevel(){

        SceneManager.LoadScene(houseLevel, LoadSceneMode.Single);
    }

    public void ToWarehouseLevel(){
        
        SceneManager.LoadScene(warehouseLevel, LoadSceneMode.Single);
    }

    public void ToLevelSelect(){
        titlingMenu.SetActive(false);
        CreditMenu.SetActive(false);
        LevelMenu.SetActive(true);
    }

    public void ToTitleScreen(){
        LevelMenu.SetActive(false);
        CreditMenu.SetActive(false);
        titlingMenu.SetActive(true);
    }

    public void ToCredits(){
        LevelMenu.SetActive(false);
        CreditMenu.SetActive(true);
        titlingMenu.SetActive(false);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
