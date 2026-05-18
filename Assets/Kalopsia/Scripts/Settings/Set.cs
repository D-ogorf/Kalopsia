using System;
using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
// Transformara em scriptable object
public class Set : MonoBehaviour
{
    public int loadOptionInt = 0;
    public KeyCode up; // Cima
    public KeyCode down; // Baixo
    public KeyCode left; // Esquerda
    public KeyCode right; // Direita
    public KeyCode jump; // Pulo
    public KeyCode dash; // Dash
    void Awake()
    {
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

        TransferSettings();
    }

    public void LoadSavedSettings()
    {
        TransferSettings();
    }

    private void TransferSettings()
    {
        this.GetComponent<mCh_Mov>().settings = this.GetComponent<Set>();
    }
}
