namespace Cappuccino.App.iOS;

public class Localization {
    private static System.Resources.ResourceManager? instance;
    public static System.Resources.ResourceManager Instance {
        get {
            if (instance == null) {
                var localizationFile = "Cappuccino.App.iOS.Resources.lang";
                var assembly = System.Reflection.Assembly.GetExecutingAssembly();
                instance = new System.Resources.ResourceManager(localizationFile, assembly);
            }
            return instance;
        }
    }
}