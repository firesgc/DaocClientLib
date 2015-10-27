﻿/*
 * DaocClientLib - Dark Age of Camelot Setup Ressources Wrapper
 * 
 * The MIT License (MIT)
 * 
 * Copyright (c) 2015 dol-leodagan
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Linq;
using System.Collections.Generic;

namespace DaocClientLib
{
	/// <summary>
	/// Client Zone Data List
	/// </summary>
	public class ZoneDataList
	{
		/// <summary>
		/// Zone Data Collection
		/// </summary>
		public ZoneData[] Zones { get; protected set; }
		
		/// <summary>
		/// Zone Data List from Parsed DAT Content
		/// </summary>
		/// <param name="content"></param>
		public ZoneDataList(IDictionary<string, IDictionary<string, string>> content)
		{
			Zones = content.Select(kv => new ZoneData(kv.Key, kv.Value)).ToArray();
		}
		
		/// <summary>
		/// Retrieve a ZoneData Collection From a File Bytes Array
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static ZoneData[] ZonesFromFileBytes(byte[] input)
		{
			if (input == null)
				throw new ArgumentNullException("input");
			
			if (input.Length == 0)
				throw new ArgumentException("Input Array is Empty", "input");

			return new ZoneDataList(input.ReadDATFile()).Zones;
		}
	}
}
