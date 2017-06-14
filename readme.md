# SilverlightReflection
Silverlightでprivate/internalメンバにアクセスするサンプル実装

Publicer.csの実装をSilverlightのプロジェクトに組み込みます。

## 使い方

1. クラスのHogeメソッドを呼び出す
```csharp
var obj = new Class1();
var result = Publicer.GetMember<string>(obj.GetType(), "GetTest", obj);
```

2. 


```cpp
print("hello\n");
```
