# Metadata Research

## Entity
--SELECT * from DigiEntity where logicalName like '%cstm%'
Database Design:
EntityId primary key grid --主键
PhysicalName Nvarchar(64) --物理名称 用于存储创建的Entity 的View Name
LogicalName Nvarchar(64) --UI上设置的名字， 唯一。小写
BaseTableName Nvarchar(64) --基础表， 一般跟LogicalName 一致，驼峰命名
