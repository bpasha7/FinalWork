using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FinalWork
{
    class DllManager //: IEnumerable
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibrary")]
        static extern int LoadLibrary(
            [MarshalAs(UnmanagedType.LPStr)] string lpLibFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress")]
        static extern IntPtr GetProcAddress(int hModule,
            [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary")]
        static extern bool FreeLibrary(int hModule);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void CPPStringParse(StringBuilder Params, int length);
        
        //Properies
        
        int DllHandle;
        int id;
        string DllName;
        string DllVersion;
        string DllAuthor;
        List<DllFunction> Functions;
        public int ID
        {
            get { return id; }
        }
        public string Name
        {
            get { return DllName; }
        }
        public string Info
        {
            get
            {
                return string.Format("Плагин: {0} ver({1}), Автор: {2} ", DllName, DllVersion, DllAuthor);
            }
        }
        public DllFunction GetFunction(int index)
        {
            return Functions[index];
        }
        public int FunctionsCount
        {
            get { return Functions.Count;}
        }
        public object CallFunctions(string FunctionName, params object[] args)
        {
            var r = from Function in Functions where Function.Name == FunctionName select Function;
            try
            {
                object er = r.ToList<DllFunction>()[0].Execute(args);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return r.ToList<DllFunction>()[0].Execute(args);
            }
        public DllManager(string DllPath, int Id)
        {
            id = Id;
            DllHandle = LoadLibrary(@DllPath);
            DllName = GetCppString("DllName");
            DllVersion = GetCppString("DllVersion");
            DllAuthor = GetCppString("DllAuthor");
            Functions = new List<DllFunction>();
            FoundFunctions();
            SetDelegates();
        }
        private void FoundFunctions()
        {
            string[] AllFunctions = GetCppString("FunctionNames").Split(new char[] { ';' });
            for(int i =0;i< AllFunctions.Length; i++)
                Functions.Add(new DllFunction(AllFunctions[i]));           
        }
        private void SetDelegates()
        {
            for (int i = 0; i < Functions.Count; i++)
            {
                Functions[i].Process = GetProcAddress(DllHandle, Functions[i].Name);
                Functions[i].SetDelegate();
            }
        }
        private string GetCppString(string FunctionName)
        {
            IntPtr pString = GetProcAddress(DllHandle, FunctionName);
            CPPStringParse Param = (CPPStringParse)Marshal.GetDelegateForFunctionPointer(pString, typeof(CPPStringParse));
            StringBuilder result = new StringBuilder(2048);
            Param(result, result.Capacity);
            return result.ToString();
        }
    }
}
