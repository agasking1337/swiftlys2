using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Core.Extensions;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Core.Convars;

internal delegate void ConVarCallbackDelegate(int playerId, nint name, nint value);

internal class ConVar<T> : IConVar<T>{

  private Dictionary<int, ConVarCallbackDelegate> _callbacks = new();
  private object _lock = new();

  private nint _minValuePtrPtr => NativeConvars.GetMinValuePtrPtr(Name);
  private nint _maxValuePtrPtr => NativeConvars.GetMaxValuePtrPtr(Name);

  public EConVarType Type => (EConVarType)NativeConvars.GetConvarType(Name);

  private bool IsValidType => Type > EConVarType.EConVarType_Invalid && Type < EConVarType.EConVarType_MAX;

  // im not sure
  private bool IsMinMaxType => IsValidType && Type != EConVarType.EConVarType_String && Type != EConVarType.EConVarType_Color;

  public T MinValue { 
    get => GetMinValue();
    set => SetMinValue(value);
  }
  public T MaxValue { 
    get => GetMaxValue();
    set => SetMaxValue(value);
  }

  public T DefaultValue {
    get => GetDefaultValue();
    set => SetDefaultValue(value);
  }

  public ConvarFlags Flags {
    get => (ConvarFlags)NativeConvars.GetFlags(Name);
    set => NativeConvars.SetFlags(Name, (ulong)value);
  }

  public bool HasDefaultValue => NativeConvars.HasDefaultValue(Name);

  public bool HasMinValue => _minValuePtrPtr.Read<nint>() != 0;
  public bool HasMaxValue => _maxValuePtrPtr.Read<nint>() != 0;

  public string Name { get; set; }

  internal ConVar(string name) {
    Name = name;

    ValidateType();
  }

  public void ValidateType()
  {
    if (
      (typeof(T) == typeof(bool) && Type != EConVarType.EConVarType_Bool) ||
      (typeof(T) == typeof(short) && Type != EConVarType.EConVarType_Int16) ||
      (typeof(T) == typeof(ushort) && Type != EConVarType.EConVarType_UInt16) ||
      (typeof(T) == typeof(int) && Type != EConVarType.EConVarType_Int32) ||
      (typeof(T) == typeof(uint) && Type != EConVarType.EConVarType_UInt32) ||
      (typeof(T) == typeof(float) && Type != EConVarType.EConVarType_Float32) ||
      (typeof(T) == typeof(long) && Type != EConVarType.EConVarType_Int64) ||
      (typeof(T) == typeof(ulong) && Type != EConVarType.EConVarType_UInt64) ||
      (typeof(T) == typeof(double) && Type != EConVarType.EConVarType_Float64) ||
      (typeof(T) == typeof(Color) && Type != EConVarType.EConVarType_Color) ||
      (typeof(T) == typeof(QAngle) && Type != EConVarType.EConVarType_Qangle) ||
      (typeof(T) == typeof(Vector) && Type != EConVarType.EConVarType_Vector3) ||
      (typeof(T) == typeof(Vector2D) && Type != EConVarType.EConVarType_Vector2) ||
      (typeof(T) == typeof(Vector4D) && Type != EConVarType.EConVarType_Vector4) ||
      (typeof(T) == typeof(string) && Type != EConVarType.EConVarType_String)
    ) {
      throw new Exception($"Type mismatch for convar {Name}. The real type is {Type}.");
    }
  }

