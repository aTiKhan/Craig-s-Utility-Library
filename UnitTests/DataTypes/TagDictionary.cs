﻿/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

using Utilities.DataTypes;
using System.Data;

using Utilities.Random;
using Utilities.DataTypes.ExtensionMethods;

namespace UnitTests.DataTypes
{
    public class TagDictionary
    {
        [Fact]
        public void BasicTest()
        {
            TagDictionary<string, string> TestObject = new TagDictionary<string, string>();
            10.Times(x => TestObject.Add("Object" + x, (x + 1).Times(y => "Key" + y).ToArray()));
            11.Times(x => Assert.Equal(10 - x, TestObject["Key" + x].Count()));
            Assert.Equal(10, TestObject["Key0"].Count());
            TestObject.Remove("Key0");
            Assert.Equal(0, TestObject["Key0"].Count());
            11.Times(x => Assert.Equal(0, TestObject["Key" + x].Count()));
        }
    }
}