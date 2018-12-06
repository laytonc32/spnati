﻿using Desktop;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class MarkerProvider : IRecordProvider<Marker>
	{
		private Character _character;

		public bool AllowsNew { get { return true; } }

		public void SetContext(object context)
		{
			_character = context as Character;
		}

		public IRecord Create(string key)
		{
			Marker marker = new Marker();
			marker.Key = key;
			if (_character != null)
			{
				if (_character.Markers.Contains(key))
				{
					return _character.Markers.Get(key);	
				}
				_character.Markers.Add(marker);
			}
			return marker;
		}

		public void Delete(IRecord record) { }

		public ListViewItem FormatItem(IRecord record)
		{
			Marker marker = record as Marker;
			return new ListViewItem(new string[] { marker.Name, marker.Description });
		}

		public string[] GetColumns()
		{
			return new string[] { "Name", "Description" };
		}

		public string GetLookupCaption()
		{
			return "Select a Marker";
		}
		public List<IRecord> GetRecords(string text)
		{
			text = text.ToLower();
			var list = new List<IRecord>();

			if (_character == null) { return list; }

			foreach (Marker record in _character.Markers.Values)
			{
				if (record.Key.ToLower().Contains(text) || record.Name.ToLower().Contains(text))
				{
					//partial match
					list.Add(record);
				}
			}
			return list;
		}

		public void Sort(List<IRecord> list)
		{
			list.Sort();
		}
	}
}