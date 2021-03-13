using UnityEngine;

namespace App
{
    public class AppMonoBehaviour : MonoBehaviour
    {
        private App _app;

        protected App App
        {
            get
            {
                if (_app == null)
                {
                    _app = new App();
                }

                return _app;
            }
        }
    }
}