  public T Value {
    get {
      if (typeof(T) == typeof(bool)) {
        return (T)(object)NativeConvars.GetConvarValueBool(Name);
      }
      else if (typeof(T) == typeof(short)) {
        return (T)(object)NativeConvars.GetConvarValueInt16(Name);
      }
      else if (typeof(T) == typeof(ushort)) {
        return (T)(object)NativeConvars.GetConvarValueUInt16(Name);
      }
      else if (typeof(T) == typeof(int)) {
        return (T)(object)NativeConvars.GetConvarValueInt32(Name);
      }
      else if (typeof(T) == typeof(uint)) {
        return (T)(object)NativeConvars.GetConvarValueUInt32(Name);
      }
      else if (typeof(T) == typeof(float)) {
        return (T)(object)NativeConvars.GetConvarValueFloat(Name);
      }
      else if (typeof(T) == typeof(long)) {
        return (T)(object)NativeConvars.GetConvarValueInt64(Name);
      }
      else if (typeof(T) == typeof(ulong)) {
        return (T)(object)NativeConvars.GetConvarValueUInt64(Name);
      }
      else if (typeof(T) == typeof(double)) {
        return (T)(object)NativeConvars.GetConvarValueDouble(Name);
      }
      else if (typeof(T) == typeof(Color)) {
        return (T)(object)NativeConvars.GetConvarValueColor(Name);
      }
      else if (typeof(T) == typeof(QAngle)) {
        return (T)(object)NativeConvars.GetConvarValueQAngle(Name);
      }
      else if (typeof(T) == typeof(Vector)) {
        return (T)(object)NativeConvars.GetConvarValueVector(Name);
      }
      else if (typeof(T) == typeof(Vector2D)) {
        return (T)(object)NativeConvars.GetConvarValueVector2D(Name);
      }
      else if (typeof(T) == typeof(Vector4D)) {
        return (T)(object)NativeConvars.GetConvarValueVector4D(Name);
      }
      else if (typeof(T) == typeof(string)) {
        return (T)(object)NativeConvars.GetConvarValueString(Name);
      }
      throw new ArgumentException($"Invalid type {typeof(T).Name}");
    }
    set {
      if (value is bool boolValue) {
        NativeConvars.SetConvarValueBool(Name, boolValue);
        return;
      }
      else if (value is short shortValue) {
        NativeConvars.SetConvarValueInt16(Name, shortValue);
        return;
      }
      else if (value is ushort ushortValue) {
        NativeConvars.SetConvarValueUInt16(Name, ushortValue);
        return;
      }
      else if (value is int intValue) {
        NativeConvars.SetConvarValueInt32(Name, intValue);
        return;
      }
      else if (value is uint uintValue) {
        NativeConvars.SetConvarValueUInt32(Name, uintValue);
        return;
      }
      else if (value is float floatValue) {
        NativeConvars.SetConvarValueFloat(Name, floatValue);
        return;
      }
      else if (value is long longValue) {
        NativeConvars.SetConvarValueInt64(Name, longValue);
        return;
      }
      else if (value is ulong ulongValue) {
        NativeConvars.SetConvarValueUInt64(Name, ulongValue);
        return;
      }
      else if (value is double doubleValue) {
        NativeConvars.SetConvarValueDouble(Name, doubleValue);
        return;
      }
      else if (value is Color colorValue) {
        NativeConvars.SetConvarValueColor(Name, colorValue);
        return;
      }
      else if (value is QAngle qAngleValue) {
        NativeConvars.SetConvarValueQAngle(Name, qAngleValue);
        return;
      }
      else if (value is Vector vectorValue) {
        NativeConvars.SetConvarValueVector(Name, vectorValue);
        return;
      }
      else if (value is Vector2D vector2DValue) {
        NativeConvars.SetConvarValueVector2D(Name, vector2DValue);
        return;
      }
      else if (value is Vector4D vector4DValue) {
        NativeConvars.SetConvarValueVector4D(Name, vector4DValue);
        return;
      }
      else if (value is string stringValue) {
        NativeConvars.SetConvarValueString(Name, stringValue);
        return;
      }
      throw new ArgumentException($"Invalid type {typeof(T).Name}");
    }
  }

  public void SetInternal(T value)
  {
    var addr = NativeConvars.GetConvarDataAddress(Name);

    if (addr == nint.Zero) {
      throw new Exception($"Convar {Name} not found.");
    }

    if (value is bool boolValue) {
      addr.Write(boolValue);
      return;
    }
    else if (value is short shortValue) {
      addr.Write(shortValue);
      return;
    }
    else if (value is ushort ushortValue) {
      addr.Write(ushortValue);
      return;
    }
    else if (value is int intValue) {
      addr.Write(intValue);
      return;
    } 
    else if (value is float floatValue) {
      addr.Write(floatValue);
      return;
    }
    else if (value is long longValue) {
      addr.Write(longValue);
      return;
    }
    else if (value is ulong ulongValue) {
      addr.Write(ulongValue);
      return;
    }
    else if (value is double doubleValue) {
      addr.Write(doubleValue);
      return;
    }
    else if (value is Color colorValue) {
      addr.Write(colorValue);
      return;
    }
    else if (value is QAngle qAngleValue) {
      addr.Write(qAngleValue);
      return;
    }
    else if (value is Vector vectorValue) {
      addr.Write(vectorValue);
      return;
    }
    else if (value is Vector2D vector2DValue) {
      addr.Write(vector2DValue);
      return;
    }
    else if (value is Vector4D vector4DValue) {
      addr.Write(vector4DValue);
      return;
    }
    else if (value is string stringValue)
    {
      var ptr = StringPool.Allocate(stringValue);
      addr.Write(ptr);
      return;
    }
    throw new ArgumentException($"Invalid type {typeof(T).Name}");
  }

