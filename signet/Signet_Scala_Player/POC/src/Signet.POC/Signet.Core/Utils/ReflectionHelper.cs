namespace Signet.Core.Utils
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class ReflectionHelper
    {
        public static ComponentCallDetails GetComponentCallDetails()
        {
            StackTrace trace = new StackTrace(false);
            return GetComponentCallDetails(trace.GetFrames().First<StackFrame>(f => !f.GetMethod().DeclaringType.Namespace.StartsWith(new StackFrame().GetMethod().DeclaringType.Namespace, StringComparison.InvariantCultureIgnoreCase)));
        }

        private static ComponentCallDetails GetComponentCallDetails(StackFrame targetFrame)
        {
            if (targetFrame != null)
            {
                StringBuilder output = new StringBuilder();
                MethodBase method = targetFrame.GetMethod();
                string namespaceName = method.DeclaringType.Namespace;
                string className = method.DeclaringType.Name;
                output.Append(method.Name);
                output.Append("(");
                ParameterInfo[] paramInfos = method.GetParameters();
                if (paramInfos.Length > 0)
                {
                    output.Append(string.Format("{0} {1}", paramInfos[0].ParameterType.Name, paramInfos[0].Name));
                    if (paramInfos.Length > 1)
                    {
                        for (int j = 1; j < paramInfos.Length; j++)
                        {
                            output.Append(string.Format(", {0} {1}", paramInfos[j].ParameterType.Name, paramInfos[j].Name));
                        }
                    }
                }
                output.Append(")");
                string methodSignature = output.ToString();
                return new ComponentCallDetails { Namespace = namespaceName, Classname = className, MethodSignature = methodSignature };
            }
            return new ComponentCallDetails { Namespace = "n/a", Classname = "n/a", MethodSignature = "n/a" };
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static ComponentCallDetails GetComponentCallDetails(int stackFrameIndex)
        {
            StackFrame targetFrame = new StackFrame(stackFrameIndex);
            return GetComponentCallDetails(targetFrame);
        }

        public static string GetComponentName(int stackFrameIndex)
        {
            StackFrame stackFrame = new StackFrame(stackFrameIndex);
            return stackFrame.GetMethod().DeclaringType.ToString();
        }

        public static string GetMethodName(int stackFrameIndex)
        {
            StackFrame stackFrame = new StackFrame(stackFrameIndex);
            return stackFrame.GetMethod().Name;
        }
    }
}