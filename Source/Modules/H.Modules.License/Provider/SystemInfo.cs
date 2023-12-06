// Copyright © 2022 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-ControlBase

using System;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace H.Modules.License
{
    internal class SystemInfo
    {
        public static SystemInfo Instance = new SystemInfo();

        private SystemInfo()
        {
            HostID = GetHostId();   //"BTSOTURGTNSQMDQX"
            HostName = Dns.GetHostName();
            OSVersion = Environment.OSVersion.ToString();
            Ipv4 = GetLocalIp(AddressFamily.InterNetwork);
            Ipv6 = GetLocalIp(AddressFamily.InterNetworkV6);
        }
        public string AppVersion { get; private set; }

        public string AppVersionGit { get; private set; }

        public string AppVersionGitLong { get; private set; }

        public string HostName { get; private set; }

        public string Ipv4 { get; private set; }

        public string Ipv6 { get; private set; }

        public string ProductName { get; private set; }

        public string SerialNumber { get; private set; }

        public string HostID { get; private set; }

        public string OSVersion { get; private set; }

        /// <summary>
        /// Gets the local ip. Currently work correctly for 1 NIC in Host.
        /// </summary>
        /// <param name="family">The family.</param>
        /// <returns>System.String.</returns>
        private string GetLocalIp(AddressFamily family)
        {
            string name = Dns.GetHostName();
            IPAddress[] ipList = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipList)
            {
                if (ipa.AddressFamily == family)
                {
                    return ipa.ToString();
                }
            }
            return string.Empty;
        }

        private const int HostIDLength = 16;

        private string GetHostId()
        {
            string id;
            int i = 0, sum = 0;
            int start, index;
            byte[] idArry = new byte[HostIDLength];

            string hwInfo = "CPU:" + cpuId() + ";HDD:" + diskId() + ";MAC:" + macId();
#if NETCOREAPP3_1||NET5_0||NET6_0||NET7_0||NET8_0
            SHA256 sha = SHA256.Create();
#else
            SHA256Cng sha = new SHA256Cng();
#endif

            byte[] bt = sha.ComputeHash(ASCIIEncoding.Default.GetBytes(hwInfo));

            for (i = 0; i < bt.Length; i++)
            {
                sum += bt[i];
            }
            start = sum % bt.Length;
            index = sum % (bt.Length - 1);
            sum = 0;
            for (i = 0; i < idArry.Length; i++)
            {
                idArry[i] = (byte)(65 + bt[(start + index * i) % bt.Length] % 26);
            }
            id = Encoding.Default.GetString(idArry);

            return id;
        }

        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }

        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            ManagementClass mc = new ManagementClass(wmiClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty]?.ToString();
                        break;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return result;
        }

        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }

        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model") + "_"
            + identifier("Win32_DiskDrive", "Signature");
            /*return identifier("Win32_DiskDrive", "Model") + ";"
            + identifier("Win32_DiskDrive", "Manufacturer") + ";"
            + identifier("Win32_DiskDrive", "Signature") + ";"
            + identifier("Win32_DiskDrive", "TotalHeads");*/
        }

        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }

        //public string GetIdn()
        //{
        //    return $"{Config.CompanyName},{productName},{serialNumber},{appVersion}";
        //}
    }
}
