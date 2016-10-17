using System;
using System.Windows;
using W1.AdminTools.WPF.Extensions.Markup;

namespace WpfExecutor.Extensions.Localization
{
    /// <summary>
    /// Базовый класс для слушателей изменения культуры
    /// </summary>
    public abstract class BaseLocalizationListener : IWeakEventListener, IDisposable
    {
        protected BaseLocalizationListener()
        {
            CultureChangedEventManager.AddListener(LocalizationManager.Instance, this);
        }

        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (managerType == typeof(CultureChangedEventManager) && e is CultureChangedEventArgs)
            {
                OnCultureChanged((CultureChangedEventArgs)e);
                return true;
            }
            return false;
        }

        protected abstract void OnCultureChanged(CultureChangedEventArgs e);

        public void Dispose()
        {
            CultureChangedEventManager.RemoveListener(LocalizationManager.Instance, this);
            GC.SuppressFinalize(this);
        }
    }
}
