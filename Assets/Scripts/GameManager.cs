using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.RunScripts;
using Unity.VisualScripting;
using System;

public sealed class GameManager : MonoBehaviour
{
    
    public static RunController runController;
    public ulong Souls;
    private static GameManager _instance;
    private static System.Random _rnd;

    public static System.Random rnd
    {
        get
        {
            if(_rnd == null)
            {
                _rnd = new System.Random();
            }
            return _rnd;
        }
    }

    // Публичное статическое свойство для доступа к экземпляру
    public static GameManager Instance
    {
        get
        {
            // Если экземпляр не существует, создаем его
            if (_instance == null)
            {
                // Создаем новый объект и добавляем к нему компонент GameManager
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            // Если экземпляр не существует, назначаем текущий объект и не уничтожаем его при загрузке новой сцены
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Если экземпляр уже существует, уничтожаем текущий объект, чтобы сохранить единственность
            Destroy(gameObject);
        }
    }

}
