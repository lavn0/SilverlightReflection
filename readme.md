# SilverlightReflection
Silverlightでprivate/internalメンバにアクセスするサンプル実装

利用するには、Publicer.csの実装をSilverlightのプロジェクトに組み込みます。

## 使い方

1. Class1クラスのHogeメソッドを呼び出す
（nonstatic. public/privateなどアクセス修飾子はどちらでも良い）
```csharp
var obj = new Class1();
var result = Publicer.GetMember<string>(typeof(Class1), "Hoge", obj);
```
