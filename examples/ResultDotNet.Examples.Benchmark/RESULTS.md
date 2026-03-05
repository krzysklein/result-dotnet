# Benchmark results

## Version 0.3.0

```

BenchmarkDotNet v0.15.8, Windows 11 (10.0.22631.6199/23H2/2023Update/SunValley3)
Intel Core i9-14900HX 2.20GHz, 1 CPU, 32 logical and 24 physical cores
.NET SDK 10.0.103
  [Host]     : .NET 10.0.3 (10.0.3, 10.0.326.7603), X64 RyuJIT x86-64-v3
  DefaultJob : .NET 10.0.3 (10.0.3, 10.0.326.7603), X64 RyuJIT x86-64-v3


```
| Method                   | Mean      | Error     | StdDev    | Median    | Gen0   | Allocated |
|------------------------- |----------:|----------:|----------:|----------:|-------:|----------:|
| CreateResultSuccess      | 2.4994 ns | 0.0516 ns | 0.0457 ns | 2.5067 ns | 0.0021 |      40 B |
| CreateResultError        | 2.5575 ns | 0.0850 ns | 0.0835 ns | 2.5419 ns | 0.0021 |      40 B |
| AccessResultValue        | 0.0007 ns | 0.0026 ns | 0.0025 ns | 0.0000 ns |      - |         - |
| AccessResultError        | 0.4408 ns | 0.0353 ns | 0.0313 ns | 0.4448 ns |      - |         - |
| CreateValueResultSuccess | 0.0499 ns | 0.0154 ns | 0.0144 ns | 0.0486 ns |      - |         - |
| CreateValueResultError   | 0.0479 ns | 0.0152 ns | 0.0142 ns | 0.0484 ns |      - |         - |
| AccessValueResultValue   | 0.0003 ns | 0.0007 ns | 0.0008 ns | 0.0000 ns |      - |         - |
| AccessValueResultError   | 0.4032 ns | 0.0431 ns | 0.0403 ns | 0.4121 ns |      - |         - |
| ResultBindSuccess        | 4.8275 ns | 0.1290 ns | 0.1677 ns | 4.8158 ns | 0.0042 |      80 B |
| ResultBindError          | 5.0744 ns | 0.1383 ns | 0.1749 ns | 5.0105 ns | 0.0042 |      80 B |
| ResultMapSuccess         | 4.5070 ns | 0.1153 ns | 0.1079 ns | 4.4883 ns | 0.0038 |      72 B |
| ResultMapError           | 4.8167 ns | 0.1265 ns | 0.1183 ns | 4.8003 ns | 0.0038 |      72 B |
| ResultMatchSuccess       | 2.1159 ns | 0.0620 ns | 0.1019 ns | 2.1214 ns | 0.0021 |      40 B |
| ResultMatchError         | 2.0353 ns | 0.0403 ns | 0.0357 ns | 2.0363 ns | 0.0021 |      40 B |
| ValueResultBindSuccess   | 3.5562 ns | 0.0278 ns | 0.0260 ns | 3.5565 ns |      - |         - |
| ValueResultBindError     | 2.4770 ns | 0.0538 ns | 0.0504 ns | 2.4848 ns |      - |         - |
| ValueResultMapSuccess    | 1.5823 ns | 0.0394 ns | 0.0368 ns | 1.5816 ns |      - |         - |
| ValueResultMapError      | 1.5278 ns | 0.0510 ns | 0.0567 ns | 1.5058 ns |      - |         - |
| ValueResultMatchSuccess  | 0.4395 ns | 0.0241 ns | 0.0225 ns | 0.4371 ns |      - |         - |
| ValueResultMatchError    | 0.4240 ns | 0.0196 ns | 0.0183 ns | 0.4318 ns |      - |         - |
