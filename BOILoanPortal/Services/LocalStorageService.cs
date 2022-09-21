using Microsoft.JSInterop;
using System.Text.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace BOILoanPortal.Services
{
    public interface ILocalStorageService
    {
        Task<T> GetItem<T>(string key);
        Task RemoveItem(string key);
        Task SetItem<T>(string key, T value);
    }

    public class LocalStorageService : ILocalStorageService
    {
        private IJSRuntime _jsRuntime;
        private ProtectedSessionStorage _ProtectedSessionStore;

        public LocalStorageService(IJSRuntime jsRuntime, ProtectedSessionStorage ProtectedSessionStore)
        {
            _jsRuntime = jsRuntime;
            _ProtectedSessionStore = ProtectedSessionStore;
        }

        public async Task<T> GetItem<T>(string key)
        {
            //var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            var result = await _ProtectedSessionStore.GetAsync<string>(key);

            if (!result.Success)
                return default;

            return JsonSerializer.Deserialize<T>(result.Value);
        }

        public async Task SetItem<T>(string key, T value)
        {
            try
            {
                //await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, JsonSerializer.Serialize(value));
                await _ProtectedSessionStore.SetAsync(key, JsonSerializer.Serialize(value));
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task RemoveItem(string key)
        {
            //await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
            await _ProtectedSessionStore.DeleteAsync(key);
        }
    }
}
