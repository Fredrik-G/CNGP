namespace Engine
{
    /// <summary>
    /// Class that contains information about a user/account.
    /// </summary>
    public class User
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string EMail { get; set; }

        private Statistics statistics;
        private IpInfo ipInfo;

        public string CurrentIp
        {
            get { return ipInfo.CurrentIp; }
            set { ipInfo.CurrentIp = value; }
        }
        public string RegisteredIp
        {
            get { return ipInfo.RegisteredIp; }
            set { ipInfo.RegisteredIp = value; }
        }


        public User(string name, string password, string eMail)
        {
            Name = name;
            Password = password;
            EMail = eMail;
        }
    }
}
