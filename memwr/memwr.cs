using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace memwr
{
    /* Author: Suyeon */
    class memwr
    {
        /* DLL Imports */
        [DllImport("kernel32.dll", EntryPoint = "WriteProcessMemory")]
        private static extern int MemoryWrite(int hProcess, int lpBaseAddress, ref int lpBuffer, int nSize, ref int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        private static extern int OpenProcess(int dwDesiredAccess, int bInheritHandle, int dwProcessId);

        /* Constants */
        const int cst = 0x1f0ff;
        const int intSize = 4;

        static void Main(string[] args)
        {
            /* 'target' process name without extension
             * 'memAddr' target (must be an int32) 
             * 'amount' value for the int pointed by memAddr*/
            String target = "";
            int memAddr = 0;
            int amount = 0;
            WriteInt32(target, memAddr, amount);
        }

        static void WriteInt32(string process, int addr, int val)
        {
            Process[] processes = Process.GetProcessesByName(process);
            int currentProcess = OpenProcess(cst, 0, processes[0].Id);
            int writeCount = 0;
            int hAddress = addr;
            int vBuffer = val;
            MemoryWrite(currentProcess, hAddress, ref vBuffer, intSize, ref writeCount);
        }
    }
}
