using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class HudManager : MonoBehaviour
    {
        public GameObject loseUI;
        public GameObject winUI;
        public GameObject mainHud;


        public void DisplayLose()
        {
            this.loseUI.SetActive(true);
        }
        public void DisplayWin()
        {
            this.winUI.SetActive(true);
        }
    }
}