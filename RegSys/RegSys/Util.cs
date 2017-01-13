using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegSys
{
    class Util
    {
        //定义变量，密码返回结果
        public static bool passwordResult = false;

        //定义变量，检查密码状态
        public static bool passwordChecking = false;

        //定义变量，管理员密码
        public static string password = "PBCguoku";

        // 定义变量,句柄类型
        public static IntPtr hanfm1;
        public static IntPtr hanfm2;

        public static void CheckPW(string pw)
        {
            passwordResult = false;
            if (pw == password)
            {
                passwordResult = true;
            }           
        }
    }
}
