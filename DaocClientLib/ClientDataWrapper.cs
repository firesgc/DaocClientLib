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
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace DaocClientLib
{
	/// <summary>
	/// Main Class to build a Ressources Tree upon a Dark Age of Camelot setup directory.
	/// </summary>
	public class ClientDataWrapper
	{
		/// <summary>
		/// File loading Filters. (Regex)
		/// </summary>
		private readonly string[] m_fileFilters;
		
		/// <summary>
		/// File loading Filters. (Regex)
		/// </summary>
		public string[] FileFilters { get { return m_fileFilters; } }
		
		/// <summary>
		/// Client Files Detected
		/// </summary>
		private readonly FileInfo[] m_clientFiles;

		/// <summary>
		/// Client Files Detected
		/// </summary>
		public FileInfo[] ClientFiles { get { return m_clientFiles; }}
				
		/// <summary>
		/// Create a Client Data Wrapper from Directory Path
		/// </summary>
		/// <param name="path"></param>
		public ClientDataWrapper(string path)
			: this(new DirectoryInfo(path))
		{
		}

		/// <summary>
		/// Create a Client Data Wrapper from Directory Path and given Regex File Filters
		/// </summary>
		/// <param name="path"></param>
		/// <param name="filters"></param>
		public ClientDataWrapper(string path, string[] filters)
			: this(new DirectoryInfo(path), filters)
		{
		}
		
		/// <summary>
		/// Create a Client Data Wrapper from Directory Path
		/// </summary>
		/// <param name="path"></param>
		public ClientDataWrapper(DirectoryInfo path)
			: this(path, null)
		{
		}
		
		/// <summary>
		/// Create a Client Data Wrapper from Directory Path and given Regex File Filters
		/// </summary>
		/// <param name="path"></param>
		/// <param name="filters"></param>
		public ClientDataWrapper(DirectoryInfo path, string[] filters)
		{
			if (filters != null)
				m_fileFilters = filters;
			else
				m_fileFilters = new []{ @"camelot\.exe", @"game\.dll", @".+\.mpk|.+\.npk", @".+\.nif|.+\.nhd", @".+\.pcx|.+\.tga|.+\.bmp|.+\.dds", @".+\.csv" };
			
			if (path == null)
				throw new ArgumentNullException("path");
						
			if (!path.Exists)
				throw new FileNotFoundException("Could not Find Daoc Client Directory !", path.FullName);
			
			// Filter All files recursively against Regex strings
			m_clientFiles = path.GetFiles("*", SearchOption.AllDirectories)
				.Where(f => m_fileFilters.Any(r => Regex.IsMatch(f.Name, r))).ToArray();
		}
	}
}
