【项目说明】
这是一个简化的模块示例，Dao、Dto等均放置到一个项目中。

【注意事项】
1、这种模块结构不易于多项目协作。
如果需要将项目模块化，提高利用性，可参见【Samples.Common】、【Samples.Common.Dto】、【Samples.Server】、【Samples.Server.Dao】项目。

【项目依赖】
1、Scm.Common：框架提供的一些公共基础方法；
2、Scm.Common.Dto：Dto基类相关；
3、Scm.Common.Text：字符相关工具；
4、Scm.Dao：Dao基类相关；
5、Scm.Dsa.Dba.Sugar：数据库切面服务；
6、Scm.Server：框架提供的服务相关方法；
7、Scm.Server.Api：框架提供的动态接口类库，用于提供Service的接口转换；
8、Scm.Server.Dao：服务端Dao基类相关；
9、SqlSuagerCore：数据库ORM框架。
10、MiniExcel：Excel文件快速导入导出辅助类。
11、Newtonsoft.Json：Json数据访问工具；