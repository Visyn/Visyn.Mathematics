#region Copyright (c) 2015-2018 Visyn
// The MIT License(MIT)
// 
// Copyright (c) 2015-2018 Visyn
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Visyn.Types;

namespace Visyn.Mathematics.Types
{
    public class Vector3DConverter : IFieldConverter, IFieldConverterHeader
    {
#if DEBUG
        public string Delimiter { get; set; } = "@";
#else
        public string Delimiter { get; set; } = ",";
#endif

        #region Implementation of IFieldConverter
        public bool CustomNullHandling => true;

        public Type Type => typeof(Vector3D);

        #endregion

        private readonly string[] _columnHeaderPrefix;

        public Vector3DConverter(string columnHeaderPrefix) : 
            this(new[] { $"{columnHeaderPrefix} X", $"{columnHeaderPrefix} Y", $"{columnHeaderPrefix} Z" }) { }

        public Vector3DConverter(string[] columnHeaderPrefixes=null)
        {
            _columnHeaderPrefix = columnHeaderPrefixes?.Length == 3 ? columnHeaderPrefixes : new[] {"X", "Y", "Z"};
        }

        public object StringToField(string text)
        {
            Debug.Assert(!Delimiter.Equals("@"));
            var split = text.Split(new[] { Delimiter },StringSplitOptions.RemoveEmptyEntries);
            Debug.Assert(split.Length % 3 == 0);
            var length = split.Length / 3;
            return length == 1 ? CreateVector(split) : CreateVectors(length, split);
        }

        public string FieldToString(object fieldValue)
        {
            if (fieldValue is Vector3D) return FieldToString((Vector3D)fieldValue);
            var vectors = fieldValue as Vector3D[];
            if (vectors != null)
            {
                return string.Join(Delimiter.ToString(), vectors.Select(FieldToString));
            }
            var list = fieldValue as IList<Vector3D>;
            if (list != null)
            {
                return string.Join(Delimiter.ToString(), list.Select(FieldToString));
            }
            Debug.Assert(false,$"Unhandled conversion type {fieldValue.GetType()}");
            return "";
        }

        private string FieldToString(Vector3D fieldValue) => $"{fieldValue.X}{Delimiter}{fieldValue.Y}{Delimiter}{fieldValue.Z}";


        private static object CreateVectors(int length, string[] split)
        {
            var vectors = new Vector3D[length];
            for (var i = 0; i < split.Length; i += 3)
            {
                vectors[i / 3] = CreateVector(split, i);
            }
            return vectors;
        }

        private static Vector3D CreateVector(string[] split, int offset = 0) 
            => new Vector3D(new[]
                { double.Parse(split[offset]), double.Parse(split[offset + 1]), double.Parse(split[offset + 2])});


        #region Implementation of IFieldConverterHeader
        public string GetHeaderText(string delimiter=null)
        {
            if (string.IsNullOrEmpty(delimiter)) delimiter = Delimiter;
            return string.Join(delimiter, _columnHeaderPrefix);
        }

        #endregion
    }
}
