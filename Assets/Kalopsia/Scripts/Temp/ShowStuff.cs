using UnityEngine;
using UnityEngine.UI;

public class ShowStuff : MonoBehaviour
{
    public GameObject mCh; // Player

    void Start()
    {
        mCh = GameObject.Find("mCh");
    }
    void Update()
    {
        GetComponent<Text>().text = "Stamina: " + mCh.GetComponent<mCh_Mov>().curStamina;
    }
}
