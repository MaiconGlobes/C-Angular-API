namespace BaseCodeAPI.Src.Models
{
   internal class EnvironmentsSettings
   {
      internal static EnvironmentsSettings _instance { get; set; }
      internal IConfigurationRoot _iConfigRoot { get; set; }

      internal static EnvironmentsSettings New()
      {
         _instance ??= new EnvironmentsSettings();
         return _instance;
      }

      internal EnvironmentsSettings()
      {
         _iConfigRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("settingsconfig.json")
            .Build();
      }
   }
}
