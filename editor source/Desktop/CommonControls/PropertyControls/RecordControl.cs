﻿using System;
using System.Reflection;

namespace Desktop.CommonControls.PropertyControls
{
	public partial class RecordControl : PropertyEditControl
	{
		private string _filterMethodName;

		public RecordControl()
		{
			InitializeComponent();
		}

		protected override void OnSetParameters(EditControlAttribute parameters)
		{
			RecordSelectAttribute attr = parameters as RecordSelectAttribute;
			recField.RecordType = attr.RecordType;
			recField.AllowCreate = attr.AllowCreate;
			recField.UseAutoComplete = attr.UseAutoComplete;
			_filterMethodName = attr.RecordFilter;
		}

		protected override void OnBoundData()
		{
			if (!string.IsNullOrEmpty(_filterMethodName))
			{
				MethodInfo mi = Data.GetType().GetMethod(_filterMethodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				if (mi != null)
				{
					Func<IRecord, bool> filter = (Func<IRecord, bool>)Delegate.CreateDelegate(typeof(Func<IRecord, bool>), Data, mi);
					recField.RecordFilter = filter;
				}
			}

			recField.RecordContext = Context;
			if (DataType == typeof(string))
			{
				recField.RecordKey = GetValue()?.ToString();
			}
			else if (typeof(IRecord).IsAssignableFrom(DataType))
			{
				IRecord record = GetValue() as IRecord;
				recField.Record = record;
			}
		}

		public override void Clear()
		{
			recField.Record = null;
		}

		public override void Save()
		{
			IRecord record = recField.Record;
			if (record == null)
			{
				SetValue(null);
			}
			else
			{
				if (DataType == typeof(string))
				{
					SetValue(record.Key);
				}
				else if (typeof(IRecord).IsAssignableFrom(DataType))
				{
					SetValue(record);
				}
			}
		}

		private void recField_RecordChanged(object sender, IRecord e)
		{
			Save();
		}
	}

	public class RecordSelectAttribute : EditControlAttribute
	{
		/// <summary>
		/// What type of records to read
		/// </summary>
		public Type RecordType { get; set; }

		/// <summary>
		/// Whether new records should be able to be created
		/// </summary>
		public bool AllowCreate { get; set; }

		/// <summary>
		/// Whether the field should display auto-complete items
		/// </summary>
		public bool UseAutoComplete { get; set; }

		/// <summary>
		/// Name of function on data object for the filter to run on records
		/// </summary>
		public string RecordFilter { get; set; }

		public override Type EditControlType
		{
			get { return typeof(RecordControl); }
		}
	}
}