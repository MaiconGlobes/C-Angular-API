namespace Src.Models.Instances
{
   internal class InstanceToken
    {
        private static readonly object FLockObject = new();
        private static object FResult;

        internal static void SaveInstance(object result)
        {
            lock (FLockObject)
                FResult = result;
        }

        internal static object LoadInstance()
        {
            lock (FLockObject)
                return FResult;
        }
    }
}
