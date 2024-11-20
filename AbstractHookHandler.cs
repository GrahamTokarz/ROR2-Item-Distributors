using AutoItemPickup.Configuration;

namespace AutoItemPickup.Hooks
{

    public abstract class AbstractHookHandler
    {
        internal HookManager hookManager;

        protected AutoItemPickup Plugin => hookManager.Plugin;
        protected Config ModConfig => Plugin.ModConfig;

        public void Init(HookManager _hookManager)
        {
            hookManager = _hookManager;
        }

        //TODO: Figure out if those can be automated by storing reference to hook and method
        public abstract void RegisterHooks();

        public abstract void UnregisterHooks();
    }
}