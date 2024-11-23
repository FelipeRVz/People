using System.Reflection;

namespace People.Services
{
    public static class Functions
    {
        public static void CopyPropertyAtoB(object a, object b)
        {
            Type aType = a.GetType();
            Type bType = b.GetType();
            foreach (PropertyInfo aProperty in aType.GetProperties())
            {
                PropertyInfo? bProperty = bType.GetProperty(aProperty.Name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (bProperty != null)
                    if (bProperty.CanWrite)
                        bProperty.SetValue(b, aProperty.GetValue(a));
            }

        }
    }
}
