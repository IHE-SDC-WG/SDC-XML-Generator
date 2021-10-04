/* TODO ERROR: Skipped IfDirectiveTrivia */
using System.Collections;
using System.Diagnostics;
using System.Windows;
using Microsoft.VisualBasic;

namespace SDC.Gen.UI.WPF
{
    namespace My
    {
        /// <summary>
    /// Module used to define the properties that are available in the My Namespace for WPF
    /// </summary>
    /// <remarks></remarks>
        [HideModuleName()]
        static class MyWpfExtension
        {
            private static MyProject.ThreadSafeObjectProvider<Microsoft.VisualBasic.Devices.Computer> s_Computer = new MyProject.ThreadSafeObjectProvider<Microsoft.VisualBasic.Devices.Computer>();
            private static MyProject.ThreadSafeObjectProvider<Microsoft.VisualBasic.ApplicationServices.User> s_User = new MyProject.ThreadSafeObjectProvider<Microsoft.VisualBasic.ApplicationServices.User>();
            private static MyProject.ThreadSafeObjectProvider<MyWindows> s_Windows = new MyProject.ThreadSafeObjectProvider<MyWindows>();
            private static MyProject.ThreadSafeObjectProvider<Microsoft.VisualBasic.Logging.Log> s_Log = new MyProject.ThreadSafeObjectProvider<Microsoft.VisualBasic.Logging.Log>();
            /// <summary>
        /// Returns the application object for the running application
        /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            internal static Application Application
            {
                get
                {
                    return (Application)System.Windows.Application.Current;
                }
            }
            /// <summary>
        /// Returns information about the host computer.
        /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            internal static Microsoft.VisualBasic.Devices.Computer Computer
            {
                get
                {
                    return s_Computer.GetInstance;
                }
            }
            /// <summary>
        /// Returns information for the current user.  If you wish to run the application with the current
        /// Windows user credentials, call My.User.InitializeWithWindowsUser().
        /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            internal static Microsoft.VisualBasic.ApplicationServices.User User
            {
                get
                {
                    return s_User.GetInstance;
                }
            }
            /// <summary>
        /// Returns the application log. The listeners can be configured by the application's configuration file.
        /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            internal static Microsoft.VisualBasic.Logging.Log Log
            {
                get
                {
                    return s_Log.GetInstance;
                }
            }

            /// <summary>
        /// Returns the collection of Windows defined in the project.
        /// </summary>
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
            internal static MyWindows Windows
            {
                [DebuggerHidden()]
                get
                {
                    return s_Windows.GetInstance;
                }
            }

            [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
            [MyGroupCollection("System.Windows.Window", "Create__Instance__", "Dispose__Instance__", "My.MyWpfExtenstionModule.Windows")]
            internal sealed class MyWindows
            {
                [DebuggerHidden()]
                private static T Create__Instance__<T>(T Instance) where T : Window, new()
                {
                    if (Instance is null)
                    {
                        if (s_WindowBeingCreated is object)
                        {
                            if (s_WindowBeingCreated.ContainsKey(typeof(T)) == true)
                            {
                                throw new System.InvalidOperationException("The window cannot be accessed via My.Windows from the Window constructor.");
                            }
                        }
                        else
                        {
                            s_WindowBeingCreated = new Hashtable();
                        }

                        s_WindowBeingCreated.Add(typeof(T), null);
                        return new T();
                        s_WindowBeingCreated.Remove(typeof(T));
                    }
                    else
                    {
                        return Instance;
                    }
                }

                [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
                [DebuggerHidden()]
                private void Dispose__Instance__<T>(ref T instance) where T : Window
                {
                    instance = null;
                }

                [DebuggerHidden()]
                [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
                public MyWindows() : base()
                {
                }

                [System.ThreadStatic()]
                private static Hashtable s_WindowBeingCreated;

                [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
                public override bool Equals(object o)
                {
                    return base.Equals(o);
                }

                [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
                public override int GetHashCode()
                {
                    return base.GetHashCode();
                }

                [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
                [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
                internal new System.Type GetType()
                {
                    return typeof(MyWindows);
                }

                [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
                public override string ToString()
                {
                    return base.ToString();
                }
            }
        }
    }

    public partial class Application : System.Windows.Application
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        internal Microsoft.VisualBasic.ApplicationServices.AssemblyInfo Info
        {
            [DebuggerHidden()]
            get
            {
                return new Microsoft.VisualBasic.ApplicationServices.AssemblyInfo(System.Reflection.Assembly.GetExecutingAssembly());
            }
        }
    }
}
/* TODO ERROR: Skipped EndIfDirectiveTrivia */