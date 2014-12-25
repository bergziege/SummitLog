using System.Diagnostics;
using System.IO;
using System.Text;

namespace De.BerndNet2000.SummitLog.Wpf {
    internal static class UpdateService {
        #region Public Methods

        public static bool Update(){
            // check if updater has been run before
            if (File.Exists("./update.finished")) {
                // delete updater dummy and return false
                File.Delete("./update.finished");
                return false;
            }
            // start updater and return true
            Process updater = new Process{ StartInfo ={ FileName = new StringBuilder().Append(Path.GetFullPath(".")).Append(Path.DirectorySeparatorChar).Append("SoftwareUpdater.exe").ToString(), Arguments = new StringBuilder().Append("/nogui").ToString() } };

            // Start application
            updater.Start();
            return true;
        }

        #endregion
    }
}