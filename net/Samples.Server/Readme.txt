【项目说明】
模块核心服务逻辑。

【注意事项】
1、所有继承自ApiService或AppService的类，系统默认为采用依赖注入模式；

【项目依赖】
1、Scm.Common：框架提供的一些公共基础方法；
2、Scm.Common.Dto：Dto基类相关；
3、Scm.Server：框架提供的服务相关方法；
4、Scm.Server.Dao：Dao基类相关；
5、Scm.Server.Api：框架提供的动态接口类库，用于提供Service的接口转换；
6、Scm.Dsa.Dba.Sugar：数据库切面服务；
7、SqlSuagerCore：数据库ORM框架。