; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [333 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [660 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 10266594, ; 2: LiveChartsCore.SkiaSharpView.dll => 0x9ca7e2 => 182
	i32 15721112, ; 3: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 32687329, ; 4: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 252
	i32 34715100, ; 5: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 286
	i32 34839235, ; 6: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 38195423, ; 7: BeursfuifChiroZevendonk.dll => 0x246d0df => 0
	i32 38948123, ; 8: ar\Microsoft.Maui.Controls.resources.dll => 0x2524d1b => 295
	i32 39485524, ; 9: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 42244203, ; 10: he\Microsoft.Maui.Controls.resources.dll => 0x284986b => 304
	i32 42639949, ; 11: System.Threading.Thread => 0x28aa24d => 145
	i32 66541672, ; 12: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 67008169, ; 13: zh-Hant\Microsoft.Maui.Controls.resources => 0x3fe76a9 => 328
	i32 68219467, ; 14: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 72070932, ; 15: Microsoft.Maui.Graphics.dll => 0x44bb714 => 198
	i32 82292897, ; 16: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 83839681, ; 17: ms\Microsoft.Maui.Controls.resources.dll => 0x4ff4ac1 => 312
	i32 101534019, ; 18: Xamarin.AndroidX.SlidingPaneLayout => 0x60d4943 => 270
	i32 117431740, ; 19: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 120558881, ; 20: Xamarin.AndroidX.SlidingPaneLayout.dll => 0x72f9521 => 270
	i32 122350210, ; 21: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 134690465, ; 22: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 290
	i32 136584136, ; 23: zh-Hans\Microsoft.Maui.Controls.resources.dll => 0x8241bc8 => 327
	i32 140062828, ; 24: sk\Microsoft.Maui.Controls.resources.dll => 0x859306c => 320
	i32 142721839, ; 25: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 26: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 27: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 28: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 226
	i32 176265551, ; 29: System.ServiceProcess => 0xa81994f => 132
	i32 182336117, ; 30: Xamarin.AndroidX.SwipeRefreshLayout.dll => 0xade3a75 => 272
	i32 184328833, ; 31: System.ValueTuple.dll => 0xafca281 => 151
	i32 205061960, ; 32: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 33: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 224
	i32 220171995, ; 34: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 35: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 246
	i32 230752869, ; 36: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 37: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 38: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 39: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 261689757, ; 40: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 229
	i32 276479776, ; 41: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 278686392, ; 42: Xamarin.AndroidX.Lifecycle.LiveData.dll => 0x109c6ab8 => 248
	i32 280482487, ; 43: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 245
	i32 291076382, ; 44: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 298918909, ; 45: System.Net.Ping.dll => 0x11d123fd => 69
	i32 317674968, ; 46: vi\Microsoft.Maui.Controls.resources => 0x12ef55d8 => 325
	i32 318968648, ; 47: Xamarin.AndroidX.Activity.dll => 0x13031348 => 215
	i32 321597661, ; 48: System.Numerics => 0x132b30dd => 83
	i32 321963286, ; 49: fr\Microsoft.Maui.Controls.resources.dll => 0x1330c516 => 303
	i32 331603304, ; 50: SixLabors.Fonts => 0x13c3dd68 => 202
	i32 342366114, ; 51: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 247
	i32 360082299, ; 52: System.ServiceModel.Web => 0x15766b7b => 131
	i32 367780167, ; 53: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 54: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 55: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 56: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 57: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 58: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395744057, ; 59: _Microsoft.Android.Resource.Designer => 0x17969339 => 329
	i32 403441872, ; 60: WindowsBase => 0x180c08d0 => 165
	i32 409257351, ; 61: tr\Microsoft.Maui.Controls.resources.dll => 0x1864c587 => 323
	i32 441335492, ; 62: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 230
	i32 442565967, ; 63: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 64: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 243
	i32 451504562, ; 65: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 66: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 67: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 68: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 69: System.dll => 0x1bff388e => 164
	i32 476646585, ; 70: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 245
	i32 486930444, ; 71: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 258
	i32 489220957, ; 72: es\Microsoft.Maui.Controls.resources.dll => 0x1d28eb5d => 301
	i32 498788369, ; 73: System.ObjectModel => 0x1dbae811 => 84
	i32 513247710, ; 74: Microsoft.Extensions.Primitives.dll => 0x1e9789de => 192
	i32 525008092, ; 75: SkiaSharp.dll => 0x1f4afcdc => 203
	i32 526420162, ; 76: System.Transactions.dll => 0x1f6088c2 => 150
	i32 527452488, ; 77: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 290
	i32 530272170, ; 78: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 538707440, ; 79: th\Microsoft.Maui.Controls.resources.dll => 0x201c05f0 => 322
	i32 539058512, ; 80: Microsoft.Extensions.Logging => 0x20216150 => 188
	i32 540030774, ; 81: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 82: System.Runtime.Extensions => 0x2080b118 => 103
	i32 546455878, ; 83: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 549171840, ; 84: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 85: Jsr305Binding => 0x213954e7 => 283
	i32 569601784, ; 86: Xamarin.AndroidX.Window.Extensions.Core.Core => 0x21f36ef8 => 281
	i32 577335427, ; 87: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 601371474, ; 88: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 89: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 610194910, ; 90: System.Reactive.dll => 0x245ed5de => 210
	i32 613668793, ; 91: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 627609679, ; 92: Xamarin.AndroidX.CustomView => 0x2568904f => 235
	i32 627931235, ; 93: nl\Microsoft.Maui.Controls.resources => 0x256d7863 => 314
	i32 639843206, ; 94: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 241
	i32 643868501, ; 95: System.Net => 0x2660a755 => 81
	i32 662205335, ; 96: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 97: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 277
	i32 666292255, ; 98: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 222
	i32 672442732, ; 99: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 100: System.Net.Security => 0x28bdabca => 73
	i32 690569205, ; 101: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 102: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 292
	i32 693804605, ; 103: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 104: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 105: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 287
	i32 700358131, ; 106: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 720511267, ; 107: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 291
	i32 722857257, ; 108: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 735137430, ; 109: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 736260964, ; 110: LiveChartsCore.Behaviours => 0x2be27364 => 181
	i32 752232764, ; 111: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 755313932, ; 112: Xamarin.Android.Glide.Annotations.dll => 0x2d052d0c => 212
	i32 759454413, ; 113: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 114: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 115: System.IO.Compression => 0x2e394f87 => 46
	i32 777317022, ; 116: sk\Microsoft.Maui.Controls.resources => 0x2e54ea9e => 320
	i32 778756650, ; 117: SkiaSharp.HarfBuzz.dll => 0x2e6ae22a => 204
	i32 789151979, ; 118: Microsoft.Extensions.Options => 0x2f0980eb => 191
	i32 790371945, ; 119: Xamarin.AndroidX.CustomView.PoolingContainer.dll => 0x2f1c1e69 => 236
	i32 804715423, ; 120: System.Data.Common => 0x2ff6fb9f => 22
	i32 807930345, ; 121: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx.dll => 0x302809e9 => 250
	i32 823281589, ; 122: System.Private.Uri.dll => 0x311247b5 => 86
	i32 830298997, ; 123: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 124: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 125: System.Net.Quic => 0x31b69d60 => 71
	i32 843511501, ; 126: Xamarin.AndroidX.Print => 0x3246f6cd => 263
	i32 869139383, ; 127: hi\Microsoft.Maui.Controls.resources.dll => 0x33ce03b7 => 305
	i32 873119928, ; 128: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 129: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 130: System.Net.Http.Json => 0x3463c971 => 63
	i32 880668424, ; 131: ru\Microsoft.Maui.Controls.resources.dll => 0x347def08 => 319
	i32 904024072, ; 132: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 133: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 918734561, ; 134: pt-BR\Microsoft.Maui.Controls.resources.dll => 0x36c2c6e1 => 316
	i32 928116545, ; 135: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 286
	i32 939938625, ; 136: SharpHook.dll => 0x38065341 => 200
	i32 952186615, ; 137: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 956575887, ; 138: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 291
	i32 961460050, ; 139: it\Microsoft.Maui.Controls.resources.dll => 0x394eb752 => 309
	i32 966729478, ; 140: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 284
	i32 967690846, ; 141: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 247
	i32 975236339, ; 142: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 143: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 144: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 145: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 990727110, ; 146: ro\Microsoft.Maui.Controls.resources.dll => 0x3b0d4bc6 => 318
	i32 992768348, ; 147: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 148: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 1001831731, ; 149: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1012816738, ; 150: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 267
	i32 1019214401, ; 151: System.Drawing => 0x3cbffa41 => 36
	i32 1028951442, ; 152: Microsoft.Extensions.DependencyInjection.Abstractions => 0x3d548d92 => 187
	i32 1031528504, ; 153: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 285
	i32 1034083993, ; 154: LiveChartsCore.SkiaSharpView.Maui.dll => 0x3da2de99 => 183
	i32 1035644815, ; 155: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 220
	i32 1036536393, ; 156: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1043950537, ; 157: de\Microsoft.Maui.Controls.resources.dll => 0x3e396bc9 => 299
	i32 1044663988, ; 158: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1052210849, ; 159: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 254
	i32 1067306892, ; 160: GoogleGson => 0x3f9dcf8c => 177
	i32 1082857460, ; 161: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1083751839, ; 162: System.IO.Packaging => 0x4098bd9f => 209
	i32 1084122840, ; 163: Xamarin.Kotlin.StdLib => 0x409e66d8 => 288
	i32 1098259244, ; 164: System => 0x41761b2c => 164
	i32 1108272742, ; 165: sv\Microsoft.Maui.Controls.resources.dll => 0x420ee666 => 321
	i32 1117529484, ; 166: pl\Microsoft.Maui.Controls.resources.dll => 0x429c258c => 315
	i32 1118262833, ; 167: ko\Microsoft.Maui.Controls.resources => 0x42a75631 => 311
	i32 1121599056, ; 168: Xamarin.AndroidX.Lifecycle.Runtime.Ktx.dll => 0x42da3e50 => 253
	i32 1127624469, ; 169: Microsoft.Extensions.Logging.Debug => 0x43362f15 => 190
	i32 1149092582, ; 170: Xamarin.AndroidX.Window => 0x447dc2e6 => 280
	i32 1168523401, ; 171: pt\Microsoft.Maui.Controls.resources => 0x45a64089 => 317
	i32 1170634674, ; 172: System.Web.dll => 0x45c677b2 => 153
	i32 1175144683, ; 173: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 276
	i32 1178241025, ; 174: Xamarin.AndroidX.Navigation.Runtime.dll => 0x463a8801 => 261
	i32 1204270330, ; 175: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 222
	i32 1208641965, ; 176: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1214827643, ; 177: CommunityToolkit.Mvvm => 0x4868cc7b => 174
	i32 1219128291, ; 178: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1243150071, ; 179: Xamarin.AndroidX.Window.Extensions.Core.Core.dll => 0x4a18f6f7 => 281
	i32 1253011324, ; 180: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1260983243, ; 181: cs\Microsoft.Maui.Controls.resources => 0x4b2913cb => 297
	i32 1264511973, ; 182: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 271
	i32 1267360935, ; 183: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 275
	i32 1273260888, ; 184: Xamarin.AndroidX.Collection.Ktx => 0x4be46b58 => 227
	i32 1275534314, ; 185: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 292
	i32 1278448581, ; 186: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 219
	i32 1283425954, ; 187: LiveChartsCore.SkiaSharpView => 0x4c7f86a2 => 182
	i32 1293217323, ; 188: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 238
	i32 1308624726, ; 189: hr\Microsoft.Maui.Controls.resources.dll => 0x4e000756 => 306
	i32 1309188875, ; 190: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1322716291, ; 191: Xamarin.AndroidX.Window.dll => 0x4ed70c83 => 280
	i32 1324164729, ; 192: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 193: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1336711579, ; 194: zh-HK\Microsoft.Maui.Controls.resources.dll => 0x4fac999b => 326
	i32 1338318188, ; 195: ExcelNumberFormat.dll => 0x4fc51d6c => 176
	i32 1338781641, ; 196: DocumentFormat.OpenXml.dll => 0x4fcc2fc9 => 175
	i32 1364015309, ; 197: System.IO => 0x514d38cd => 57
	i32 1373134921, ; 198: zh-Hans\Microsoft.Maui.Controls.resources => 0x51d86049 => 327
	i32 1376866003, ; 199: Xamarin.AndroidX.SavedState => 0x52114ed3 => 267
	i32 1379779777, ; 200: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1402170036, ; 201: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 202: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 231
	i32 1408764838, ; 203: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 204: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1422545099, ; 205: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1430672901, ; 206: ar\Microsoft.Maui.Controls.resources => 0x55465605 => 295
	i32 1434145427, ; 207: System.Runtime.Handles => 0x557b5293 => 104
	i32 1435222561, ; 208: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 284
	i32 1439761251, ; 209: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1452070440, ; 210: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 211: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 212: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 213: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1461004990, ; 214: es\Microsoft.Maui.Controls.resources => 0x57152abe => 301
	i32 1461234159, ; 215: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 216: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 217: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 218: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 221
	i32 1470490898, ; 219: Microsoft.Extensions.Primitives => 0x57a5e912 => 192
	i32 1479771757, ; 220: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 221: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 222: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1490025113, ; 223: Xamarin.AndroidX.SavedState.SavedState.Ktx.dll => 0x58cffa99 => 268
	i32 1526286932, ; 224: vi\Microsoft.Maui.Controls.resources.dll => 0x5af94a54 => 325
	i32 1536373174, ; 225: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 226: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 227: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 228: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1565862583, ; 229: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 230: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 231: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 232: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1582372066, ; 233: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 237
	i32 1592978981, ; 234: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1597949149, ; 235: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 285
	i32 1601112923, ; 236: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1604827217, ; 237: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 238: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622152042, ; 239: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 257
	i32 1622358360, ; 240: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1623212457, ; 241: SkiaSharp.Views.Maui.Controls => 0x60c041a9 => 206
	i32 1624863272, ; 242: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 279
	i32 1635184631, ; 243: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 241
	i32 1636350590, ; 244: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 234
	i32 1639515021, ; 245: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 246: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 247: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 248: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 249: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 273
	i32 1658251792, ; 250: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 282
	i32 1670060433, ; 251: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 229
	i32 1675553242, ; 252: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 253: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 254: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 255: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1691477237, ; 256: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 257: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1698840827, ; 258: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 289
	i32 1701541528, ; 259: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1704247311, ; 260: BeursfuifChiroZevendonk => 0x6594c00f => 0
	i32 1720223769, ; 261: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx => 0x66888819 => 250
	i32 1726116996, ; 262: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 263: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1729485958, ; 264: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 225
	i32 1743415430, ; 265: ca\Microsoft.Maui.Controls.resources => 0x67ea6886 => 296
	i32 1744735666, ; 266: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 267: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 268: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 269: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 270: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 271: System.Reflection.Extensions => 0x6942234e => 93
	i32 1766324549, ; 272: Xamarin.AndroidX.SwipeRefreshLayout => 0x6947f945 => 272
	i32 1770582343, ; 273: Microsoft.Extensions.Logging.dll => 0x6988f147 => 188
	i32 1776026572, ; 274: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 275: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 276: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1782862114, ; 277: ms\Microsoft.Maui.Controls.resources => 0x6a445122 => 312
	i32 1788241197, ; 278: Xamarin.AndroidX.Fragment => 0x6a96652d => 243
	i32 1793755602, ; 279: he\Microsoft.Maui.Controls.resources => 0x6aea89d2 => 304
	i32 1808609942, ; 280: Xamarin.AndroidX.Loader => 0x6bcd3296 => 257
	i32 1813058853, ; 281: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 288
	i32 1813201214, ; 282: Xamarin.Google.Android.Material => 0x6c13413e => 282
	i32 1818569960, ; 283: Xamarin.AndroidX.Navigation.UI.dll => 0x6c652ce8 => 262
	i32 1818787751, ; 284: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 285: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 286: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1828688058, ; 287: Microsoft.Extensions.Logging.Abstractions.dll => 0x6cff90ba => 189
	i32 1847515442, ; 288: Xamarin.Android.Glide.Annotations => 0x6e1ed932 => 212
	i32 1853025655, ; 289: sv\Microsoft.Maui.Controls.resources => 0x6e72ed77 => 321
	i32 1856505197, ; 290: Irony => 0x6ea8056d => 179
	i32 1858542181, ; 291: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 292: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1875935024, ; 293: fr\Microsoft.Maui.Controls.resources => 0x6fd07f30 => 303
	i32 1879696579, ; 294: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 295: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 223
	i32 1888955245, ; 296: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 297: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1893218855, ; 298: cs\Microsoft.Maui.Controls.resources.dll => 0x70d83a27 => 297
	i32 1898237753, ; 299: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 300: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1910275211, ; 301: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 302: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1953182387, ; 303: id\Microsoft.Maui.Controls.resources.dll => 0x746b32b3 => 308
	i32 1956758971, ; 304: System.Resources.Writer => 0x74a1c5bb => 100
	i32 1961813231, ; 305: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 269
	i32 1968388702, ; 306: Microsoft.Extensions.Configuration.dll => 0x75533a5e => 184
	i32 1981716658, ; 307: SharpHook.Reactive => 0x761e98b2 => 201
	i32 1983156543, ; 308: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 289
	i32 1985761444, ; 309: Xamarin.Android.Glide.GifDecoder => 0x765c50a4 => 214
	i32 2003115576, ; 310: el\Microsoft.Maui.Controls.resources => 0x77651e38 => 300
	i32 2011961780, ; 311: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 312: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 254
	i32 2031763787, ; 313: Xamarin.Android.Glide => 0x791a414b => 211
	i32 2045470958, ; 314: System.Private.Xml => 0x79eb68ee => 88
	i32 2055257422, ; 315: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 249
	i32 2060060697, ; 316: System.Windows.dll => 0x7aca0819 => 154
	i32 2066184531, ; 317: de\Microsoft.Maui.Controls.resources => 0x7b277953 => 299
	i32 2070888862, ; 318: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2079903147, ; 319: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 320: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2127167465, ; 321: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 322: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 323: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 324: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2159891885, ; 325: Microsoft.Maui => 0x80bd55ad => 196
	i32 2166698602, ; 326: ClosedXML => 0x8125326a => 173
	i32 2169148018, ; 327: hu\Microsoft.Maui.Controls.resources => 0x814a9272 => 307
	i32 2181898931, ; 328: Microsoft.Extensions.Options.dll => 0x820d22b3 => 191
	i32 2192057212, ; 329: Microsoft.Extensions.Logging.Abstractions => 0x82a8237c => 189
	i32 2193016926, ; 330: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2201107256, ; 331: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 293
	i32 2201231467, ; 332: System.Net.Http => 0x8334206b => 64
	i32 2207618523, ; 333: it\Microsoft.Maui.Controls.resources => 0x839595db => 309
	i32 2217644978, ; 334: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 276
	i32 2222056684, ; 335: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2243124894, ; 336: Maui.ColorPicker.dll => 0x85b35e9e => 199
	i32 2244775296, ; 337: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 258
	i32 2252106437, ; 338: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2253078669, ; 339: SharpHook => 0x864b408d => 200
	i32 2256313426, ; 340: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 341: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2266799131, ; 342: Microsoft.Extensions.Configuration.Abstractions => 0x871c9c1b => 185
	i32 2267999099, ; 343: Xamarin.Android.Glide.DiskLruCache.dll => 0x872eeb7b => 213
	i32 2279755925, ; 344: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 265
	i32 2293034957, ; 345: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 346: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 347: System.Net.Mail => 0x88ffe49e => 66
	i32 2303942373, ; 348: nb\Microsoft.Maui.Controls.resources => 0x89535ee5 => 313
	i32 2305521784, ; 349: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2315684594, ; 350: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 217
	i32 2320631194, ; 351: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2340441535, ; 352: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 353: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2353062107, ; 354: System.Net.Primitives => 0x8c40e0db => 70
	i32 2364201794, ; 355: SkiaSharp.Views.Maui.Core => 0x8ceadb42 => 208
	i32 2366048013, ; 356: hu\Microsoft.Maui.Controls.resources.dll => 0x8d07070d => 307
	i32 2368005991, ; 357: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2371007202, ; 358: Microsoft.Extensions.Configuration => 0x8d52b2e2 => 184
	i32 2378619854, ; 359: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 360: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2395872292, ; 361: id\Microsoft.Maui.Controls.resources => 0x8ece1c24 => 308
	i32 2401565422, ; 362: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2403452196, ; 363: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 240
	i32 2421380589, ; 364: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2423080555, ; 365: Xamarin.AndroidX.Collection.Ktx.dll => 0x906d466b => 227
	i32 2427813419, ; 366: hi\Microsoft.Maui.Controls.resources => 0x90b57e2b => 305
	i32 2435356389, ; 367: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 368: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 369: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 370: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 371: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465532216, ; 372: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 230
	i32 2471841756, ; 373: netstandard.dll => 0x93554fdc => 167
	i32 2475788418, ; 374: Java.Interop.dll => 0x93918882 => 168
	i32 2480646305, ; 375: Microsoft.Maui.Controls => 0x93dba8a1 => 194
	i32 2483903535, ; 376: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 377: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 378: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 379: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2503351294, ; 380: ko\Microsoft.Maui.Controls.resources.dll => 0x95361bfe => 311
	i32 2505896520, ; 381: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 252
	i32 2521915375, ; 382: SkiaSharp.Views.Maui.Controls.Compatibility => 0x96515fef => 207
	i32 2522472828, ; 383: Xamarin.Android.Glide.dll => 0x9659e17c => 211
	i32 2538310050, ; 384: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2538318350, ; 385: Irony.dll => 0x974baa0e => 179
	i32 2541464940, ; 386: SharpHook.Reactive.dll => 0x977bad6c => 201
	i32 2550873716, ; 387: hr\Microsoft.Maui.Controls.resources => 0x980b3e74 => 306
	i32 2556439392, ; 388: LiveChartsCore.SkiaSharpView.Maui => 0x98602b60 => 183
	i32 2562349572, ; 389: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 390: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2576534780, ; 391: ja\Microsoft.Maui.Controls.resources.dll => 0x9992ccfc => 310
	i32 2581783588, ; 392: Xamarin.AndroidX.Lifecycle.Runtime.Ktx => 0x99e2e424 => 253
	i32 2581819634, ; 393: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 275
	i32 2585220780, ; 394: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 395: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 396: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2593496499, ; 397: pl\Microsoft.Maui.Controls.resources => 0x9a959db3 => 315
	i32 2605712449, ; 398: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 293
	i32 2615233544, ; 399: Xamarin.AndroidX.Fragment.Ktx => 0x9be14c08 => 244
	i32 2616218305, ; 400: Microsoft.Extensions.Logging.Debug.dll => 0x9bf052c1 => 190
	i32 2617129537, ; 401: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 402: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2620871830, ; 403: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 234
	i32 2624644809, ; 404: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 239
	i32 2625339995, ; 405: SkiaSharp.Views.Maui.Core.dll => 0x9c7b825b => 208
	i32 2626831493, ; 406: ja\Microsoft.Maui.Controls.resources => 0x9c924485 => 310
	i32 2627185994, ; 407: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2629843544, ; 408: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2633051222, ; 409: Xamarin.AndroidX.Lifecycle.LiveData => 0x9cf12c56 => 248
	i32 2663391936, ; 410: Xamarin.Android.Glide.DiskLruCache => 0x9ec022c0 => 213
	i32 2663698177, ; 411: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 412: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 413: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 414: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 415: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 416: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 417: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 273
	i32 2715334215, ; 418: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2717744543, ; 419: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 420: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2724373263, ; 421: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2732626843, ; 422: Xamarin.AndroidX.Activity => 0xa2e0939b => 215
	i32 2735172069, ; 423: System.Threading.Channels => 0xa30769e5 => 139
	i32 2737747696, ; 424: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 221
	i32 2740698338, ; 425: ca\Microsoft.Maui.Controls.resources.dll => 0xa35bbce2 => 296
	i32 2740948882, ; 426: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 427: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2752995522, ; 428: pt-BR\Microsoft.Maui.Controls.resources => 0xa41760c2 => 316
	i32 2758225723, ; 429: Microsoft.Maui.Controls.Xaml => 0xa4672f3b => 195
	i32 2764765095, ; 430: Microsoft.Maui.dll => 0xa4caf7a7 => 196
	i32 2765824710, ; 431: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2770495804, ; 432: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 287
	i32 2778768386, ; 433: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 278
	i32 2779977773, ; 434: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 266
	i32 2785988530, ; 435: th\Microsoft.Maui.Controls.resources => 0xa60ecfb2 => 322
	i32 2788224221, ; 436: Xamarin.AndroidX.Fragment.Ktx.dll => 0xa630ecdd => 244
	i32 2795602088, ; 437: SkiaSharp.Views.Android.dll => 0xa6a180a8 => 205
	i32 2801831435, ; 438: Microsoft.Maui.Graphics => 0xa7008e0b => 198
	i32 2803228030, ; 439: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2810250172, ; 440: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 231
	i32 2819470561, ; 441: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 442: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2821294376, ; 443: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 266
	i32 2824502124, ; 444: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2838993487, ; 445: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx.dll => 0xa9379a4f => 255
	i32 2849599387, ; 446: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2853208004, ; 447: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 278
	i32 2855708567, ; 448: Xamarin.AndroidX.Transition => 0xaa36a797 => 274
	i32 2861098320, ; 449: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2861189240, ; 450: Microsoft.Maui.Essentials => 0xaa8a4878 => 197
	i32 2870099610, ; 451: Xamarin.AndroidX.Activity.Ktx.dll => 0xab123e9a => 216
	i32 2875164099, ; 452: Jsr305Binding.dll => 0xab5f85c3 => 283
	i32 2875220617, ; 453: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2877542466, ; 454: ClosedXML.dll => 0xab83d042 => 173
	i32 2884993177, ; 455: Xamarin.AndroidX.ExifInterface => 0xabf58099 => 242
	i32 2887636118, ; 456: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 457: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 458: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 459: System.Reflection => 0xacf080de => 97
	i32 2904610611, ; 460: XLParser => 0xad20d733 => 294
	i32 2905242038, ; 461: mscorlib.dll => 0xad2a79b6 => 166
	i32 2909740682, ; 462: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2912489636, ; 463: SkiaSharp.Views.Android => 0xad9910a4 => 205
	i32 2916838712, ; 464: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 279
	i32 2919462931, ; 465: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 466: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 218
	i32 2936416060, ; 467: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 468: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 469: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2959614098, ; 470: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 471: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2972252294, ; 472: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2978675010, ; 473: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 238
	i32 2987532451, ; 474: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 269
	i32 2996846495, ; 475: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 251
	i32 3016983068, ; 476: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 271
	i32 3023353419, ; 477: WindowsBase.dll => 0xb434b64b => 165
	i32 3024354802, ; 478: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 246
	i32 3038032645, ; 479: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 329
	i32 3053864966, ; 480: fi\Microsoft.Maui.Controls.resources.dll => 0xb6064806 => 302
	i32 3056245963, ; 481: Xamarin.AndroidX.SavedState.SavedState.Ktx => 0xb62a9ccb => 268
	i32 3057625584, ; 482: Xamarin.AndroidX.Navigation.Common => 0xb63fa9f0 => 259
	i32 3059408633, ; 483: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 484: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 485: System.Threading.Tasks => 0xb755818f => 144
	i32 3081706019, ; 486: LiveChartsCore => 0xb7af1a23 => 180
	i32 3090735792, ; 487: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 488: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 489: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 490: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3118851116, ; 491: ExcelNumberFormat => 0xb9e5e42c => 176
	i32 3121463068, ; 492: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 493: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 494: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3147165239, ; 495: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 496: GoogleGson.dll => 0xbba64c02 => 177
	i32 3159123045, ; 497: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 498: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3178803400, ; 499: Xamarin.AndroidX.Navigation.Fragment.dll => 0xbd78b0c8 => 260
	i32 3178908327, ; 500: SixLabors.Fonts.dll => 0xbd7a4aa7 => 202
	i32 3192346100, ; 501: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 502: System.Web => 0xbe592c0c => 153
	i32 3204380047, ; 503: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 504: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3211777861, ; 505: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 237
	i32 3220365878, ; 506: System.Threading => 0xbff2e236 => 148
	i32 3226221578, ; 507: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3251039220, ; 508: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3258312781, ; 509: Xamarin.AndroidX.CardView => 0xc235e84d => 225
	i32 3265493905, ; 510: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 511: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3277815716, ; 512: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 513: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 514: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3290767353, ; 515: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 516: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 517: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3305363605, ; 518: fi\Microsoft.Maui.Controls.resources => 0xc503d895 => 302
	i32 3316684772, ; 519: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317135071, ; 520: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 235
	i32 3317144872, ; 521: System.Data => 0xc5b79d28 => 24
	i32 3340387945, ; 522: SkiaSharp => 0xc71a4669 => 203
	i32 3340431453, ; 523: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 223
	i32 3345895724, ; 524: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 264
	i32 3346324047, ; 525: Xamarin.AndroidX.Navigation.Runtime => 0xc774da4f => 261
	i32 3357674450, ; 526: ru\Microsoft.Maui.Controls.resources => 0xc8220bd2 => 319
	i32 3358260929, ; 527: System.Text.Json => 0xc82afec1 => 137
	i32 3362336904, ; 528: Xamarin.AndroidX.Activity.Ktx => 0xc8693088 => 216
	i32 3362522851, ; 529: Xamarin.AndroidX.Core => 0xc86c06e3 => 232
	i32 3366347497, ; 530: Java.Interop => 0xc8a662e9 => 168
	i32 3374999561, ; 531: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 265
	i32 3381016424, ; 532: da\Microsoft.Maui.Controls.resources => 0xc9863768 => 298
	i32 3384551493, ; 533: LiveChartsCore.dll => 0xc9bc2845 => 180
	i32 3395150330, ; 534: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 535: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3405233483, ; 536: Xamarin.AndroidX.CustomView.PoolingContainer => 0xcaf7bd4b => 236
	i32 3428513518, ; 537: Microsoft.Extensions.DependencyInjection.dll => 0xcc5af6ee => 186
	i32 3429136800, ; 538: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 539: netstandard => 0xcc7d82b4 => 167
	i32 3441283291, ; 540: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 239
	i32 3445260447, ; 541: System.Formats.Tar => 0xcd5a809f => 39
	i32 3452344032, ; 542: Microsoft.Maui.Controls.Compatibility.dll => 0xcdc696e0 => 193
	i32 3458724246, ; 543: pt\Microsoft.Maui.Controls.resources.dll => 0xce27f196 => 317
	i32 3466574376, ; 544: SkiaSharp.Views.Maui.Controls.Compatibility.dll => 0xce9fba28 => 207
	i32 3471940407, ; 545: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3473156932, ; 546: SkiaSharp.Views.Maui.Controls.dll => 0xcf042b44 => 206
	i32 3476120550, ; 547: Mono.Android => 0xcf3163e6 => 171
	i32 3484440000, ; 548: ro\Microsoft.Maui.Controls.resources => 0xcfb055c0 => 318
	i32 3485117614, ; 549: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3486566296, ; 550: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 551: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 228
	i32 3509114376, ; 552: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 553: System.Security.dll => 0xd1854eb4 => 130
	i32 3530912306, ; 554: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 555: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3556829416, ; 556: LiveChartsCore.Behaviours.dll => 0xd400e8e8 => 181
	i32 3560100363, ; 557: System.Threading.Timer => 0xd432d20b => 147
	i32 3570554715, ; 558: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3580758918, ; 559: zh-HK\Microsoft.Maui.Controls.resources => 0xd56e0b86 => 326
	i32 3592228221, ; 560: zh-Hant\Microsoft.Maui.Controls.resources.dll => 0xd61d0d7d => 328
	i32 3597029428, ; 561: Xamarin.Android.Glide.GifDecoder.dll => 0xd6665034 => 214
	i32 3598340787, ; 562: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 563: System.Linq.dll => 0xd715a361 => 61
	i32 3624195450, ; 564: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3627220390, ; 565: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 263
	i32 3633644679, ; 566: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 218
	i32 3638274909, ; 567: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 568: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 249
	i32 3643446276, ; 569: tr\Microsoft.Maui.Controls.resources => 0xd92a9404 => 323
	i32 3643854240, ; 570: Xamarin.AndroidX.Navigation.Fragment => 0xd930cda0 => 260
	i32 3645089577, ; 571: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3657292374, ; 572: Microsoft.Extensions.Configuration.Abstractions.dll => 0xd9fdda56 => 185
	i32 3660523487, ; 573: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3672681054, ; 574: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3682565725, ; 575: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 224
	i32 3684561358, ; 576: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 228
	i32 3700866549, ; 577: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3706696989, ; 578: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 233
	i32 3716563718, ; 579: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 580: Xamarin.AndroidX.Annotation => 0xdda814c6 => 217
	i32 3724971120, ; 581: Xamarin.AndroidX.Navigation.Common.dll => 0xde068c70 => 259
	i32 3731644420, ; 582: System.Reactive => 0xde6c6004 => 210
	i32 3732100267, ; 583: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 584: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 585: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 586: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3751619990, ; 587: da\Microsoft.Maui.Controls.resources.dll => 0xdf9d2d96 => 298
	i32 3786282454, ; 588: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 226
	i32 3792276235, ; 589: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3792835768, ; 590: HarfBuzzSharp => 0xe21214b8 => 178
	i32 3800979733, ; 591: Microsoft.Maui.Controls.Compatibility => 0xe28e5915 => 193
	i32 3802395368, ; 592: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 593: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3822443793, ; 594: DocumentFormat.OpenXml => 0xe3d5dd11 => 175
	i32 3823082795, ; 595: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 596: System.Numerics.dll => 0xe4436460 => 83
	i32 3841636137, ; 597: Microsoft.Extensions.DependencyInjection.Abstractions.dll => 0xe4fab729 => 187
	i32 3844307129, ; 598: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3849253459, ; 599: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3860151183, ; 600: Maui.ColorPicker => 0xe6153b8f => 199
	i32 3870376305, ; 601: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 602: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 603: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3885497537, ; 604: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3885922214, ; 605: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 274
	i32 3888767677, ; 606: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 264
	i32 3896106733, ; 607: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 608: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 232
	i32 3901907137, ; 609: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3904638161, ; 610: XLParser.dll => 0xe8bc0cd1 => 294
	i32 3920221145, ; 611: nl\Microsoft.Maui.Controls.resources.dll => 0xe9a9d3d9 => 314
	i32 3920810846, ; 612: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 613: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 277
	i32 3928044579, ; 614: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 615: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3931092270, ; 616: Xamarin.AndroidX.Navigation.UI => 0xea4fb52e => 262
	i32 3945713374, ; 617: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3952357212, ; 618: System.IO.Packaging.dll => 0xeb942f5c => 209
	i32 3953953790, ; 619: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 3955647286, ; 620: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 220
	i32 3959773229, ; 621: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 251
	i32 4003436829, ; 622: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4003906742, ; 623: HarfBuzzSharp.dll => 0xeea6c4b6 => 178
	i32 4015948917, ; 624: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 219
	i32 4025784931, ; 625: System.Memory => 0xeff49a63 => 62
	i32 4046471985, ; 626: Microsoft.Maui.Controls.Xaml.dll => 0xf1304331 => 195
	i32 4054681211, ; 627: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4066802364, ; 628: SkiaSharp.HarfBuzz => 0xf2667abc => 204
	i32 4068434129, ; 629: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4073602200, ; 630: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4091086043, ; 631: el\Microsoft.Maui.Controls.resources.dll => 0xf3d904db => 300
	i32 4094352644, ; 632: Microsoft.Maui.Essentials.dll => 0xf40add04 => 197
	i32 4099507663, ; 633: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 634: System.Private.Uri => 0xf462c30d => 86
	i32 4101593132, ; 635: Xamarin.AndroidX.Emoji2 => 0xf479582c => 240
	i32 4103439459, ; 636: uk\Microsoft.Maui.Controls.resources.dll => 0xf4958463 => 324
	i32 4126470640, ; 637: Microsoft.Extensions.DependencyInjection => 0xf5f4f1f0 => 186
	i32 4127667938, ; 638: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 639: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 640: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4150914736, ; 641: uk\Microsoft.Maui.Controls.resources => 0xf769eeb0 => 324
	i32 4151237749, ; 642: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 643: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 644: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 645: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 646: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4182413190, ; 647: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 256
	i32 4185676441, ; 648: System.Security => 0xf97c5a99 => 130
	i32 4196529839, ; 649: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 650: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4249188766, ; 651: nb\Microsoft.Maui.Controls.resources.dll => 0xfd45799e => 313
	i32 4256097574, ; 652: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 233
	i32 4258378803, ; 653: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx => 0xfdd1b433 => 255
	i32 4260525087, ; 654: System.Buffers => 0xfdf2741f => 7
	i32 4271975918, ; 655: Microsoft.Maui.Controls.dll => 0xfea12dee => 194
	i32 4274623895, ; 656: CommunityToolkit.Mvvm.dll => 0xfec99597 => 174
	i32 4274976490, ; 657: System.Runtime.Numerics => 0xfecef6ea => 110
	i32 4292120959, ; 658: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 256
	i32 4294763496 ; 659: Xamarin.AndroidX.ExifInterface.dll => 0xfffce3e8 => 242
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [660 x i32] [
	i32 68, ; 0
	i32 67, ; 1
	i32 182, ; 2
	i32 108, ; 3
	i32 252, ; 4
	i32 286, ; 5
	i32 48, ; 6
	i32 0, ; 7
	i32 295, ; 8
	i32 80, ; 9
	i32 304, ; 10
	i32 145, ; 11
	i32 30, ; 12
	i32 328, ; 13
	i32 124, ; 14
	i32 198, ; 15
	i32 102, ; 16
	i32 312, ; 17
	i32 270, ; 18
	i32 107, ; 19
	i32 270, ; 20
	i32 139, ; 21
	i32 290, ; 22
	i32 327, ; 23
	i32 320, ; 24
	i32 77, ; 25
	i32 124, ; 26
	i32 13, ; 27
	i32 226, ; 28
	i32 132, ; 29
	i32 272, ; 30
	i32 151, ; 31
	i32 18, ; 32
	i32 224, ; 33
	i32 26, ; 34
	i32 246, ; 35
	i32 1, ; 36
	i32 59, ; 37
	i32 42, ; 38
	i32 91, ; 39
	i32 229, ; 40
	i32 147, ; 41
	i32 248, ; 42
	i32 245, ; 43
	i32 54, ; 44
	i32 69, ; 45
	i32 325, ; 46
	i32 215, ; 47
	i32 83, ; 48
	i32 303, ; 49
	i32 202, ; 50
	i32 247, ; 51
	i32 131, ; 52
	i32 55, ; 53
	i32 149, ; 54
	i32 74, ; 55
	i32 145, ; 56
	i32 62, ; 57
	i32 146, ; 58
	i32 329, ; 59
	i32 165, ; 60
	i32 323, ; 61
	i32 230, ; 62
	i32 12, ; 63
	i32 243, ; 64
	i32 125, ; 65
	i32 152, ; 66
	i32 113, ; 67
	i32 166, ; 68
	i32 164, ; 69
	i32 245, ; 70
	i32 258, ; 71
	i32 301, ; 72
	i32 84, ; 73
	i32 192, ; 74
	i32 203, ; 75
	i32 150, ; 76
	i32 290, ; 77
	i32 60, ; 78
	i32 322, ; 79
	i32 188, ; 80
	i32 51, ; 81
	i32 103, ; 82
	i32 114, ; 83
	i32 40, ; 84
	i32 283, ; 85
	i32 281, ; 86
	i32 120, ; 87
	i32 52, ; 88
	i32 44, ; 89
	i32 210, ; 90
	i32 119, ; 91
	i32 235, ; 92
	i32 314, ; 93
	i32 241, ; 94
	i32 81, ; 95
	i32 136, ; 96
	i32 277, ; 97
	i32 222, ; 98
	i32 8, ; 99
	i32 73, ; 100
	i32 155, ; 101
	i32 292, ; 102
	i32 154, ; 103
	i32 92, ; 104
	i32 287, ; 105
	i32 45, ; 106
	i32 291, ; 107
	i32 109, ; 108
	i32 129, ; 109
	i32 181, ; 110
	i32 25, ; 111
	i32 212, ; 112
	i32 72, ; 113
	i32 55, ; 114
	i32 46, ; 115
	i32 320, ; 116
	i32 204, ; 117
	i32 191, ; 118
	i32 236, ; 119
	i32 22, ; 120
	i32 250, ; 121
	i32 86, ; 122
	i32 43, ; 123
	i32 160, ; 124
	i32 71, ; 125
	i32 263, ; 126
	i32 305, ; 127
	i32 3, ; 128
	i32 42, ; 129
	i32 63, ; 130
	i32 319, ; 131
	i32 16, ; 132
	i32 53, ; 133
	i32 316, ; 134
	i32 286, ; 135
	i32 200, ; 136
	i32 105, ; 137
	i32 291, ; 138
	i32 309, ; 139
	i32 284, ; 140
	i32 247, ; 141
	i32 34, ; 142
	i32 158, ; 143
	i32 85, ; 144
	i32 32, ; 145
	i32 318, ; 146
	i32 12, ; 147
	i32 51, ; 148
	i32 56, ; 149
	i32 267, ; 150
	i32 36, ; 151
	i32 187, ; 152
	i32 285, ; 153
	i32 183, ; 154
	i32 220, ; 155
	i32 35, ; 156
	i32 299, ; 157
	i32 58, ; 158
	i32 254, ; 159
	i32 177, ; 160
	i32 17, ; 161
	i32 209, ; 162
	i32 288, ; 163
	i32 164, ; 164
	i32 321, ; 165
	i32 315, ; 166
	i32 311, ; 167
	i32 253, ; 168
	i32 190, ; 169
	i32 280, ; 170
	i32 317, ; 171
	i32 153, ; 172
	i32 276, ; 173
	i32 261, ; 174
	i32 222, ; 175
	i32 29, ; 176
	i32 174, ; 177
	i32 52, ; 178
	i32 281, ; 179
	i32 5, ; 180
	i32 297, ; 181
	i32 271, ; 182
	i32 275, ; 183
	i32 227, ; 184
	i32 292, ; 185
	i32 219, ; 186
	i32 182, ; 187
	i32 238, ; 188
	i32 306, ; 189
	i32 85, ; 190
	i32 280, ; 191
	i32 61, ; 192
	i32 112, ; 193
	i32 326, ; 194
	i32 176, ; 195
	i32 175, ; 196
	i32 57, ; 197
	i32 327, ; 198
	i32 267, ; 199
	i32 99, ; 200
	i32 19, ; 201
	i32 231, ; 202
	i32 111, ; 203
	i32 101, ; 204
	i32 102, ; 205
	i32 295, ; 206
	i32 104, ; 207
	i32 284, ; 208
	i32 71, ; 209
	i32 38, ; 210
	i32 32, ; 211
	i32 103, ; 212
	i32 73, ; 213
	i32 301, ; 214
	i32 9, ; 215
	i32 123, ; 216
	i32 46, ; 217
	i32 221, ; 218
	i32 192, ; 219
	i32 9, ; 220
	i32 43, ; 221
	i32 4, ; 222
	i32 268, ; 223
	i32 325, ; 224
	i32 31, ; 225
	i32 138, ; 226
	i32 92, ; 227
	i32 93, ; 228
	i32 49, ; 229
	i32 141, ; 230
	i32 112, ; 231
	i32 140, ; 232
	i32 237, ; 233
	i32 115, ; 234
	i32 285, ; 235
	i32 157, ; 236
	i32 76, ; 237
	i32 79, ; 238
	i32 257, ; 239
	i32 37, ; 240
	i32 206, ; 241
	i32 279, ; 242
	i32 241, ; 243
	i32 234, ; 244
	i32 64, ; 245
	i32 138, ; 246
	i32 15, ; 247
	i32 116, ; 248
	i32 273, ; 249
	i32 282, ; 250
	i32 229, ; 251
	i32 48, ; 252
	i32 70, ; 253
	i32 80, ; 254
	i32 126, ; 255
	i32 94, ; 256
	i32 121, ; 257
	i32 289, ; 258
	i32 26, ; 259
	i32 0, ; 260
	i32 250, ; 261
	i32 97, ; 262
	i32 28, ; 263
	i32 225, ; 264
	i32 296, ; 265
	i32 149, ; 266
	i32 169, ; 267
	i32 4, ; 268
	i32 98, ; 269
	i32 33, ; 270
	i32 93, ; 271
	i32 272, ; 272
	i32 188, ; 273
	i32 21, ; 274
	i32 41, ; 275
	i32 170, ; 276
	i32 312, ; 277
	i32 243, ; 278
	i32 304, ; 279
	i32 257, ; 280
	i32 288, ; 281
	i32 282, ; 282
	i32 262, ; 283
	i32 2, ; 284
	i32 134, ; 285
	i32 111, ; 286
	i32 189, ; 287
	i32 212, ; 288
	i32 321, ; 289
	i32 179, ; 290
	i32 58, ; 291
	i32 95, ; 292
	i32 303, ; 293
	i32 39, ; 294
	i32 223, ; 295
	i32 25, ; 296
	i32 94, ; 297
	i32 297, ; 298
	i32 89, ; 299
	i32 99, ; 300
	i32 10, ; 301
	i32 87, ; 302
	i32 308, ; 303
	i32 100, ; 304
	i32 269, ; 305
	i32 184, ; 306
	i32 201, ; 307
	i32 289, ; 308
	i32 214, ; 309
	i32 300, ; 310
	i32 7, ; 311
	i32 254, ; 312
	i32 211, ; 313
	i32 88, ; 314
	i32 249, ; 315
	i32 154, ; 316
	i32 299, ; 317
	i32 33, ; 318
	i32 116, ; 319
	i32 82, ; 320
	i32 20, ; 321
	i32 11, ; 322
	i32 162, ; 323
	i32 3, ; 324
	i32 196, ; 325
	i32 173, ; 326
	i32 307, ; 327
	i32 191, ; 328
	i32 189, ; 329
	i32 84, ; 330
	i32 293, ; 331
	i32 64, ; 332
	i32 309, ; 333
	i32 276, ; 334
	i32 143, ; 335
	i32 199, ; 336
	i32 258, ; 337
	i32 157, ; 338
	i32 200, ; 339
	i32 41, ; 340
	i32 117, ; 341
	i32 185, ; 342
	i32 213, ; 343
	i32 265, ; 344
	i32 131, ; 345
	i32 75, ; 346
	i32 66, ; 347
	i32 313, ; 348
	i32 172, ; 349
	i32 217, ; 350
	i32 143, ; 351
	i32 106, ; 352
	i32 151, ; 353
	i32 70, ; 354
	i32 208, ; 355
	i32 307, ; 356
	i32 156, ; 357
	i32 184, ; 358
	i32 121, ; 359
	i32 127, ; 360
	i32 308, ; 361
	i32 152, ; 362
	i32 240, ; 363
	i32 141, ; 364
	i32 227, ; 365
	i32 305, ; 366
	i32 20, ; 367
	i32 14, ; 368
	i32 135, ; 369
	i32 75, ; 370
	i32 59, ; 371
	i32 230, ; 372
	i32 167, ; 373
	i32 168, ; 374
	i32 194, ; 375
	i32 15, ; 376
	i32 74, ; 377
	i32 6, ; 378
	i32 23, ; 379
	i32 311, ; 380
	i32 252, ; 381
	i32 207, ; 382
	i32 211, ; 383
	i32 91, ; 384
	i32 179, ; 385
	i32 201, ; 386
	i32 306, ; 387
	i32 183, ; 388
	i32 1, ; 389
	i32 136, ; 390
	i32 310, ; 391
	i32 253, ; 392
	i32 275, ; 393
	i32 134, ; 394
	i32 69, ; 395
	i32 146, ; 396
	i32 315, ; 397
	i32 293, ; 398
	i32 244, ; 399
	i32 190, ; 400
	i32 88, ; 401
	i32 96, ; 402
	i32 234, ; 403
	i32 239, ; 404
	i32 208, ; 405
	i32 310, ; 406
	i32 31, ; 407
	i32 45, ; 408
	i32 248, ; 409
	i32 213, ; 410
	i32 109, ; 411
	i32 158, ; 412
	i32 35, ; 413
	i32 22, ; 414
	i32 114, ; 415
	i32 57, ; 416
	i32 273, ; 417
	i32 144, ; 418
	i32 118, ; 419
	i32 120, ; 420
	i32 110, ; 421
	i32 215, ; 422
	i32 139, ; 423
	i32 221, ; 424
	i32 296, ; 425
	i32 54, ; 426
	i32 105, ; 427
	i32 316, ; 428
	i32 195, ; 429
	i32 196, ; 430
	i32 133, ; 431
	i32 287, ; 432
	i32 278, ; 433
	i32 266, ; 434
	i32 322, ; 435
	i32 244, ; 436
	i32 205, ; 437
	i32 198, ; 438
	i32 159, ; 439
	i32 231, ; 440
	i32 163, ; 441
	i32 132, ; 442
	i32 266, ; 443
	i32 161, ; 444
	i32 255, ; 445
	i32 140, ; 446
	i32 278, ; 447
	i32 274, ; 448
	i32 169, ; 449
	i32 197, ; 450
	i32 216, ; 451
	i32 283, ; 452
	i32 40, ; 453
	i32 173, ; 454
	i32 242, ; 455
	i32 81, ; 456
	i32 56, ; 457
	i32 37, ; 458
	i32 97, ; 459
	i32 294, ; 460
	i32 166, ; 461
	i32 172, ; 462
	i32 205, ; 463
	i32 279, ; 464
	i32 82, ; 465
	i32 218, ; 466
	i32 98, ; 467
	i32 30, ; 468
	i32 159, ; 469
	i32 18, ; 470
	i32 127, ; 471
	i32 119, ; 472
	i32 238, ; 473
	i32 269, ; 474
	i32 251, ; 475
	i32 271, ; 476
	i32 165, ; 477
	i32 246, ; 478
	i32 329, ; 479
	i32 302, ; 480
	i32 268, ; 481
	i32 259, ; 482
	i32 170, ; 483
	i32 16, ; 484
	i32 144, ; 485
	i32 180, ; 486
	i32 125, ; 487
	i32 118, ; 488
	i32 38, ; 489
	i32 115, ; 490
	i32 176, ; 491
	i32 47, ; 492
	i32 142, ; 493
	i32 117, ; 494
	i32 34, ; 495
	i32 177, ; 496
	i32 95, ; 497
	i32 53, ; 498
	i32 260, ; 499
	i32 202, ; 500
	i32 129, ; 501
	i32 153, ; 502
	i32 24, ; 503
	i32 161, ; 504
	i32 237, ; 505
	i32 148, ; 506
	i32 104, ; 507
	i32 89, ; 508
	i32 225, ; 509
	i32 60, ; 510
	i32 142, ; 511
	i32 100, ; 512
	i32 5, ; 513
	i32 13, ; 514
	i32 122, ; 515
	i32 135, ; 516
	i32 28, ; 517
	i32 302, ; 518
	i32 72, ; 519
	i32 235, ; 520
	i32 24, ; 521
	i32 203, ; 522
	i32 223, ; 523
	i32 264, ; 524
	i32 261, ; 525
	i32 319, ; 526
	i32 137, ; 527
	i32 216, ; 528
	i32 232, ; 529
	i32 168, ; 530
	i32 265, ; 531
	i32 298, ; 532
	i32 180, ; 533
	i32 101, ; 534
	i32 123, ; 535
	i32 236, ; 536
	i32 186, ; 537
	i32 163, ; 538
	i32 167, ; 539
	i32 239, ; 540
	i32 39, ; 541
	i32 193, ; 542
	i32 317, ; 543
	i32 207, ; 544
	i32 17, ; 545
	i32 206, ; 546
	i32 171, ; 547
	i32 318, ; 548
	i32 137, ; 549
	i32 150, ; 550
	i32 228, ; 551
	i32 155, ; 552
	i32 130, ; 553
	i32 19, ; 554
	i32 65, ; 555
	i32 181, ; 556
	i32 147, ; 557
	i32 47, ; 558
	i32 326, ; 559
	i32 328, ; 560
	i32 214, ; 561
	i32 79, ; 562
	i32 61, ; 563
	i32 106, ; 564
	i32 263, ; 565
	i32 218, ; 566
	i32 49, ; 567
	i32 249, ; 568
	i32 323, ; 569
	i32 260, ; 570
	i32 14, ; 571
	i32 185, ; 572
	i32 68, ; 573
	i32 171, ; 574
	i32 224, ; 575
	i32 228, ; 576
	i32 78, ; 577
	i32 233, ; 578
	i32 108, ; 579
	i32 217, ; 580
	i32 259, ; 581
	i32 210, ; 582
	i32 67, ; 583
	i32 63, ; 584
	i32 27, ; 585
	i32 160, ; 586
	i32 298, ; 587
	i32 226, ; 588
	i32 10, ; 589
	i32 178, ; 590
	i32 193, ; 591
	i32 11, ; 592
	i32 78, ; 593
	i32 175, ; 594
	i32 126, ; 595
	i32 83, ; 596
	i32 187, ; 597
	i32 66, ; 598
	i32 107, ; 599
	i32 199, ; 600
	i32 65, ; 601
	i32 128, ; 602
	i32 122, ; 603
	i32 77, ; 604
	i32 274, ; 605
	i32 264, ; 606
	i32 8, ; 607
	i32 232, ; 608
	i32 2, ; 609
	i32 294, ; 610
	i32 314, ; 611
	i32 44, ; 612
	i32 277, ; 613
	i32 156, ; 614
	i32 128, ; 615
	i32 262, ; 616
	i32 23, ; 617
	i32 209, ; 618
	i32 133, ; 619
	i32 220, ; 620
	i32 251, ; 621
	i32 29, ; 622
	i32 178, ; 623
	i32 219, ; 624
	i32 62, ; 625
	i32 195, ; 626
	i32 90, ; 627
	i32 204, ; 628
	i32 87, ; 629
	i32 148, ; 630
	i32 300, ; 631
	i32 197, ; 632
	i32 36, ; 633
	i32 86, ; 634
	i32 240, ; 635
	i32 324, ; 636
	i32 186, ; 637
	i32 50, ; 638
	i32 6, ; 639
	i32 90, ; 640
	i32 324, ; 641
	i32 21, ; 642
	i32 162, ; 643
	i32 96, ; 644
	i32 50, ; 645
	i32 113, ; 646
	i32 256, ; 647
	i32 130, ; 648
	i32 76, ; 649
	i32 27, ; 650
	i32 313, ; 651
	i32 233, ; 652
	i32 255, ; 653
	i32 7, ; 654
	i32 194, ; 655
	i32 174, ; 656
	i32 110, ; 657
	i32 256, ; 658
	i32 242 ; 659
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.1xx @ af27162bee43b7fecdca59b4f67aa8c175cbc875"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
