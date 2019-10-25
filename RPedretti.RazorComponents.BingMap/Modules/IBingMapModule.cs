using System.Threading.Tasks;

namespace RPedretti.RazorComponents.BingMap.Modules
{
    public interface IBingMapModule
    {
        #region Methods

        Task InitAsync(string mapId);

        #endregion Methods
    }
}
