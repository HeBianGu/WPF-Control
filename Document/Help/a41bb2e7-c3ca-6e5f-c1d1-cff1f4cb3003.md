# DisplayBindableBase 类
https://github.com/HeBianGu/WPF-Control

\[缺少 "T:H.Mvvm.ViewModels.Base.DisplayBindableBase" 的 &lt;summary&gt; 文档\]



## Definition
**命名空间：** <a href="1a39445a-2086-c1ca-7c41-28cbba243517">H.Mvvm.ViewModels.Base</a>  
**程序集：** H.Mvvm (在 H.Mvvm.dll 中) 版本：1.0.3+58daf524b91f25bbf55d2f64e549e4224fcf9802

**C#**
``` C#
public abstract class DisplayBindableBase : CommandsBindableBase, 
	IDable, IDisplayBindable, IIconable, INameable, IOrderable, 
	IGroupable, IDescriptionable
```

<table><tr><td><strong>Inheritance</strong></td><td><a href="https://learn.microsoft.com/dotnet/api/system.object" target="_blank" rel="noopener noreferrer">Object</a>  →  <a href="360d8001-5c49-3ab8-4aca-1d47bb7fdebe">BindableBase</a>  →  <a href="8ab78628-2bd0-bb2a-c8d0-dbc372370609">Bindable</a>  →  <a href="7abd43fb-12ec-05ae-f7f4-cc4e20c08f16">CommandsBindableBase</a>  →  DisplayBindableBase</td></tr>
<tr><td><strong>Derived</strong></td><td><a href="ad856c26-8bae-2667-466c-4854a85d948d">H.Mvvm.DisplayBindable(T)</a><br /><a href="8ba16f24-5efb-6ddb-6317-1c8d25d5fe9b">H.Mvvm.ViewModels.Base.DesignPresenterBase</a><br /><a href="33f24787-95a3-8380-161e-6f40dbca8e0c">H.Mvvm.ViewModels.Base.GroupDisplayBindableBase(T)</a></td></tr>
<tr><td><strong>Implements</strong></td><td><a href="7ccc9720-d325-f983-d8d1-b8eadac4020e">IDescriptionable</a>, <a href="fc60ab0c-0cc2-19ab-db7e-223f13fd9c0d">IDisplayBindable</a>, <a href="5341405b-8c90-727f-eb2d-d6d50c2bfe5c">IIconable</a>, <a href="027e827a-8ffd-1fb9-b33e-8ac065d37cda">IOrderable</a>, <a href="d694bff3-860c-f503-860a-370bc99903f5">IDable</a>, <a href="70cdfa7f-23c8-af1f-68b3-859abd3565aa">IGroupable</a>, <a href="33808a66-de13-9d84-9008-d5fdb9455e51">INameable</a></td></tr>
</table>



## 构造函数
<table>
<tr>
<td><a href="528b067d-672e-1fe5-7db3-9339774fa260">DisplayBindableBase</a></td>
<td>初始化 DisplayBindableBase 类的一个新实例</td></tr>
</table>

## 属性
<table>
<tr>
<td><a href="5ce3f30d-7494-8bcb-4631-b6051a66526d">CallMethodCommand</a></td>
<td><br />(继承自 <a href="8ab78628-2bd0-bb2a-c8d0-dbc372370609">Bindable</a>。)</td></tr>
<tr>
<td><a href="4ae54119-5efe-4fd5-d92f-31979f27659a">Commands</a></td>
<td><br />(继承自 <a href="7abd43fb-12ec-05ae-f7f4-cc4e20c08f16">CommandsBindableBase</a>。)</td></tr>
<tr>
<td><a href="a2348692-d5c0-a2cc-d5c7-3c07ffa4396a">Description</a></td>
<td> </td></tr>
<tr>
<td><a href="bf8f2fad-93c7-e80a-798c-75a2973769d5">GroupName</a></td>
<td> </td></tr>
<tr>
<td><a href="fb8cc2a2-61cc-462a-37f0-3f76162a45c2">Icon</a></td>
<td> </td></tr>
<tr>
<td><a href="1fe9b696-57f2-13f8-6b8b-e60f4feb87a8">ID</a></td>
<td> </td></tr>
<tr>
<td><a href="d8df4f13-37c5-b0cc-91fa-b6456796858a">IsLoaded</a></td>
<td> </td></tr>
<tr>
<td><a href="a1caaf42-824c-6072-98b1-a1e9c5b4cf9c">LoadDefaultCommand</a></td>
<td> </td></tr>
<tr>
<td><a href="b50c5e10-29b3-8ff7-caa3-235798b7c239">LoadedCommand</a></td>
<td><br />(继承自 <a href="8ab78628-2bd0-bb2a-c8d0-dbc372370609">Bindable</a>。)</td></tr>
<tr>
<td><a href="9f281146-e624-119d-73e0-3dd626568cdb">Name</a></td>
<td> </td></tr>
<tr>
<td><a href="a60d7461-f515-8cbb-d6e3-1bd23ef734ab">Order</a></td>
<td> </td></tr>
<tr>
<td><a href="edf50f2a-ae9f-6b8d-87d9-73284b2add66">RelayCommand</a></td>
<td><br />(继承自 <a href="8ab78628-2bd0-bb2a-c8d0-dbc372370609">Bindable</a>。)</td></tr>
<tr>
<td><a href="b7cbcac1-b0ed-cedb-bd3a-d4d15edd319b">ShortName</a></td>
<td> </td></tr>
</table>

## 方法
<table>
<tr>
<td><a href="9e8d7706-8c68-e48d-9083-8cb39e53a7e4">LoadDefault</a></td>
<td> </td></tr>
<tr>
<td><a href="a8f427ea-ac92-e56e-c7b8-b2cdeef36028">RaisePropertyChanged</a></td>
<td><br />(继承自 <a href="360d8001-5c49-3ab8-4aca-1d47bb7fdebe">BindableBase</a>。)</td></tr>
</table>

## 事件
<table>
<tr>
<td><a href="bd7ae655-1278-f2bf-6f7c-43023ee1c861">PropertyChanged</a></td>
<td><br />(继承自 <a href="360d8001-5c49-3ab8-4aca-1d47bb7fdebe">BindableBase</a>。)</td></tr>
</table>

## 参见


#### 引用
<a href="1a39445a-2086-c1ca-7c41-28cbba243517">H.Mvvm.ViewModels.Base 命名空间</a>  
