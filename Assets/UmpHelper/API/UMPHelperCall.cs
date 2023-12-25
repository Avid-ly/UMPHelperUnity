using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UMPHelper
{
    public class UMPHelperCall
    {
#if UNITY_IOS && !UNITY_EDITOR
    
#elif UNITY_ANDROID && !UNITY_EDITOR
			private static AndroidJavaClass jc = null;
			private readonly static string JavaClassName = "com.avidly.ump.unitywrapper.UmpHelperWrapper";
			private readonly static string JavaClassStaticMethod_isGDPR = "isGDPR";
            private readonly static string JavaClassStaticMethod_isUmpAllowed = "isUmpAllowed";

#else
        // "do nothing";
#endif
        public UMPHelperCall()
        {
#if UNITY_IOS && !UNITY_EDITOR
      
#elif UNITY_ANDROID && !UNITY_EDITOR
			if (jc == null) {
				Debug.Log ("===> UMPHelperCall instanced.");
				jc = new AndroidJavaClass (JavaClassName);
			}
#endif
        }

        public bool isGDPR()
        {
            Debug.Log("===> call isGDPR in UMPHelperCall.");
#if UNITY_IOS && !UNITY_EDITOR

#elif UNITY_ANDROID && !UNITY_EDITOR
          if (jc != null) 
			{
				return jc.CallStatic<bool> (JavaClassStaticMethod_isGDPR);
			}else{
                Debug.Log ("===> jc is null.");
                return false;
            }
#endif
            return false;
        }

         public bool isUmpAllowed()
        {
            Debug.Log("===> call isUmpAllowed in UMPHelperCall.");
#if UNITY_IOS && !UNITY_EDITOR

#elif UNITY_ANDROID && !UNITY_EDITOR
          if (jc != null) 
			{
				return jc.CallStatic<bool> (JavaClassStaticMethod_isUmpAllowed);
			}else{
                Debug.Log ("===> jc is null.");
                return false;
            }
#endif
            return false;
        }
    }
}