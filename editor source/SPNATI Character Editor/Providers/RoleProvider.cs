﻿using Desktop;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SPNATI_Character_Editor.Providers
{
	public class RoleProvider : IRecordProvider<FilterRole>
	{
		private static List<FilterRole> _roles;

		static RoleProvider()
		{
			_roles = new List<FilterRole>();
			_roles.Add(new FilterRole("target", "Target", "Character must be the current target to count"));
			_roles.Add(new FilterRole("opp", "Opponent", "Only counts opponents (i.e. excludes this character from the count)"));
			_roles.Add(new FilterRole("other", "Also Playing", "Only counts characters that are neither the target nor this character"));
		}

		public string GetLookupCaption()
		{
			return "Role Select";
		}

		public bool AllowsNew
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

		public List<IRecord> GetRecords(string text)
		{
			text = text.ToLower();
			List<IRecord> list = new List<IRecord>();
			foreach (FilterRole record in _roles)
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
			list.Sort((record1, record2) => record1.CompareTo(record2));
		}

		public string[] GetColumns()
		{
			return new string[] { "Name", "Description" };
		}

		public ListViewItem FormatItem(IRecord record)
		{
			FilterRole role = record as FilterRole;
			return new ListViewItem(new string[] { role.Name, role.Description });
		}

		public void SetContext(object context)
		{
		}
	}
}