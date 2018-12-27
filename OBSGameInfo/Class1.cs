using System;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace OBSGameInfo
{
    public class OBSGameInfo : IGameLaunchingPlugin
    {
        string path = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        public void OnAfterGameLaunched(IGame game, IAdditionalApplication app, IEmulator emulator)
        {

            //serializes json
            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(game);
            //Check if folder exists, if not, creates.
            Directory.CreateDirectory(path + "\\obs");
            //Write Json to folder
            File.WriteAllText(path + "\\obs\\game.json", jsonString);

            //Write individual files

            File.WriteAllText(path + "\\obs\\title.txt", game.Title);
            File.WriteAllText(path + "\\obs\\platform.txt", game.Platform);
            File.WriteAllText(path + "\\obs\\year.txt", game.ReleaseYear.ToString());
            File.WriteAllText(path + "\\obs\\publisher.txt", game.Publisher);
            File.WriteAllText(path + "\\obs\\developer.txt", game.Developer);
            File.WriteAllText(path + "\\obs\\notes.txt", game.Notes);
            File.WriteAllText(path + "\\obs\\DetailsWithPlatform.txt", game.DetailsWithPlatform);

            //Copy Some Images
            copyFiles(game.ScreenshotImagePath, "screenshot");
            copyFiles(game.FrontImagePath, "frontImage");
            copyFiles(game.ClearLogoImagePath, "clearlogo");
            copyFiles(game.Box3DImagePath, "3dbox");
            copyFiles(game.BackImagePath, "backImage");
            copyFiles(game.BackImagePath, "backImage");
        }

        public void OnBeforeGameLaunching(IGame game, IAdditionalApplication app, IEmulator emulator)
        {
            throw new NotImplementedException();
        }

        public void OnGameExited()
        {
            throw new NotImplementedException();
        }

        public void copyFiles(string currentDir, string type)
        {
            var finalDest = path + @"\obs\" + type + ".jpg";
            if (currentDir == null)
            {
                File.Delete(finalDest);
                return;
            }else
            {
                FileInfo currentFile = new FileInfo(currentDir);
                File.Copy(currentFile.FullName, finalDest, true);
            }
        }
    }
}
