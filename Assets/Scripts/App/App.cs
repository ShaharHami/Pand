using Audio;
using Utils;

namespace App
{
    public class App
    {
        public readonly GlobalState globalState = GlobalState.Instance;
        public readonly ObjectPooler objectPooler = ObjectPooler.Instance;
        public readonly LocalState localState = LocalState.Instance;
        public readonly AudioController audioController = AudioController.Instance;
    }
}