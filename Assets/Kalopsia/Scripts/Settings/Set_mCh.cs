using System;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Rendering;
// Transformara em scriptable object
public class Set_mCh : MonoBehaviour 
{
    public GameObject player;
    public int loadOptionInt = 0;
    public KeyCode up; // Cima
    public KeyCode down; // Baixo
    public KeyCode left; // Esquerda
    public KeyCode right; // Direita
    public KeyCode jump; // Pulo
    public KeyCode dash; // Dash

    public KeyCode shoot; // Atirar
    public KeyCode altFire; // Tiro alternativo
    void Awake()
    {
        player = GameObject.Find("player");

        if(this.loadOptionInt == 0) LoadDefaultSettings();
        else LoadSavedSettings();
    }
    public void LoadDefaultSettings()
    {
        this.up = KeyCode.W;
        this.down = KeyCode.S;
        this.left = KeyCode.A;
        this.right = KeyCode.D;
        this.jump = KeyCode.Space;
        this.dash = KeyCode.LeftShift;

        this.shoot = KeyCode.Mouse0;
        this.altFire = KeyCode.Mouse1;
    
        TransferSettings();
    }

    public void LoadSavedSettings()
    {
        TransferSettings();
    }

    private void TransferSettings()
    {
        try
        {
            this.player.GetComponent<mCh_Mov>().settings = this.GetComponent<Set_mCh>();
            this.player.GetComponentInChildren<Gun_Array>().settings = this.GetComponent<Set_mCh>();
        }
        catch(Exception e)
        {
            print(e);
        }
    }
}
