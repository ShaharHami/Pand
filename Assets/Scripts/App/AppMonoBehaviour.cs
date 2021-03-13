using UnityEngine;

namespace App
{
    public class AppMonoBehaviour : MonoBehaviour
    {
        private App _app;

        protected App App => _app ?? (_app = new App());
    }
}