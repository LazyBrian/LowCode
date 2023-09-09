## 低代码平台流程
1. 创建Entity,Attributes,Forms,Views 等信息存储在数据库内。创建Entity的同时需要在数据库运行相应的脚本创建好数据库表和相应的字段

    使用SqlManagementObjects 用于在创建Entity的时候同时创建数据库对应的表结构。
```
dotnet add package Microsoft.SqlServer.SqlManagementObjects --version 170.18.0
```

2. 根据传入的相应的Entity, 获取到 Views 配置， 生成相应的动态SQL 语句。用于查询在数据库的数据，展示到Browse 页面

3. 根据传入的相应的Entity,获取到 Forms 配置， 生成相应的动态SQL 语句。 用于查询到数据库的数据，并根据Attributes 的类型等配置绑定到相应的控件内，以提供添加，编辑，删除等功能使用。

4. 需要有一个基础的Entity 用于存储动态实体的数据，用NameValueCollection 来存储字段和值。也可以使用DynamicObject 等内置类来使用

## 数据库设计

## 功能

## Entities CRUD

### Add Entity 
打开Add Entity 页面后， 展示需要填写的信息。当点击Save 的时候，自动在数据库创建一个数据库表， 并生成主键Id, Createon CreateBy 等数据。

### Edit Entity 
可编辑Entity 的一些属性， Logical Name 不可修改。

### Delete Entity
删除记录，同时数据库表也要跟着删除。

### Add One to Many /Many to One /Many to Many 关系 （待完成）

添加一个Relationship 表， 用于存储添加的关系。 添加的同时，需要处理数据库内的关系，添加外键。

**另外需要考虑添加后在数据库内添加一个View，这样可以直接查询View来获取相应的数据，如果EF Core 没有更好的处理办法的情况下。**

## Browse Attributs （CRUD 已完成）

### Add Attribute
打开Att Attribute 页面后，展示需要填写的信息，当点击Save 的时候，自动在关联的数据库表内创建一个相应的字段。


