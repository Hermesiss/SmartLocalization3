// Copyright (c) 2012-2013 Rotorz Limited. All rights reserved.
// Use of this source code is governed by a BSD-style license that can be
// found in the LICENSE file.

using UnityEngine;
using UnityEditor;

using System;
using System.Reflection;

namespace SmartLocalization.ReorderableList.Internal {

	/// <summary>
	/// Utility functions to assist with GUIs.
	/// </summary>
	internal static class GUIHelper {

		static GUIHelper() {
			var tyGUIClip = typeof(GUI).Assembly.GetType("UnityEngine.GUIClip");
			if (tyGUIClip != null) {
				var piVisibleRect = tyGUIClip.GetProperty("visibleRect", BindingFlags.Static | BindingFlags.NonPublic);
				if (piVisibleRect != null)
					VisibleRect = (Func<Rect>)Delegate.CreateDelegate(typeof(Func<Rect>), piVisibleRect.GetGetMethod(true));
			}
			
			var miFocusTextInControl = typeof(EditorGUI).GetMethod("FocusTextInControl", BindingFlags.Static | BindingFlags.Public);
			if (miFocusTextInControl == null)
				miFocusTextInControl = typeof(GUI).GetMethod("FocusControl", BindingFlags.Static | BindingFlags.Public);

			FocusTextInControl = (Action<string>)Delegate.CreateDelegate(typeof(Action<string>), miFocusTextInControl);
		}

		/// <summary>
		/// Gets visible rectangle within GUI.
		/// </summary>
		/// <remarks>
		/// <para>VisibleRect = TopmostRect + scrollViewOffsets</para>
		/// </remarks>
		public static readonly Func<Rect> VisibleRect;

		/// <summary>
		/// Focus control and text editor where applicable.
		/// </summary>
		public static readonly Action<string> FocusTextInControl;

	}

}