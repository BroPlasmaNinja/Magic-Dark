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

    // ��������� ����������� �������� ��� ������� � ����������
    public static GameManager Instance
    {
        get
        {
            // ���� ��������� �� ����������, ������� ���
            if (_instance == null)
            {
                // ������� ����� ������ � ��������� � ���� ��������� GameManager
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            // ���� ��������� �� ����������, ��������� ������� ������ � �� ���������� ��� ��� �������� ����� �����
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ���� ��������� ��� ����������, ���������� ������� ������, ����� ��������� ��������������
            Destroy(gameObject);
        }
    }

}
