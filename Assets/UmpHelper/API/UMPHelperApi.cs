using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace UMPHelper
{
    public class UMPHelperApi
    {

        
        public readonly static string iOS_SDK_Version = "1.0.0";
        public readonly static string Android_SDK_Version = "1.0.1";
        public readonly static string Unity_Package_Version = "1.0.1";
        private static UMPHelperCall helperCall = null;

        private static void instanceOfCall()
        {
            if (helperCall == null)
            {
                helperCall = new UMPHelperCall();
            }
        }

        public  bool isGDPR()
        {
            Debug.Log("===> call isGDPR ");
            instanceOfCall();
            return  helperCall.isGDPR();
        }

        public  bool isUmpAllowed()
        {
            Debug.Log("===> call isUmpAllowed ");
            instanceOfCall();
            return helperCall.isUmpAllowed();
        }
    }
}