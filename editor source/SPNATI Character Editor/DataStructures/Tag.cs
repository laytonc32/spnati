﻿using Desktop;
using System;
using System.Xml.Serialization;

namespace SPNATI_Character_Editor
{
	public class Tag : IRecord, IComparable<Tag>
	{
		[XmlIgnore]
		public string Name { get { return DisplayName; } }
		[XmlIgnore]
		public string Key { get { return Value; } set { Value = value; } }
		[XmlIgnore]
		public string Group { get; }

		[XmlText]
		public string Value;

		[XmlAttribute("display")]
		public string DisplayName;

		[XmlAttribute("description")]
		public string Description;

		[XmlAttribute("paired")]
		public string PairedTag;

		[XmlAttribute("gender")]
		public string Gender;

		[XmlIgnore]
		public int Count;

		public string ToLookupString()
		{
			return string.Format("{0} [{1}]", DisplayName ?? Value, Value);
		}

		public override string ToString()
		{
			return string.Format("{0} ({1})", DisplayName ?? Value, Count);
		}

		public int CompareTo(Tag other)
		{
			return Value.CompareTo(other.Value);
		}

		public int CompareTo(IRecord other)
		{
			return Name.CompareTo(other.Name);
		}
	}
}