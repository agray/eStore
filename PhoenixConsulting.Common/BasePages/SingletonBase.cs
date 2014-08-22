#region Licence
/*
 * The MIT License
 *
 * Copyright (c) 2008-2013, Andrew Gray
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
using System;
using System.Globalization;
using System.Reflection;
using phoenixconsulting.common.handlers;

namespace phoenixconsulting.common.basepages {
    public abstract class SingletonBase<T> : HTTPScopeHandlerBase where T : class {
        ////Singleton based on http://csharpindepth.com/Articles/General/Singleton.aspx. Fifth version.

        /// <summary>
        /// Gets the singleton instance of this class.
        /// </summary>
        public static T Instance {
            get { return SingletonFactory.Instance; }
        }

        /// <summary>
        /// The singleton class factory to create the singleton instance.
        /// </summary>
        class SingletonFactory {
            // Prevent the compiler from generating a default constructor.
            SingletonFactory() { }

            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static SingletonFactory() {
                var theType = typeof(T);
                if(theType.Equals(typeof(SessionHandler))) {
                    SessionHandler.Instance.SetCurrency("AUD", 1, "1");
                }
            }
                        
            internal static readonly T Instance = GetInstance();

            static T GetInstance() {
                var theType = typeof(T);

                T inst;

                try {
                    inst = (T)theType
                      .InvokeMember(theType.Name,
                        BindingFlags.CreateInstance | BindingFlags.Instance
                        | BindingFlags.NonPublic,
                        null, null, null,
                        CultureInfo.InvariantCulture);
                } catch(MissingMethodException ex) {
                    throw new TypeLoadException(string.Format(
                      CultureInfo.CurrentCulture,
                      "The type '{0}' must have a private constructor to " +
                      "be used in the Singleton pattern.", theType.FullName)
                      , ex);
                }

                return inst;
            }
        }
    }
}