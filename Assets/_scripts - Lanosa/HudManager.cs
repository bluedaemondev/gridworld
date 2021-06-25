﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class HudManager : MonoBehaviour
    {
        public GameObject loseUI;
        public GameObject winUI;
        public GameObject mainHud;

        public EnemiesRemainingUI enemiesRemainingUi;

        public void UpdateUIEnemies(int countRemaining, int countMax)
        {
            enemiesRemainingUi.UpdateUI(countRemaining, countMax);
        }

        private void Start()
        {
            CursorLocker.instance.LockCursorOnCenter();
        }

        public void DisplayLose()
        {
            this.loseUI.SetActive(true);
            CursorLocker.instance.UnlockFreeCursor();
        }

        public void DisplayWin()
        {
            this.winUI.SetActive(true);
            CursorLocker.instance.UnlockFreeCursor();
        }
    }
}