﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.RunScripts
{
    internal class LvlUpUI : MonoBehaviour
    {
        [SerializeField]
        GameObject UIRoot;
        [SerializeField]
        GameObject FirstImage;
        [SerializeField]
        GameObject SecondImage;
        [SerializeField]
        GameObject ThirdImage;

        public Spell[] crutch;

        public void PickFirst()
        {
            crutch[0].state.lvl++;
        }
        public void PickSecond()
        {
            crutch[1].state.lvl++;
        }
        public void PickThird()
        {
            crutch[2].state.lvl++;
        }
        public void TurnOff()
        {
            UIRoot.SetActive(false);
        }
        private void Start()
        {
            GameManager.runController.LVLUP += TurnOn;
        }

        private void TurnOn(object sender, EventArgs e)
        {
            UIRoot.SetActive(true);
            if(crutch == null)
                crutch = Player.ins.SpellList.ToArray();
        }
    }
}
