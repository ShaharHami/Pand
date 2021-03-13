using UnityEngine;

namespace App
{
    public class AppMonoBehaviour : MonoBehaviour
    {
        private AppBase _app;

        protected AppBase App => _app ?? (_app = new AppBase());
    }
}