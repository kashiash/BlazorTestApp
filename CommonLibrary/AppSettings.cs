namespace Common.Module.Utils
{
    public class AppSettings
    {
        public static string ConnectionString
        {
            get
            {
                return "Integrated Security=SSPI;Pooling=false;Data Source=.;Initial Catalog=fleetman";
                //@"Server=tcp:xafblazorjk.database.windows.net,1433;Initial Catalog=fleetman;Persist Security Info=False;User ID=kashiash;Password=N!....20;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            }

        }
    }
}