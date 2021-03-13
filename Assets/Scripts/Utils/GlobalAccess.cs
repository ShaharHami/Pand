using Audio;
using Game;

namespace Utils
{
    public class GlobalAccess
    {
        protected readonly GlobalState globalState = GlobalState.Instance;
        protected readonly ObjectPooler objectPooler = ObjectPooler.Instance;
        protected readonly LocalState localState = LocalState.Instance;
        protected readonly AudioController audioController = AudioController.Instance;
    }
}