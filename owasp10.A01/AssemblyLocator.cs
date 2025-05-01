using System.Reflection;

namespace owasp10.A01;

public static class AssemblyLocator
{
    public static Assembly GetAssembly()
    {
        return Assembly.GetExecutingAssembly();
    }
}
