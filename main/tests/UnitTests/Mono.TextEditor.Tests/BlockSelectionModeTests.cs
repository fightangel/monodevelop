// 
// BlockSelectionModeTests.cs
//  
// Author:
//       Mike Krüger <mkrueger@xamarin.com>
// 
// Copyright (c) 2012 Xamarin Inc. (http://xamarin.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
using System;
using NUnit.Framework;
using System.Linq;

namespace Mono.TextEditor.Tests
{
	[TestFixture()]
	public class BlockSelectionModeTests : TextEditorTestBase
	{
		[Test]
		public void TestInsertAtCaret ()
		{
			var data = Create (
@"1234567890
1234<-567890
1234567890
1234567890
1234->$567890
1234567890");
			data.MainSelection.SelectionMode = SelectionMode.Block;
			data.InsertAtCaret ("hello");

			Check (data, @"1234567890
1234hello<-567890
1234hello567890
1234hello567890
1234hello->$567890
1234567890");

		}

		[Test]
		public void TestEditModeInput ()
		{
			var data = Create (
@"1234567890
1234<-567890
1234567890
1234567890
1234->$567890
1234567890");
			data.MainSelection.SelectionMode = SelectionMode.Block;
			data.CurrentMode = new SimpleEditMode ();
			data.CurrentMode.InternalHandleKeypress (null, data, Gdk.Key.a, (uint)'a', Gdk.ModifierType.None);
			Check (data, @"1234567890
1234a<-567890
1234a567890
1234a567890
1234a->$567890
1234567890");

		}

		[Test]
		public void TestBackspace ()
		{
			var data = Create (
@"1234567890
1234<-567890
1234567890
1234567890
1234->$567890
1234567890");
			data.MainSelection.SelectionMode = SelectionMode.Block;
			DeleteActions.Backspace (data);
			Check (data, @"1234567890
123<-567890
123567890
123567890
123->$567890
1234567890");
		}

		[Test]
		public void TestDelete ()
		{
			var data = Create (
@"1234567890
1234<-567890
1234567890
1234567890
1234->$567890
1234567890");
			data.MainSelection.SelectionMode = SelectionMode.Block;
			DeleteActions.Delete (data);
			Check (data, @"1234567890
1234<-67890
123467890
123467890
1234->$67890
1234567890");
		}

	}
}

