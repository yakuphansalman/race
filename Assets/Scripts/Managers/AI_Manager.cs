using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Manager : MonoBehaviour
    {
        [SerializeField] private protected AI_Preferences_SO _aiPrefs;

        #region Commands
        private protected int _commAcc;
        private protected int _commSteer;
        private protected int _commBrake;
        #endregion

        private void FixedUpdate()
        {
            
        }

    }
}

