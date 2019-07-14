﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Desktop.Providers
{
	public abstract class CategoryProvider<T> : IRecordProvider<T> where T : Category
	{
		private T[] _categoryValues;

		public bool AllowsNew
		{
			get { return false; }
		}

		public bool TrackRecent
		{
			get { return false; }
		}

		public IRecord Create(string key)
		{
			throw new NotImplementedException();
		}

		public void Delete(IRecord record)
		{
			throw new NotImplementedException();
		}

		public ListViewItem FormatItem(IRecord record)
		{
			ListViewItem item = new ListViewItem(new string[] { record.Key, record.Name });
			return item;
		}

		public string[] GetColumns()
		{
			return new string[] { "Key", "Value" };
		}

		public abstract string GetLookupCaption();

		protected abstract T[] GetCategoryValues();

		public List<IRecord> GetRecords(string text)
		{
			if (_categoryValues == null)
			{
				_categoryValues = GetCategoryValues();
			}

			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (T category in _categoryValues)
			{
				if (category.Key.ToLower().Contains(text) || category.Name.ToLower().Contains(text))
				{
					list.Add(category);
				}
			}
			return list;
		}

		public void SetContext(object context)
		{
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}
}