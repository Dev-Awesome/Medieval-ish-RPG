using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialsLoader : MonoBehaviour
{
    public static EssentialsLoader instance;
    public GameObject gameManagerPrefab;
    public GameObject playerPrefab;
    public GameObject uiScreenPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (PlayerController.instance == null)
        {
            PlayerController.instance = Instantiate(playerPrefab).GetComponent<PlayerController>();
        }

        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(uiScreenPrefab).GetComponent<UIFade>();
        }

        if (GameManager.instance == null)
        {
            GameManager.instance = Instantiate(gameManagerPrefab).GetComponent<GameManager>();
        }
    }
}