  public void ReplicateToClient(int clientId, T value)
  {
    if (value is bool boolValue) {
      NativeConvars.SetClientConvarValueBool(clientId, Name, boolValue);
      return;
    }
    else if (value is short shortValue) {
      NativeConvars.SetClientConvarValueInt16(clientId, Name, shortValue);
      return;
    }
    else if (value is ushort ushortValue) {
      NativeConvars.SetClientConvarValueUInt16(clientId, Name, ushortValue);
      return;
    }
    else if (value is int intValue) {
      NativeConvars.SetClientConvarValueInt32(clientId, Name, intValue);
      return;
    }
    else if (value is uint uintValue) {
      NativeConvars.SetClientConvarValueUInt32(clientId, Name, uintValue);
      return;
    }
    else if (value is float floatValue) {
      NativeConvars.SetClientConvarValueFloat(clientId, Name, floatValue);
      return;
    }
    else if (value is long longValue) {
      NativeConvars.SetClientConvarValueInt64(clientId, Name, longValue);
      return;
    }
    else if (value is ulong ulongValue) {
      NativeConvars.SetClientConvarValueUInt64(clientId, Name, ulongValue);
      return;
    }
    else if (value is double doubleValue) {
      NativeConvars.SetClientConvarValueDouble(clientId, Name, doubleValue);
      return;
    }
    else if (value is Color colorValue) {
      NativeConvars.SetClientConvarValueColor(clientId, Name, colorValue);
      return;
    }
    else if (value is QAngle qAngleValue) {
      NativeConvars.SetClientConvarValueQAngle(clientId, Name, qAngleValue);
      return;
    }
    else if (value is Vector vectorValue) {
      NativeConvars.SetClientConvarValueVector(clientId, Name, vectorValue);
      return;
    }
    else if (value is Vector2D vector2DValue) {
      NativeConvars.SetClientConvarValueVector2D(clientId, Name, vector2DValue);
      return;
    }
    else if (value is Vector4D vector4DValue) {
      NativeConvars.SetClientConvarValueVector4D(clientId, Name, vector4DValue);
      return;
    }
    else if (value is string stringValue) {
      NativeConvars.SetClientConvarValueString(clientId, Name, stringValue);
      return;
    }
    throw new ArgumentException($"Invalid type {typeof(T).Name}");
  }

  public void QueryClient(int clientId, Action<string> callback)
  {

    Action? removeSelf = null;
    ConVarCallbackDelegate nativeCallback = (playerId, namePtr, valuePtr) =>
    {
      if (clientId != playerId) return;
      var name = Marshal.PtrToStringAnsi(namePtr);
      if (name != Name) return;
      var value = Marshal.PtrToStringAnsi(valuePtr)!;

      // var convertedValue = (T)Convert.ChangeType(value, typeof(T))!;
      callback(value);
      if(removeSelf != null) removeSelf();
    };


    var callbackPtr = Marshal.GetFunctionPointerForDelegate(nativeCallback);

    var listenerId = NativeConvars.AddQueryClientCvarCallback(callbackPtr);
    lock (_lock)
    {
      _callbacks[listenerId] = nativeCallback;
    }

    removeSelf = () => {
      lock (_lock) {
        _callbacks.Remove(listenerId);
        NativeConvars.RemoveQueryClientCvarCallback(listenerId);
      }
    };
    
    NativeConvars.QueryClientConvar(clientId, Name);
  }

  public T GetMinValue()
  {
    if (!IsMinMaxType) {
      throw new Exception($"Convar {Name} is not a min/max type.");
    }
    if (!HasMinValue) {
      throw new Exception($"Convar {Name} doesn't have a min value.");
    }
    unsafe {
      return **(T**)_minValuePtrPtr;
    }
  }

  public T GetMaxValue()
  {
    if (!IsMinMaxType) {
      throw new Exception($"Convar {Name} is not a min/max type.");
    }
    if (!HasMaxValue) {
      throw new Exception($"Convar {Name} doesn't have a max value.");
    }
    unsafe {
      return **(T**)_maxValuePtrPtr;
    }
  }
  public void SetMinValue(T minValue)
  {
    if (!IsMinMaxType) {
      throw new Exception($"Convar {Name} is not a min/max type.");
    }
    unsafe
    {
      if (_minValuePtrPtr.Read<nint>() == nint.Zero) {
        _minValuePtrPtr.Write(NativeAllocator.Alloc(16));
      }
      **(T**)_minValuePtrPtr = minValue;
    }
  }

  public void SetMaxValue(T maxValue)
  {
    if (!IsMinMaxType) {
      throw new Exception($"Convar {Name} is not a min/max type.");
    }
    unsafe
    {
      if (_maxValuePtrPtr.Read<nint>() == nint.Zero)
      {
        _maxValuePtrPtr.Write(NativeAllocator.Alloc(16));
      }
      **(T**)_maxValuePtrPtr = maxValue;
    }
  }

  public T GetDefaultValue()
  {
    unsafe {
      var ptr = NativeConvars.GetDefaultValuePtr(Name);
      if (ptr == nint.Zero) {
        throw new Exception($"Convar {Name} doesn't have a default value.");
      }
      if (Type != EConVarType.EConVarType_String) {
        return *(T*)ptr;
      }
      else {
        return (T)(object)(*(CUtlString*)ptr).Value;
      }
    }
  }

  public void SetDefaultValue(T defaultValue)
  {
    unsafe {
      var ptr = NativeConvars.GetDefaultValuePtr(Name);
      if (ptr == nint.Zero) {
        throw new Exception($"Convar {Name} doesn't have a default value.");
      }
      if (Type != EConVarType.EConVarType_String) {
        *(T*)NativeConvars.GetDefaultValuePtr(Name) = defaultValue;
      }
      else {
        NativeConvars.GetDefaultValuePtr(Name).Write(StringPool.Allocate((string)(object)defaultValue));
      }
    }
  }
}
