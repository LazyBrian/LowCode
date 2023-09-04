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

##