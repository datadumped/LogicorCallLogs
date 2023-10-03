namespace LogicorSupportCalls.Shared
{
    public class AppState
    {
        public bool Authenticated { get; set; }
        public bool IsNavMenuExpanded { get; private set; } = true;

        public event Action OnChange;

        public void ToggleNavMenu()
        {
            IsNavMenuExpanded = !IsNavMenuExpanded;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
