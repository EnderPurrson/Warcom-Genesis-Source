using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TNetSdk
{
	public class SFSObject : ISFSObject
	{
		private Dictionary<string, SFSDataWrapper> dataHolder;

		private ISFSDataSerializer serializer;

		public SFSObject()
		{
			dataHolder = new Dictionary<string, SFSDataWrapper>();
			serializer = DefaultSFSDataSerializer.Instance;
		}

		public static SFSObject NewFromObject(object o)
		{
			throw new NotImplementedException();
		}

		public static SFSObject NewFromBinaryData(ByteArray ba)
		{
			return DefaultSFSDataSerializer.Instance.Binary2Object(ba) as SFSObject;
		}

		public static SFSObject NewInstance()
		{
			return new SFSObject();
		}

		private string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Convert.ToString(DefaultObjectDumpFormatter.TOKEN_INDENT_OPEN));
			foreach (KeyValuePair<string, SFSDataWrapper> item in dataHolder)
			{
				SFSDataWrapper value = item.Value;
				string key = item.Key;
				int type = value.Type;
				stringBuilder.Append("(" + ((SFSDataType)type).ToString().ToLower() + ")");
				stringBuilder.Append(" " + key + ": ");
				switch (type)
				{
				case 18:
					stringBuilder.Append((value.Data as SFSObject).GetDump(false));
					break;
				case 17:
					stringBuilder.Append((value.Data as SFSArray).GetDump(false));
					break;
				case 9:
				case 10:
				case 11:
				case 12:
				case 13:
				case 14:
				case 15:
				case 16:
					stringBuilder.Append(string.Concat("[", value.Data, "]"));
					break;
				default:
					stringBuilder.Append(value.Data);
					break;
				}
				stringBuilder.Append(DefaultObjectDumpFormatter.TOKEN_DIVIDER);
			}
			string text = stringBuilder.ToString();
			if (Size() > 0)
			{
				text = text.Substring(0, text.Length - 1);
			}
			return text + DefaultObjectDumpFormatter.TOKEN_INDENT_CLOSE;
		}

		public SFSDataWrapper GetData(string key)
		{
			return dataHolder[key];
		}

		public T GetValue<T>(string key)
		{
			if (!dataHolder.ContainsKey(key))
			{
				return default(T);
			}
			return (T)dataHolder[key].Data;
		}

		public bool GetBool(string key)
		{
			return GetValue<bool>(key);
		}

		public byte GetByte(string key)
		{
			return GetValue<byte>(key);
		}

		public short GetShort(string key)
		{
			return GetValue<short>(key);
		}

		public int GetInt(string key)
		{
			return GetValue<int>(key);
		}

		public long GetLong(string key)
		{
			return GetValue<long>(key);
		}

		public float GetFloat(string key)
		{
			return GetValue<float>(key);
		}

		public double GetDouble(string key)
		{
			return GetValue<double>(key);
		}

		public string GetUtfString(string key)
		{
			return GetValue<string>(key);
		}

		private ICollection GetArray(string key)
		{
			return GetValue<ICollection>(key);
		}

		public bool[] GetBoolArray(string key)
		{
			return (bool[])GetArray(key);
		}

		public ByteArray GetByteArray(string key)
		{
			return GetValue<ByteArray>(key);
		}

		public short[] GetShortArray(string key)
		{
			return (short[])GetArray(key);
		}

		public int[] GetIntArray(string key)
		{
			return (int[])GetArray(key);
		}

		public long[] GetLongArray(string key)
		{
			return (long[])GetArray(key);
		}

		public float[] GetFloatArray(string key)
		{
			return (float[])GetArray(key);
		}

		public double[] GetDoubleArray(string key)
		{
			return (double[])GetArray(key);
		}

		public string[] GetUtfStringArray(string key)
		{
			return (string[])GetArray(key);
		}

		public ISFSArray GetSFSArray(string key)
		{
			return GetValue<ISFSArray>(key);
		}

		public ISFSObject GetSFSObject(string key)
		{
			return GetValue<ISFSObject>(key);
		}

		public void PutNull(string key)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.NULL, null);
		}

		public void PutBool(string key, bool val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.BOOL, val);
		}

		public void PutByte(string key, byte val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.BYTE, val);
		}

		public void PutShort(string key, short val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.SHORT, val);
		}

		public void PutInt(string key, int val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.INT, val);
		}

		public void PutLong(string key, long val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.LONG, val);
		}

		public void PutFloat(string key, float val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.FLOAT, val);
		}

		public void PutDouble(string key, double val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.DOUBLE, val);
		}

		public void PutUtfString(string key, string val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.UTF_STRING, val);
		}

		public void PutBoolArray(string key, bool[] val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.BOOL_ARRAY, val);
		}

		public void PutByteArray(string key, ByteArray val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.BYTE_ARRAY, val);
		}

		public void PutShortArray(string key, short[] val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.SHORT_ARRAY, val);
		}

		public void PutIntArray(string key, int[] val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.INT_ARRAY, val);
		}

		public void PutLongArray(string key, long[] val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.LONG_ARRAY, val);
		}

		public void PutFloatArray(string key, float[] val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.FLOAT_ARRAY, val);
		}

		public void PutDoubleArray(string key, double[] val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.DOUBLE_ARRAY, val);
		}

		public void PutUtfStringArray(string key, string[] val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.UTF_STRING_ARRAY, val);
		}

		public void PutSFSArray(string key, ISFSArray val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.SFS_ARRAY, val);
		}

		public void PutSFSObject(string key, ISFSObject val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.SFS_OBJECT, val);
		}

		public void Put(string key, SFSDataWrapper val)
		{
			dataHolder[key] = val;
		}

		public bool ContainsKey(string key)
		{
			return dataHolder.ContainsKey(key);
		}

		public object GetClass(string key)
		{
			SFSDataWrapper sFSDataWrapper = dataHolder[key];
			if (sFSDataWrapper != null)
			{
				return sFSDataWrapper.Data;
			}
			return null;
		}

		public string GetDump(bool format)
		{
			if (!format)
			{
				return Dump();
			}
			return DefaultObjectDumpFormatter.PrettyPrintDump(Dump());
		}

		public string GetDump()
		{
			return GetDump(true);
		}

		public string GetHexDump()
		{
			return DefaultObjectDumpFormatter.HexDump(ToBinary());
		}

		public string[] GetKeys()
		{
			string[] array = new string[dataHolder.Keys.Count];
			dataHolder.Keys.CopyTo(array, 0);
			return array;
		}

		public bool IsNull(string key)
		{
			return !ContainsKey(key) || GetData(key).Data == null;
		}

		public void PutClass(string key, object val)
		{
			dataHolder[key] = new SFSDataWrapper(SFSDataType.CLASS, val);
		}

		public void RemoveElement(string key)
		{
			dataHolder.Remove(key);
		}

		public int Size()
		{
			return dataHolder.Count;
		}

		public ByteArray ToBinary()
		{
			return serializer.Object2Binary(this);
		}
	}
}
