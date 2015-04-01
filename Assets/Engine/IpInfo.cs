namespace Engine
{
    /// <summary>
    /// Class that contains information about IP.
    /// </summary>
    class IpInfo
    {
        public string CurrentIp { get; set; }
        public string RegisteredIp { get; set; }

        public IpInfo(string currentIp, string registeredIp)
        {
            CurrentIp = currentIp;
            RegisteredIp = registeredIp;
        }
    }
}
