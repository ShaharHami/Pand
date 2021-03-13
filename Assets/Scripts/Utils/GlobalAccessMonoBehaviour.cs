using System;
using Audio;
using Game;
using UnityEngine;

namespace Utils
{
    public class GlobalAccessMonoBehaviour : MonoBehaviour
    {
        protected GlobalState globalState;
        protected ObjectPooler objectPooler;
        protected LocalState localState;
        protected AudioController audioController;

        protected void InitializeReferences()
        {
            if (globalState == null)
            {
                globalState = GlobalState.Instance;
            }

            if (objectPooler == null)
            {
                objectPooler = ObjectPooler.Instance;
            }

            if (localState == null)
            {
                localState = LocalState.Instance;
            }

            if (audioController == null)
            {
                audioController = AudioController.Instance;
            }
        }
    }
}