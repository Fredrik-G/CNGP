using System;
using UnityEngine;
namespace Engine
{
    /// <summary>
    /// Class that contains information about IP.
    /// </summary>
    public class IpInfo
    {
        public string CurrentIp { get; set; }
        public string RegisteredIp { get; set; }

        public IpInfo()
        {
            CurrentIp = RegisteredIp = String.Empty;
        }

        public IpInfo(string currentIp, string registeredIp)
        {
            CurrentIp = currentIp;
            RegisteredIp = registeredIp;
        }
    }
}
