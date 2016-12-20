using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace FinalWork
{
    class DllFunction
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate long InitializateData(string IP, string FileName, string[] Names, string[] Properties, string[] Dates, int Count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate void CPPStringParse(StringBuilder Params, int length);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate int Export(long InitializatedData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        delegate long Acting(long InitializatedData);

        //
        string FunctionName;
        IntPtr ProccesAddress;
        Delegate Function;

        public DllFunction()
        {
            ProccesAddress = (IntPtr)0;
            FunctionName = "Not Loaded";
        }
        public DllFunction(string Name)
        {
            FunctionName = Name;
        }
        public string Name
        {
            get { return FunctionName; }
            set { FunctionName = value; }
        }
        public bool SetDelegate()
        {
            if (Name != "" || Name != "Not Loaded")
            {
                switch(Name)
                {
                    case "EncodeHexProperty": Function = Function = (Acting)Marshal.GetDelegateForFunctionPointer(ProccesAddress, typeof(Acting)); break;
                    case "SortDataByProperties": Function = Function = (Acting)Marshal.GetDelegateForFunctionPointer(ProccesAddress, typeof(Acting)); break;
                    case "FormComponets": Function = (CPPStringParse)Marshal.GetDelegateForFunctionPointer(ProccesAddress, typeof(CPPStringParse)); break;
                    case "CSVExport": Function = Function = (Export)Marshal.GetDelegateForFunctionPointer(ProccesAddress, typeof(Export)); break;
                    case "HtmlExport": Function = Function = (Export)Marshal.GetDelegateForFunctionPointer(ProccesAddress, typeof(Export)); break;
                    case "Initializate": Function = (InitializateData)Marshal.GetDelegateForFunctionPointer(ProccesAddress, typeof(InitializateData)); break;
                    default: break;
                }
                return true;
            }
            else
                return false;          
        }
        public Delegate GetDelegate
        {           
            get { return Function; }
        }
        public object Execute(params object[] args)
        {
            if (Function == null)
                return -1;
            switch (FunctionName)
            {
                case "EncodeHexProperty": return Function.DynamicInvoke(args[0]);
                case "SortDataByProperties": return Function.DynamicInvoke(args[0]);
                case "FormComponets": return Function.DynamicInvoke(args[0], args[1]);
                case "CSVExport": return Function.DynamicInvoke(args[0]);
                case "HtmlExport": return Function.DynamicInvoke(args[0]);
                case "Initializate": return Function.DynamicInvoke(args[0], args[1], args[2], args[3], args[4], args[5]);
                default: return null;
            }
        }
        public IntPtr Process
        {
            get { return ProccesAddress; }
            set { ProccesAddress = value; }
        }
    }

    public class Class1
    {
    }
}
