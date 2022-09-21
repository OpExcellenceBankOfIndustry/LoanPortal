using BOILoanPortal.Models;

namespace BOILoanPortal.Services
{
    public class AppState
    {
        /// <summary>
        /// This state property with initial value
        /// </summary>
        public object? Value { get; private set; }
        /// <summary>
        /// The event will be raised for state changed
        /// </summary>
        public event Action? OnStateChange;
        /// <summary>
        /// This method will be accessed by the sender component 
        /// to update the state
        /// </summary>
        /// <param name="value"></param>
        public void SetParameters(object value)
        {
            Value = value;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnStateChange?.Invoke();
    }
}
