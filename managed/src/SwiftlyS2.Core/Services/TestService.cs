using System.Buffers;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Convars;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
internal delegate nint DispatchSpawnHook(nint entity, nint kv);

internal class TestService {

  private ILogger<TestService> _Logger { get; init; }
  private ProfileService _ProfileService { get; init; }
  private ISwiftlyCore _Core { get; init; }
  public unsafe TestService(
    ILogger<TestService> logger,
    ProfileService profileService,
    ISwiftlyCore core
  ) {
    _ProfileService = profileService;
    _Core = core;
    _Logger = logger;

    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");
    _Logger.LogWarning("TestService created");

    Test();
  }


  public void Test()
  {
    _Core.Command.RegisterCommand("rrr", (context) => {

      var a = _Core.ConVar.Create("test_convar", "Test convar", 123);

      Console.WriteLine(a.Flags.HasFlag(ConvarFlags.DEVELOPMENT_ONLY));

      // setting this to DEVELOPMENT_ONLY will cause a crash later
      // because if its development only, the ConVarRefAbstract will not be able to find the ConvarData
      // eventually return a nullptr in GetConvarData
      a.Flags |= ConvarFlags.REPLICATED;

      Console.WriteLine(a.Flags.HasFlag(ConvarFlags.REPLICATED));
      Console.WriteLine(a.Flags.HasFlag(ConvarFlags.CHEAT));

      Console.WriteLine(a.DefaultValue);
      a.DefaultValue = 234;
      Console.WriteLine(a.DefaultValue);
    });
    // _Core.Event.OnItemServicesCanAcquireHook += (@event) => {
    //   Console.WriteLine(@event.EconItemView.ItemDefinitionIndex);

    //   @event.SetAcquireResult(AcquireResult.NotAllowedByProhibition);
    // };


  }
}