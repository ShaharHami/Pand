using Audio;
using Ball;
using UnityEngine;
using Utils;

namespace App
{
    public class AppBase
    {
        public readonly GlobalState globalState;
        public readonly ObjectPooler objectPooler;
        public readonly LocalState localState;
        public readonly AudioController audioController;

        public AppBase()
        {
            globalState = GameObject.FindObjectOfType<GlobalState>();
            objectPooler = GameObject.FindObjectOfType<ObjectPooler>();
            localState = GameObject.FindObjectOfType<LocalState>();
            audioController = GameObject.FindObjectOfType<AudioController>();
        }
    }
}