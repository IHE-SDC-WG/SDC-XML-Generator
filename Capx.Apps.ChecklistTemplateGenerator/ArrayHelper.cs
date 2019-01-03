using System;

namespace Capx.Apps.ChecklistTemplateGenerator.DAL
{
    /// <summary>
    /// Array Helper extends the system.array class with two add methods
    /// </summary>
    public static class ArrayHelper
    {
        #region Extension methods for Array

        /// <summary>
        /// Add an object class to an object array
        /// </summary>
        /// <param name="input">array of objects</param>
        /// <param name="o1">an object to be added</param>
        /// <returns>the array of objects plus the object to be added</returns>
        /// <example>a.Items = a.Items.Add(a1) where Items is an object array</example>
        public static object[] Add
            (this Array input, 
            object o1)
        {
            object[] o2;
            if (input == null)
            {
                o2 = new object[1];
                o2[0] = o1;
                return o2;
            }
            o2 = new object[input.Length + 1];
            Int32 i = 0;
            foreach (var k in input)
            {
                o2[i] = (object)k;
                i++;
            }
            o2[i] = o1;
            return o2;
        }

        /// <summary>
        /// Add an item of any class to an array of that class
        /// </summary>
        /// <typeparam name="T">generic class variable</typeparam>
        /// <param name="input">an array of a specific class</param>
        /// <param name="o1">an instance of a specific class</param>
        /// <returns>an array of specific class plus the instance of the specific class to be added</returns>
        /// <example>a.Questions = a.Questions.AddItem(q1) where Questions is an array of class question</example>
        public static T[] AddItem<T>(this Array input, T o1)
        {
            T[] o2;
            if (input == null)
            {
                o2 = new T[1];
                o2[0] = o1;
                return o2;
            }
            o2 = new T[input.Length + 1];
            Int32 i = 0;
            foreach (var k in input)
            {
                o2[i] = (T)k;
                i++;
            }
            o2[i] = o1;
            return o2;
        }

        #endregion
    }
}
