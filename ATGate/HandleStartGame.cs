﻿using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace ATGate
{
    class HandleStartGame
    {
        /// <summary>
        /// 新建进程启动游戏
        /// </summary>
        /// <param name="absPath">游戏路径</param>
        /// <param name="cmd">CMD参数串</param>
        /// <returns>真/假</returns>
        public static bool StartProcessSimplify(string absPath, string cmd)
        {
            Console.WriteLine(cmd);
            STARTUPINFO si = new STARTUPINFO();
            PROCESS_INFORMATION pi = new PROCESS_INFORMATION();
            bool status = CreateProcess(
                absPath,
                cmd,
                IntPtr.Zero,
                IntPtr.Zero,
                false,
                0,
                IntPtr.Zero,
                Path.GetDirectoryName(absPath),
                ref si,
                out pi);
            return status;
        }

        /// <summary>
        /// CMD启动游戏
        /// </summary>
        /// <param name="server">服务器对象</param>
        /// <returns>真/假</returns>
        public static bool StartProcessByCmd(Server server)
        {
            Console.WriteLine(server.getCmdString);
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = server.getCmdString
            };
            process.StartInfo = startInfo;
            bool status = process.Start();
            return status;
        }

        /// <summary>
        /// 导入外部方式
        /// </summary>
        /// <param name="lpApplicationName"></param>
        /// <param name="lpCommandLine"></param>
        /// <param name="lpProcessAttributes"></param>
        /// <param name="lpThreadAttributes"></param>
        /// <param name="bInheritHandles"></param>
        /// <param name="dwCreationFlags"></param>
        /// <param name="lpEnvironment"></param>
        /// <param name="lpCurrentDirectory"></param>
        /// <param name="lpStartupInfo"></param>
        /// <param name="lpProcessInformation"></param>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        static extern bool CreateProcess(
            string lpApplicationName,
            string lpCommandLine,
            IntPtr lpProcessAttributes,
            IntPtr lpThreadAttributes,
            bool bInheritHandles,
            uint dwCreationFlags,
            IntPtr lpEnvironment,
            string lpCurrentDirectory,
            ref STARTUPINFO lpStartupInfo,
            out PROCESS_INFORMATION lpProcessInformation);


        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public uint dwProcessId;
            public uint dwThreadId;
        }

        public struct STARTUPINFO
        {
            public uint cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public uint dwX;
            public uint dwY;
            public uint dwXSize;
            public uint dwYSize;
            public uint dwXCountChars;
            public uint dwYCountChars;
            public uint dwFillAttribute;
            public uint dwFlags;
            public short wShowWindow;
            public short cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        public struct SECURITY_ATTRIBUTES
        {
            public int length;
            public IntPtr lpSecurityDescriptor;
            public bool bInheritHandle;
        }
    }
}
